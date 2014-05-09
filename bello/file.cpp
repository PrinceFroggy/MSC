#include "file.h"
#include "macros.h"
#include "values.h"
#include <map>
#include <string>
#include <fstream>

using namespace std;

#pragma region Global Variables

EXTERN_C IMAGE_DOS_HEADER __ImageBase;

static enum StringValue { evS1, evS2, evS3, evS4, evS5, evS6, evS7, evS8, evS9, evS10, evS11, evS12, evS13, evS14, evS15, evS16, evS17, evS18, evS19, evS20, evS21, evS22, evS23, evS24, evS25, evS26, evS27, evS28, evS29, evS30, evS31, evS32, evS33, evS34, evS35, evS36, evS37, evS38, evS39, evS40, evS41, evS42, evS43, evS44, evS45};

static std::map<std::string, StringValue> s_mapStringValues;

static char Input[_MAX_PATH];

string Line[5];

int Temporary;

int keys [3];

int attackDelay;

int healthValue;
int manaValue;

int rangeX;
int rangeY;

int kamiDelay;

#pragma endregion

#pragma region Functions

string retrievePath()
{
	LPTSTR strDLLPath1 = new TCHAR[_MAX_PATH];
	::GetModuleFileName((HINSTANCE)&__ImageBase, strDLLPath1, _MAX_PATH);
	string::size_type pos = string(strDLLPath1).find_last_of("\\/");
	return string(strDLLPath1).substr(0, pos);
}

void initializeKeys()
{
	s_mapStringValues["0"] = evS1;
	s_mapStringValues["1"] = evS2;
	s_mapStringValues["2"] = evS3;
	s_mapStringValues["3"] = evS4;
	s_mapStringValues["4"] = evS5;
	s_mapStringValues["5"] = evS6;
	s_mapStringValues["6"] = evS7;
	s_mapStringValues["7"] = evS8;
	s_mapStringValues["8"] = evS9;
	s_mapStringValues["9"] = evS10;

	s_mapStringValues["A"] = evS11;
	s_mapStringValues["B"] = evS12;
	s_mapStringValues["C"] = evS13;
	s_mapStringValues["D"] = evS14;
	s_mapStringValues["E"] = evS15;
	s_mapStringValues["F"] = evS16;
	s_mapStringValues["G"] = evS17;
	s_mapStringValues["H"] = evS18;
	s_mapStringValues["I"] = evS19;
	s_mapStringValues["J"] = evS20;

	s_mapStringValues["K"] = evS21;
	s_mapStringValues["L"] = evS22;
	s_mapStringValues["M"] = evS23;
	s_mapStringValues["N"] = evS24;
	s_mapStringValues["O"] = evS25;
	s_mapStringValues["P"] = evS26;
	s_mapStringValues["Q"] = evS27;
	s_mapStringValues["R"] = evS28;
	s_mapStringValues["S"] = evS29;
	s_mapStringValues["T"] = evS30;

	s_mapStringValues["U"] = evS31;
	s_mapStringValues["V"] = evS32;
	s_mapStringValues["W"] = evS33;
	s_mapStringValues["X"] = evS34;
	s_mapStringValues["Y"] = evS35;
	s_mapStringValues["Z"] = evS36;

	s_mapStringValues["SHIFT"] = evS37;
	s_mapStringValues["CTRL"] = evS38;
	s_mapStringValues["ALT"] = evS39;
	s_mapStringValues["PAGE UP"] = evS40;
	s_mapStringValues["PAGE DOWN"] = evS41;
	s_mapStringValues["END"] = evS42;
	s_mapStringValues["HOME"] = evS43;
	s_mapStringValues["INS"] = evS44;
	s_mapStringValues["DEL"] = evS45;
}

void mapKeys()
{
	switch(s_mapStringValues[Input])
    {
      case evS1:
		  Temporary = 0x30;
        break;
      case evS2:
		  Temporary = 0x31;
        break;
      case evS3:
		  Temporary = 0x32;
        break;
	  case evS4:
		  Temporary = 0x33;
        break;
      case evS5:
		  Temporary = 0x34;
        break;
      case evS6:
		  Temporary = 0x35;
        break;
	  case evS7:
		  Temporary = 0x36;
        break;
      case evS8:
		  Temporary = 0x37;
        break;
      case evS9:
		  Temporary = 0x38;
        break;
	  case evS10:
		  Temporary = 0x39;
        break;
      case evS11:
		  Temporary = 0x41;
        break;
      case evS12:
		  Temporary = 0x42;
        break;
	  case evS13:
		  Temporary = 0x43;
        break;
      case evS14:
		  Temporary = 0x44;
        break;
      case evS15:
		  Temporary = 0x45;
        break;
	  case evS16:
		  Temporary = 0x46;
        break;
      case evS17:
		  Temporary = 0x47;
        break;
      case evS18:
		  Temporary = 0x48;
        break;
	  case evS19:
		  Temporary = 0x49;
        break;
      case evS20:
		  Temporary = 0x4A;
        break;
      case evS21:
		  Temporary = 0x4B;
        break;
	  case evS22:
		  Temporary = 0x4C;
        break;
      case evS23:
		  Temporary = 0x4D;
        break;
      case evS24:
		  Temporary = 0x4E;
        break;
	  case evS25:
		  Temporary = 0x4F;
        break;
      case evS26:
		  Temporary = 0x50;
        break;
      case evS27:
		  Temporary = 0x51;
        break;
	  case evS28:
		  Temporary = 0x52;
        break;
      case evS29:
		  Temporary = 0x53;
        break;
      case evS30:
		  Temporary = 0x54;
        break;
	  case evS31:
		  Temporary = 0x55;
        break;
      case evS32:
		  Temporary = 0x56;
        break;
      case evS33:
		  Temporary = 0x57;
        break;
	  case evS34:
		  Temporary = 0x58;
        break;
      case evS35:
		  Temporary = 0x59;
        break;
      case evS36:
		  Temporary = 0x5A;
        break;
	  case evS37:
		  Temporary = 0x10;
        break;
      case evS38:
		  Temporary = 0x11;
        break;
      case evS39:
		  Temporary = 0x12;
        break;
	  case evS40:
		  Temporary = 0x21;
        break;
      case evS41:
		  Temporary = 0x22;
        break;
      case evS42:
		  Temporary = 0x23;
        break;
	  case evS43:
		  Temporary = 0x24;
        break;
      case evS44:
		  Temporary = 0x2D;
        break;
      case evS45:
		  Temporary = 0x2E;
        break;
      default:
        break;
    }
}

bool readFile()
{
	int lineCounter = 0;

	string configurationLocation = retrievePath() + "\\configuration";

	ifstream configurationFile;

	configurationFile.open(configurationLocation + ".config");

	if (!configurationFile)
	{
		return false;
	}
	else
	{
		while (lineCounter <= 5)
		{
			getline(configurationFile, Line[lineCounter]);

			lineCounter++;
		}
	}
	configurationFile.close();
	return true;
}

bool assignKeys()
{
	if (Line[0] != "attack: ;")
	{
		string Line0Temp = Line[0];

		string Line0 = Line[0].substr(Line[0].find(':') + 1);

		int Index0 = Line0.find(';');

		if (Index0 > 0)
		{
			Line0 = Line0.substr(0, Index0);
			attackDelay = atoi(Line0.c_str());
		}

		string Line00 = Line0Temp.substr(Line0Temp.find(';') + 1);

		int TempLine0 = Line00.size();

		for (int a = 0; a <= TempLine0; a++)
		{
			Input[a] = Line00[a];
		}

		mapKeys();

		keys[attackKey] = Temporary;
	}
	else
	{
		keys[attackKey] = NULL;
	}

	if (Line[2] != "health: ;")
	{
		string Line2Temp = Line[2];

		string Line2 = Line[2].substr(Line[2].find(':') + 1);

		int Index1 = Line2.find(';');

		if (Index1 > 0)
		{
			Line2 = Line2.substr(0, Index1);
			healthValue = atoi(Line2.c_str());
		}

		string Line22 = Line2Temp.substr(Line2Temp.find(';') + 1);

		int TempLine2 = Line22.size();

		for (int a = 0; a <= TempLine2; a++)
		{
			Input[a] = Line22[a];
		}

		mapKeys();

		keys[healthKey] = Temporary;
	}
	else
	{
		keys[healthKey] = NULL;
	}

	if (Line[3] != "mana: ;")
	{
		string Line3Temp = Line[3];

		string Line3 = Line[3].substr(Line[3].find(':') + 1);

		int Index2 = Line3.find(';');

		if (Index2 > 0)
		{
			Line3 = Line3.substr(0, Index2);
			manaValue = atoi(Line3.c_str());
		}

		string Line33 = Line3Temp.substr(Line3Temp.find(';') + 1);

		int TempLine3 = Line33.size();

		for (int a = 0; a <= TempLine3; a++)
		{
			Input[a] = Line33[a];
		}

		mapKeys();

		keys[manaKey] = Temporary;
	}
	else
	{
		keys[manaKey] = NULL;
	}

	if (keys[attackKey] != NULL || keys[healthKey] != NULL || keys[manaKey] != NULL)
	{
		return true;
	}
	else
	{
		return false;
	}
}

#pragma endregion