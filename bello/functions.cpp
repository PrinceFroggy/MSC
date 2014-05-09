#include "bot.h"
#include "functions.h"
#include "macros.h"
#include "rusher.h"

using namespace std;

#pragma region Functions

void DbgOutInt(string label, int value ) 
{
	stringstream strs;
	strs << value;
	label.append(strs.str()) ;
	const char *c_str =label.c_str() ;
	OutputDebugString( c_str ) ;
}

bool SetHook(__in BOOL bInstall, __inout PVOID* ppvTarget, __in PVOID pvDetour)
{
	if (DetourTransactionBegin() != NO_ERROR)
		return false;

	if (DetourUpdateThread(GetCurrentThread()) == NO_ERROR)
		if ((bInstall ? DetourAttach : DetourDetach)(ppvTarget, pvDetour) == NO_ERROR)
			if (DetourTransactionCommit() == NO_ERROR)
				return true;

	DetourTransactionAbort();
	return false;
}

CString GetRegistry(LPCTSTR pszValueName)
{
    REGSAM Wow64Flag;
    #ifdef _WIN64
    Wow64Flag = KEY_WOW64_32KEY;
    #else
    Wow64Flag = 0;
    #endif

    HKEY hKey = NULL;

    LONG lResult = RegOpenKeyEx(HKEY_LOCAL_MACHINE, _T("SOFTWARE\\MSC"), 0, KEY_QUERY_VALUE | Wow64Flag, &hKey);
    if (lResult != ERROR_SUCCESS)
    {
        SetLastError(lResult);
        AtlThrowLastWin32();
    }

    DWORD cbValueLength;

    lResult = RegQueryValueEx(hKey, pszValueName, NULL, NULL, NULL, &cbValueLength);
	if (lResult != ERROR_SUCCESS)
    {
        RegCloseKey(hKey);
        SetLastError(lResult);
        AtlThrowLastWin32();
    }

    CString sValue;

    if ( cbValueLength > 0 )
    {
        std::vector<TCHAR> szValue((cbValueLength / sizeof(TCHAR))+1);

        lResult = RegQueryValueEx(hKey, pszValueName, NULL, NULL, reinterpret_cast<LPBYTE>(&szValue[0]), &cbValueLength);
        if (lResult != ERROR_SUCCESS)
		{
            RegCloseKey(hKey);
            SetLastError(lResult);
            AtlThrowLastWin32();
        }

        szValue[cbValueLength / sizeof(TCHAR)] = 0;
        sValue = &szValue[0];
    }

    RegCloseKey(hKey);

    return sValue;
}

void CreateKeys()
{
	initializeKeys();
}

void CreateMapData()
{
	CreatePortalStructure();
	CreateRopeStructure();
}

void AssignMacroConfig()
{
	AssignConfig();
}

void AssignRusher(bool rusher)
{
	ToggleRusher(rusher);
}

DWORD ReadPointer(DWORD dwBase, DWORD dwOffset)
{
	__try
	{
		return *(PDWORD)(*(PDWORD)dwBase + dwOffset);
	}
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		return 0;
	}
}

bool WritePointer(DWORD dwBase, DWORD dwOffset, DWORD dwValue)
{
	__try
	{
		*(PDWORD)(*(PDWORD)dwBase + dwOffset) = dwValue;
		return true;
	}
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		return false;
	}
}

DWORD GetValue(DWORD dwBase, DWORD dwOffset)
{
	__try
	{
		return *(PDWORD)(dwBase + dwOffset);
	}
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		return 0;
	}
}

HWND GetHandle()
{
	return FindWindowA("MapleStoryClass", NULL);
}

void PressKey(HWND hWnd, unsigned int key)
{
	LPARAM lparam = (MapVirtualKey(key, 0) << 16) + 1;
	PostMessage(hWnd, WM_KEYDOWN, key, lparam);
	PostMessage(hWnd, WM_KEYUP, key, NULL);
}

#pragma endregion