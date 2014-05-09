#include "functions.h"
#include "initialize.h"
#include <winbase.h>

#pragma region Variables

HANDLE hPipe;
BOOL startread = FALSE;
char chRequest[4096]; 
char chReply[4096]; 
DWORD cbBytesRead, cbReplyBytes, cbWritten; 
CONST DWORD BUFSIZE = 4096;
BOOL loopstop = FALSE;
LPCTSTR lpszPipename = TEXT("\\\\.\\pipe\\MSC");

#pragma endregion

#pragma region Functions

void __cdecl DebugPrintA(__in_z __format_string LPCSTR lpcszFormat, ...) 
{ 
	va_list pArguments; 
	char szDebugBuffer[1024];
	va_start(pArguments, lpcszFormat); 
	if (SUCCEEDED(StringCchVPrintfA(szDebugBuffer, _countof(szDebugBuffer), lpcszFormat, pArguments))) 
		OutputDebugStringA(szDebugBuffer); 
	va_end(pArguments); 
}

void __stdcall GetAnswerToRequest(__in char* szRequest)
{
	switch (szRequest[0])
	{
	case BOT:
		if (szRequest[1]  == 1)
		{
			// enable bot
		}
		else
		{
			// disable bot
		}
		break;

	case CONFIG:
		if (szRequest[1]  == 1)
		{
			AssignMacroConfig();
		}
		break;

	case RUSH:
		if (szRequest[1]  == 1)
		{
			AssignRusher(true);
		}
		else
		{
			AssignRusher(false);
		}
		break;
	}
}

__inline BOOL SendToPipe(__in BYTE bID, __in BYTE bSetting, __in BYTE b3,
						 __in BYTE b4, __in BYTE b5)
{
	DWORD dwcbWritten;
	BYTE  cbData[5];

	cbData[0] = bID;
	cbData[1] = bSetting;
	cbData[2] = b3;
	cbData[3] = b4;
	cbData[4] = b5;
	return WriteFile(hPipe, (LPCVOID)cbData, sizeof(cbData), &dwcbWritten, NULL);
}

DWORD ServerCreate()
{
	hPipe = CreateNamedPipe(lpszPipename,
		PIPE_ACCESS_DUPLEX, 
		PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_NOWAIT, 
		PIPE_UNLIMITED_INSTANCES,
		BUFSIZE ,
		BUFSIZE ,
		0, 
		NULL);

	if(hPipe == INVALID_HANDLE_VALUE)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

void PipeInstanceProc()
{
	while(ServerCreate() == 1)
	{
		Sleep(1000);	
	}

	ConnectNamedPipe(hPipe, NULL);
	startread=TRUE;

	for(;;)
	{
		if(	ConnectNamedPipe(hPipe, NULL)==0)
		{
			if(GetLastError()==ERROR_NO_DATA)
			{
				DisconnectNamedPipe(hPipe);
				ConnectNamedPipe(hPipe, NULL);
			}
			Sleep(1000);
		}
	}
}

DWORD ReadClient()
{
	while(!loopstop)
	{
		if(startread)
		{
			while(ReadFile(hPipe, chRequest, BUFSIZE, &cbBytesRead, NULL) >0)
			{
				GetAnswerToRequest(chRequest); 
				Sleep(20); 
			}				
			Sleep(100);
			SendToPipe(0, 0);
		}
		else
		{
			Sleep(500);
		}
	}
	return 0;
}

void DisconnectPipe()
{
	DisconnectNamedPipe(hPipe);
	CloseHandle(hPipe);
}

#pragma endregion