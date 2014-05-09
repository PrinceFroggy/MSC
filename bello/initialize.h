#ifndef INITIALIZE_H
#define INITIALIZE_H

#include "basic.h"
#include "codes.h"

void __stdcall GetAnswerToRequest(__in char* szRequest);

__inline BOOL SendToPipe(__in BYTE bID, __in BYTE bSetting, BYTE b3 = 0,
						 BYTE b4 = 0, BYTE b5 = 0);
void __stdcall GetAnswerToRequest(__in char* szRequest);
DWORD ServerCreate();
void PipeInstanceProc();
DWORD ReadClient();
void DisconnectPipe();
void Terminate();

void __cdecl DebugPrintA(__in_z __format_string LPCSTR lpcszFormat, ...);

#endif