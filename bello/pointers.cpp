#include "functions.h"
#include "initialize.h"
#include "memory.h"
#include "pointers.h"

#pragma region Variables

int characterx;
int charactery;

int mapID;

int monstercount;

int health;

int mana;

#pragma endregion

#pragma region Functions

VOID InitializeExtras()
{
	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateCharacterX, NULL , 0, NULL );
	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateCharacterY, NULL , 0, NULL );

	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateMap, NULL , 0, NULL );

	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateHealth, NULL , 0, NULL );
	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateMana, NULL , 0, NULL );

	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateMonsterCount, NULL , 0, NULL );

	CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)&UpdateDebug, NULL , 0, NULL );
}

VOID UpdateCharacterX()
{
	while(TRUE)
	{
		characterx = (int) ReadPointer(characterBase, characterX);
		Sleep(100);

		SendToPipe(CHARACTERX, (BYTE)characterx, (BYTE)(characterx >> 8), (BYTE)(characterx >> 16),
			(BYTE)(characterx >> 24));
		Sleep(500);
	}
}

VOID UpdateCharacterY()
{
	while(TRUE)
	{
		charactery = (int) ReadPointer(characterBase, characterY);
		Sleep(100);

		SendToPipe(CHARACTERY, (BYTE)charactery, (BYTE)(charactery >> 8), (BYTE)(charactery >> 16),
			(BYTE)(charactery >> 24));
		Sleep(500);
	}
}

VOID UpdateMap()
{
	while(TRUE)
	{
		mapID = (int) ReadPointer(mapBase, mapId);
		Sleep(100);

		SendToPipe(MAP, (BYTE)mapID, (BYTE)(mapID >> 8), (BYTE)(mapID >> 16),
			(BYTE)(mapID >> 24));
		Sleep(500);
	}
}

VOID UpdateHealth()
{
	while(TRUE)
	{
		if ((int) ReadPointer(alertBase, hpAlert) < 20)
		{
			WritePointer(alertBase, hpAlert, 20);
		}

		health = (int) ReadPointer(guiBase, guiHealth);

		Sleep(100);
	}
}

VOID UpdateMana()
{
	while(TRUE)
	{
		if ((int) ReadPointer(alertBase, mpAlert) < 20)
		{
			WritePointer(alertBase, mpAlert, 20);
		}

		mana = (int) ReadPointer(guiBase, guiMana);

		Sleep(100);
	}
}

VOID UpdateMonsterCount()
{
	while(TRUE)
	{
		monstercount = (int) ReadPointer(monsterBase, monsterCount);
		Sleep(100);

		SendToPipe(MONSTERCOUNT, (BYTE)monstercount, (BYTE)(monstercount >> 8), (BYTE)(monstercount >> 16),
			(BYTE)(monstercount >> 24));
		Sleep(500);
	}
}

VOID UpdateDebug()
{
	while(TRUE)
	{
		DbgOutInt("map= ", mapID);
		DbgOutInt("charX= ", characterx);
		DbgOutInt("charY= ", charactery);
		DbgOutInt("hp= ", health);
		DbgOutInt("mp= ", mana);

		Sleep(100);
	}
}

bool leftWallLocation(int X)
{
    return ((int)ReadPointer(wallBase, leftWall) <= X);
}

bool topWallLocation(int Y)
{
    return ((int)ReadPointer(wallBase, topWall) <= Y);
}

bool rightWallLocation(int X)
{
    return ((int)ReadPointer(wallBase, rightWall) >= X);
}

bool bottomWallLocation(int Y)
{
    return ((int)ReadPointer(wallBase, bottomWall) >= Y);
}

#pragma endregion