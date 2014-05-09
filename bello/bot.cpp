#define _CRT_SECURE_NO_WARNINGS 1
#include "bot.h"
#include "functions.h"
#include "initialize.h"
#include "hacks.h"
#include "memory.h"
#include "resource.h"

string lrmap;

HANDLE lrthread;

int lrmapID;

map <int, LRData> MsRope;
vector <LRopeData> Temp1;

BOOL lrOnlyOnce = FALSE;

void CreateRopeStructure()
{
	try
	{
		LRPair NewMap;
		string TempString;
		BOOL SkipReadLine = FALSE;

		HRSRC hRes = FindResource(hDLL, MAKEINTRESOURCE(IDR_TEXT2), _T("TEXT"));
		DWORD dwSize = SizeofResource(hDLL, hRes);
		HGLOBAL hGlob = LoadResource(hDLL, hRes);
		const CHAR* pData = reinterpret_cast<const CHAR*>(::LockResource(hGlob));

		std::istringstream File(pData);

		while (File.good())
		{
			LRData NewLRData;
			LRopeData NewLRopeData;

			if (!SkipReadLine)
				getline (File, TempString);

			NewLRData.LRID = atoi (TempString.substr (1, TempString.length() - 1).c_str());

			if (SkipReadLine)
			{
				SkipReadLine = FALSE;
				getline (File, TempString);
			}

			File >> TempString;

			if (!strcmp (TempString.substr (0, 1).c_str(), "["))
			{
				SkipReadLine = TRUE;
				NewMap = LRPair (NewLRData.LRID, NewLRData);
				MsRope.insert (NewMap);
			}
			else 
			{
				int NumLR = atoi (TempString.substr (6, TempString.length() - 1).c_str());
				for (int i = 1; i <= NumLR; i++)
				{
					File >> NewLRopeData.X;
					File >> NewLRopeData.Y1;
					File >> NewLRopeData.Y2;
					NewLRData.Ropes.push_back (NewLRopeData);
				}

				getline (File, TempString);
				NewMap = LRPair (NewLRData.LRID, NewLRData);
				MsRope.insert (NewMap);
			}
		}
	}
	catch (...)
	{

	}
}