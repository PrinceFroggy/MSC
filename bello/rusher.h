#ifndef RUSHER_H
#define RUSHER_H

#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#define RUSH_VALID 1
#define MAPRUSH_SUCCESS 2
#define ERROR_SAME_MAP 3
#define ERROR_INVALID_MAPS 4
#define CANNOT_FIND_PATH 5

#include "basic.h"

using namespace std;

struct MapMove
{
	bool Special;
	int Counter;
	string PN;
	POINT XY;
};

struct PortalData
{
	int ToMapID;
	string PortalName;
	int X;
	int Y;
	int PortalType;
};

struct MapData
{
	int MapID;
	vector <PortalData> Portals;
};

typedef pair <int, MapData> MapPair;
extern map <int, MapData> MsMap;

void CreatePortalStructure();
void ToggleRusher(bool b);
void GetRM();
void RushToDestByID();
int MapRush (int DestMap);
int CheckStartDest (int Start, int End);
vector <MapMove> FindMap (int StartID, int EndID);
BOOL GetNextMap (int NextMap, int StartID, int DestinationID, vector <PortalData>* TempList);
void TeleportToMap (MapMove m);

#endif