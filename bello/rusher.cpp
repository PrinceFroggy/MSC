#define _CRT_SECURE_NO_WARNINGS 1
#include "functions.h"
#include "initialize.h"
#include "hacks.h"
#include "memory.h"
#include "rusher.h"
#include "resource.h"

string rmap;

HANDLE rthread;

int rmapID;

map <int, MapData> MsMap;
vector <PortalData> Temp1;

BOOL rOnlyOnce = FALSE;

void CreatePortalStructure()
{
	try
	{
		MapPair NewMap;
		string TempString;
		BOOL SkipReadLine = FALSE;

		HRSRC hRes = FindResource(hDLL, MAKEINTRESOURCE(IDR_TEXT1), _T("TEXT"));
		DWORD dwSize = SizeofResource(hDLL, hRes);
		HGLOBAL hGlob = LoadResource(hDLL, hRes);
		const CHAR* pData = reinterpret_cast<const CHAR*>(::LockResource(hGlob));

		std::istringstream File(pData);

		while (File.good())
		{
			MapData NewMapData;
			PortalData NewPortalData;

			if (!SkipReadLine)
				getline (File, TempString);

			NewMapData.MapID = atoi (TempString.substr (1, TempString.length() - 1).c_str());

			if (SkipReadLine)
			{
				SkipReadLine = FALSE;
				getline (File, TempString);
			}

			File >> TempString;

			if (!strcmp (TempString.substr (0, 1).c_str(), "["))
			{
				SkipReadLine = TRUE;
				NewMap = MapPair (NewMapData.MapID, NewMapData);
				MsMap.insert (NewMap);
			}
			else 
			{
				int NumMaps = atoi (TempString.substr (6, TempString.length() - 1).c_str());
				for (int i = 1; i <= NumMaps; i++)
				{
					File >> NewPortalData.PortalName;
					File >> NewPortalData.PortalType;
					File >> NewPortalData.X;
					File >> NewPortalData.Y;
					File >> NewPortalData.ToMapID;
					NewMapData.Portals.push_back (NewPortalData);
				}

				getline (File, TempString);
				NewMap = MapPair (NewMapData.MapID, NewMapData);
				MsMap.insert (NewMap);
			}
		}
	}
	catch (...)
	{

	}
}

void ToggleRusher(bool b)
{
	if (b)
	{
		GetRM();

		TogglePGM(true);
		ToggleSpawnControl(true);

		rthread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)RushToDestByID, NULL , 0, NULL );
	}
	else
	{
		TogglePGM(false);
		ToggleSpawnControl(false);

		TerminateThread(rthread, 0);
		CloseHandle(rthread);
	}
}

void GetRM()
{
	rmap = GetRegistry(_T("ru"));

	rmapID = atoi(rmap.c_str());
}

void RushToDestByID()
{
	int Rslt;

	Rslt = MapRush (rmapID);

	SendToPipe(RMESSAGE, Rslt);

	TogglePGM(false);
	ToggleSpawnControl(false);

	TerminateThread(rthread, 0);
	CloseHandle(rthread);
}

int MapRush (int DestMap)
{
	int tmapID = (int) ReadPointer(mapBase, mapId);

	if (tmapID == DestMap)
		return ERROR_SAME_MAP;

	int IsValid = CheckStartDest (tmapID, DestMap);
	if (IsValid != RUSH_VALID)
		return IsValid;

	int StartMap = tmapID;

	vector <MapMove> PortalsPath = FindMap (StartMap, DestMap);

	if (PortalsPath.size() == 0)
		return CANNOT_FIND_PATH;

	for (unsigned int i = 0; i < PortalsPath.size(); i++)
	{
		TeleportToMap (PortalsPath [i]);

		Sleep(1000);
	}

	PortalsPath.clear();
	Temp1.clear();
	rOnlyOnce = FALSE;

	return MAPRUSH_SUCCESS;
}

int CheckStartDest (int Start, int End)
{
	if (MsMap.find (Start) == MsMap.end())
	{
		return ERROR_INVALID_MAPS;
	}

	if (MsMap.find (End) == MsMap.end())
	{
		return ERROR_INVALID_MAPS;
	}

	return RUSH_VALID;
}

vector <MapMove> FindMap (int StartID, int EndID)
{
	vector <PortalData> TempList;
	GetNextMap (StartID, StartID, EndID, &TempList);

	TempList = Temp1;

	vector <MapMove> Maps;
	MapMove m;

	for (unsigned int i = 1; i < TempList.size() + 1; i++)
	{
		m.Special = FALSE;
		m.Counter = i;
		m.PN = TempList [i - 1].PortalName;
		m.XY.x = TempList [i - 1].X;
		m.XY.y = TempList [i - 1].Y;
		Maps.push_back (m);
	}

	TempList.clear();

	return Maps;
}

BOOL GetNextMap (int NextMap, int StartID, int DestinationID, vector <PortalData>* TempList)
{
	MapData CurrentMap = MsMap [NextMap];
	static int Counter = 0;

	if (CurrentMap.MapID == DestinationID) 
	{
		if (!rOnlyOnce)
		{
			Temp1 = *TempList;
			rOnlyOnce = TRUE;
		}

		if (Temp1.size() > (*TempList).size())
			Temp1 = *TempList;

		return FALSE;
	}

	if ((CurrentMap.Portals.size() <= 1 && CurrentMap.MapID != StartID) || Counter >= 300)
		return FALSE;

	int SizeList = TempList->size();
	for (int i = 0; i < SizeList - 1; i++)
		if ((*TempList) [i].ToMapID == CurrentMap.MapID || (*TempList) [i].ToMapID == StartID)
			return FALSE;

	for (unsigned int i = 0; i < CurrentMap.Portals.size(); i++)
	{
		TempList->push_back (CurrentMap.Portals [i]);
		Counter++;

		if (!GetNextMap (CurrentMap.Portals [i].ToMapID, StartID, DestinationID, TempList))
		{
			Counter--;
			TempList->pop_back();
		}
		else
			return TRUE;
	}

	return FALSE;
}

void TeleportToMap (MapMove m)
{
	SetTeleport(m.XY.x, m.XY.y - 4);

	Sleep(3000);
	
	PressKey(hMaple, 0x26);
}