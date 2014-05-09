#include "functions.h"
#include "hook.h"
#include "initialize.h"
#include "pointers.h"

HMODULE kernel32Module;

CreateWindowExPtr _CreateWindowEx = &CreateWindowEx;

HMODULE hDLL;
HWND hMaple;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	
	case DLL_PROCESS_ATTACH:

		hDLL = hModule;

		DisableThreadLibraryCalls(hModule);

		kernel32Module = GetModuleHandle("kernel32.dll");

		if(kernel32Module == NULL)
		{
			MessageBox(NULL, "Couldn't get the kernel32 module handle.", "Sorry!", MB_OK);
			return false;
		}
		else
		{
			try
			{
				DWORD loadLibraryA = (DWORD) GetProcAddress(kernel32Module, "LoadLibraryA");

				if (NULL != loadLibraryA)
				{
					oldLoadLibraryA = (LoadLibraryA_T) DetourFunction(&loadLibraryA, (DWORD*) &HookLoadLibraryA, 5);
				}
			}
			catch(...)
			{
				throw;
			}
		}

		SetHook(true,(PVOID*)&_CreateWindowEx,CreateWindowEx_Hook);

		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&PipeInstanceProc, NULL , 0, NULL );
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&ReadClient, NULL, 0, NULL);
		CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&InitializeExtras, NULL , 0, NULL );

		CreateKeys();
		CreateMapData();
		break;

	case DLL_PROCESS_DETACH:

		DisconnectPipe();

		TerminateProcess(GetCurrentProcess(), 0);

		break;
	}
	return TRUE;
}

HWND WINAPI CreateWindowEx_Hook(unsigned long ulExStyle, const char* lpcszClassName, const char* lpcszWindowName, unsigned long ulStyle, int x, int y, int iWidth, int iHeight, HWND hWndParent, HMENU hMenu, HINSTANCE hInstance, void* lpvParam)
{
	if (!strcmp(lpcszClassName, "StartUpDlgClass"))
	{
		printf("Blocked StartUpDlgClass\n");
		return NULL;
	}
	else if (!strcmp(lpcszClassName, "NexonADBallon"))
	{
		printf("Blocked NexonADBallon\n");
		return NULL;
	}
	else if (!strcmp(lpcszClassName, "MapleStoryClass"))
	{
		hMaple = _CreateWindowEx(ulExStyle, lpcszClassName, lpcszWindowName, ulStyle, x, y, iWidth, iHeight, hWndParent, hMenu, hInstance, lpvParam);

		printf("Hooked handle\n");

		return hMaple;
	}
	return _CreateWindowEx(ulExStyle, lpcszClassName, lpcszWindowName, ulStyle, x, y, iWidth, iHeight, hWndParent, hMenu, hInstance, lpvParam);
}

// the hooked input create.

HRESULT HookDirectInput8Create(HINSTANCE hinst, DWORD dwVersion, REFIID riidltf, LPVOID *ppvOut, LPUNKNOWN punkOuter)
{
    HRESULT device = oldDirectInput8Create(hinst, dwVersion, riidltf, ppvOut, punkOuter);
 
    DWORD* ppvtbl = (DWORD*)*ppvOut;
    DWORD* pvtbl = (DWORD*) *ppvtbl;
 
    DWORD dwOldProtect, dwNewProtect;
    MEMORY_BASIC_INFORMATION mbi;
 
    VirtualQuery((void*)pvtbl, &mbi, sizeof(mbi));
    dwNewProtect = mbi.Protect;
    dwNewProtect &= ~(PAGE_READONLY | PAGE_EXECUTE_READ | PAGE_EXECUTE);
    dwNewProtect |= (PAGE_READWRITE);
    if ( !VirtualProtect((void*)pvtbl, sizeof(PVOID)*5, dwNewProtect, &dwOldProtect))
        return false;
 
    DWORD *pCreateDevice = MakePointer(pvtbl, CREATEDEVICE, DWORD*);
    oldCreateDevice = (CreateDevice_T) pCreateDevice;
    *pCreateDevice = (DWORD)HookCreateDevice;
 
    VirtualProtect((void*)pvtbl, sizeof(PVOID), dwOldProtect, NULL);
    return device;
}
 
// the hooked load library, unhooks as soon as dinput loads.

HMODULE WINAPI HookLoadLibraryA ( __in LPCTSTR lpFileName )
{
    HMODULE hModule = (HMODULE) oldLoadLibraryA(lpFileName);
    if ( hModule != NULL ) {        
        if ( !stricmp(lpFileName, "dinput8.dll") ) {
            DWORD addy = (DWORD) GetProcAddress(hModule,"DirectInput8Create");
            oldDirectInput8Create = (DirectInput8Create_T) DetourFunction(&addy, (DWORD*) &HookDirectInput8Create, 5); 
            DetourFunction((DWORD*) HookLoadLibraryA, (DWORD*) &oldLoadLibraryA, 5);
        }
    }
    return hModule;
}
 
HRESULT HookCreateDevice(LPVOID *plpvOut, REFGUID rguid, LPDIRECTINPUTDEVICE *lplpDirectInputDevice, LPUNKNOWN pUnkOuter)
{
    HRESULT device = oldCreateDevice(plpvOut, rguid, lplpDirectInputDevice, pUnkOuter);
    DWORD* ppvtbl = (DWORD*)*lplpDirectInputDevice;
    DWORD* pvtbl = (DWORD*) *ppvtbl;
 
    DWORD dwOldProtect, dwNewProtect;
    MEMORY_BASIC_INFORMATION mbi; 
 
    VirtualQuery((void*)pvtbl, &mbi, sizeof(mbi));
    dwNewProtect = mbi.Protect;
    dwNewProtect &= ~(PAGE_READONLY | PAGE_EXECUTE_READ | PAGE_EXECUTE);
    dwNewProtect |= (PAGE_READWRITE);
    if ( !VirtualProtect((void*)pvtbl, sizeof(PVOID)*5, dwNewProtect, &dwOldProtect))
        return false;
 
    DWORD *pGetDeviceState = MakePointer(pvtbl, GETDEVICESTATE, DWORD*);
    oldGetDeviceState = (GetDeviceState_T) pGetDeviceState;
    *pGetDeviceState = (DWORD) HookGetDeviceState;
 
    VirtualProtect((void*)pvtbl, sizeof(PVOID), dwOldProtect, NULL);
    return device;
}
 
HRESULT HookGetDeviceState(LPVOID *plpvOut, DWORD cbData, LPVOID lpvData)
{
    BYTE* keys;
    HRESULT hr = oldGetDeviceState(plpvOut,cbData,lpvData);
    keys = (BYTE*) lpvData;
    // Inject keypresses here by modifying lpv data.
    keys[DIK_LEFT] = 0x80;
    return hr;
}
 
DWORD DetourFunction(DWORD *address, DWORD *jmpAddress, int len)
{
    DWORD* jmp = (DWORD*)malloc(len+5);
    DWORD dwOldProtect;
    VirtualProtect(address, len, PAGE_READWRITE, &dwOldProtect);
    memcpy(jmp, address, len); jmp += len;
    jmp[0] = 0xE9;
    *(DWORD*)(jmp+1) = (DWORD)(address+len - jmp) - 5;
    address[0] = 0xE9;
    *(DWORD*)(address+1) = (DWORD)(jmpAddress - address) - 5;
    VirtualProtect(address, len, dwOldProtect, NULL);
    return (DWORD) (jmp-len);
}