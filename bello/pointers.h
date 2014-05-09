#ifndef POINTERS_H
#define POINTERS_H

#include "basic.h"

extern int characterx;
extern int charactery;

extern int mapID;

extern int monstercount;

extern int healthalert;
extern int health;

extern int manaalert;
extern int mana;

VOID InitializeExtras(VOID);

VOID UpdateCharacterX(VOID);
VOID UpdateCharacterY(VOID);

VOID UpdateMap(VOID);
VOID UpdateMapX(VOID);
VOID UpdateMapY(VOID);

VOID UpdateHealth(VOID);
VOID UpdateMana(VOID);

VOID UpdateMonsterCount(VOID);

VOID UpdateDebug(VOID);

bool leftWallLocation(int X);
bool topWallLocation(int Y);
bool rightWallLocation(int X);
bool bottomWallLocation(int Y);

#endif