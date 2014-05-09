#ifndef FUNCTIONS_H
#define FUNCTIONS_H

#include "basic.h"

extern HMODULE hDLL;
extern HWND hMaple;

void DbgOutInt(std::string label, int value);

bool SetHook(__in BOOL bInstall, __inout PVOID* ppvTarget, __in PVOID pvDetour);

CString GetRegistry(LPCTSTR pszValueName);

void CreateKeys();
void CreateMapData();
void AssignMacroConfig();
void AssignRusher(bool rusher);

DWORD ReadPointer(DWORD dwBase, DWORD dwOffset);
bool WritePointer(DWORD dwBase, DWORD dwOffset, DWORD dwValue);
DWORD GetValue(DWORD dwBase, DWORD dwOffset);

void PressKey(HWND hWnd, unsigned int key);
void HoldKey(HWND hWnd, unsigned int key);

#endif