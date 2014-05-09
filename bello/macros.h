#ifndef MACROS_H
#define MACROS_H

#include "basic.h"

void initializeKeys();

void ReadValues();
void AssignValues();
void RetrieveKey();

BOOL CheckMethod(__in INT nKey);
BOOL KeyAvailable(__in INT nTemp);

void AssignConfig();

VOID autoAttack();
VOID autoLoot();
VOID autoHealth();
VOID autoMana();

#endif