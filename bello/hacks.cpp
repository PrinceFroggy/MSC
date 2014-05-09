#include "memory.h"
#include "pointers.h"

#define PGMAddy1 0x1218C3F
#define PGMJmp 0x1219AD3
DWORD PGMEntry = 0x16400C4;
DWORD PGMRet = NULL;

#define SPAddy 0x011DB70A
DWORD SPEntry = 0x017EB9BC;
DWORD SPJmp = 0x012D21F0;
DWORD SPRet = NULL;

int iSetTeleportX, iSetTeleportY;

bool _isExecutedOnce = false;

__declspec(naked) void PGMHook()
{
    __asm
    {
         cmp dword ptr [esp], [PGMAddy1]
         jne PGMReturn
         mov dword ptr [esp], offset PGM

         PGMReturn:
         jmp dword ptr [PGMRet]

         PGM:
         push dword ptr [PGMJmp]
		 ret
    }
}


void TogglePGM(bool bEnable)
{
	unsigned long protect;

    if (bEnable)
    {
        PGMRet = *(DWORD*)(PGMEntry);
        VirtualProtect((void*)(PGMEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
        *(DWORD*)(PGMEntry) = (DWORD)&PGMHook;
        VirtualProtect((void*)(PGMEntry), 4, protect, &protect);
    }
    else
	{
		VirtualProtect((void*)(PGMEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
        *(DWORD*)(PGMEntry) = PGMRet;
		VirtualProtect((void*)(PGMEntry), 4, protect, &protect);
	}
}

__declspec(naked) void NewTeleportXY()
{
        __asm
        {
                mov esi, [characterBase]
                mov esi, [esi]
                lea ecx, [esi + 0x04]
                call [dwNewTeleportXYCall1]
                test eax, eax
                je TeleportEnd
                push ebx
                push edx
                push 0x00
                mov ecx, eax
                call [dwNewTeleportXYCall2]
 
                TeleportEnd:
                ret
        }
}

void SetTeleport(int x, int y)
{
	if (!_isExecutedOnce)
	{
		if (leftWallLocation(x) && rightWallLocation(x) && topWallLocation(y) && bottomWallLocation(y))
		{
			__asm
			{
				pushad;
				mov edx, x
				mov ebx, y
				call NewTeleportXY;
				popad;
			}
		}
		
		_isExecutedOnce = true;
	}
	else
	{
		iSetTeleportX = x;
		iSetTeleportY = y;
	}
}

__declspec(naked) void SpawnXY()
{
	__asm
	{
		cmp dword ptr [esp], [SPAddy] //51 8B ? 89 ? 24 ? 50 8D ? ? E8 ? ? ? ? 8B ? ? ? ? ? E8
		jne Return
		mov eax, [iSetTeleportX]
		mov ebx, [iSetTeleportY]
		mov dword ptr [esp+0x08], eax //X Coord
		mov dword ptr [esp+0x0C], ebx //Y Coord

		Return:
		//6A FF 68 ? ? ? ? 64 A1 ? ? ? ? 50 81 ? ? ? ? ? 53 55 56 57 A1 ? ? ? ? 33 ? 50 8D ? 24 ? ? ? ? 64 ? ? ? ? ? 8B ? 8B ? 24 ? ? ? ? 8B ? 24 ? ? ? ? 8B
		jmp dword ptr [SPJmp]
	}
}

void ToggleSpawnControl(bool bEnable)
{
	_isExecutedOnce = false;

	unsigned long protect;

    if (bEnable)
    {
        SPRet = *(DWORD*)(SPEntry);
        VirtualProtect((void*)(SPEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
        *(DWORD*)(SPEntry) = (DWORD)SpawnXY;
        VirtualProtect((void*)(SPEntry), 4, protect, &protect);
    }
    else
	{
		VirtualProtect((void*)(SPEntry), 4, PAGE_EXECUTE_READWRITE, &protect);
        *(DWORD*)(SPEntry) = SPRet;
		VirtualProtect((void*)(SPEntry), 4, protect, &protect);
	}
}