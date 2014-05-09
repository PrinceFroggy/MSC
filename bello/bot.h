#ifndef BOT_H
#define BOT_H

#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")

#include "basic.h"

using namespace std;

struct LRopeSearch
{
	bool Special;
	int Counter;
	string PN;
	POINT XY;
};

struct LRopeData
{
	int X;
	int Y1;
	int Y2;
};

struct LRData
{
	int LRID;
	vector <LRopeData> Ropes;
};

typedef pair <int, LRData> LRPair;
extern map <int, LRData> LRMap;

void CreateRopeStructure();

#endif