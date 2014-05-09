#include "functions.h"
#include "macros.h"

using namespace std;

enum StringValue 
{ 
	evS1, evS2, evS3, evS4, evS5, evS6, evS7, evS8, evS9, evS10, evS11, evS12, evS13, evS14, evS15, evS16, evS17, evS18, evS19, evS20, evS21, evS22, evS23, evS24, evS25, evS26, evS27, evS28, evS29, evS30, evS31, evS32, evS33, evS34, evS35, evS36, evS37, evS38, evS39, evS40, evS41, evS42, evS43, evS44, evS45
};

std::map<std::string, StringValue> s_mapStringValues;

char Input[_MAX_PATH];

int TempLine;

int Temporary;

string trange;

string tattackKey;
string tlootKey;
string tjumpKey;
string thealthKey;
string tmanaKey;

string thealthValue;
string tmanaValue;

int keys [5];

#define attackKey 0
#define lootKey 1
#define jumpKey 2
#define healthKey 3
#define manaKey 4

void initializeKeys()
{
	try
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
	catch(...)
	{

	}
}

void AssignConfig()
{
	ReadValues();

	AssignValues();
}

void ReadValues()
{
	trange = GetRegistry(_T("ra"));
	tattackKey = GetRegistry(_T("ak"));
	tlootKey = GetRegistry(_T("lk"));
	tjumpKey = GetRegistry(_T("jk"));
	thealthValue = GetRegistry(_T("hv"));
	thealthKey = GetRegistry(_T("hk"));
	tmanaValue = GetRegistry(_T("mv"));
	tmanaKey = GetRegistry(_T("mk"));
}

void AssignValues()
{
	for (int i = 0; i < 5; i++)
	{
		switch(i)
		{
		
		case 0:

			TempLine = tattackKey.size();

			for (int a = 0; a <= TempLine; a++)
			{
				Input[a] = tattackKey[a];
			}

			RetrieveKey();

			keys[attackKey] = Temporary;

			break;

		case 1:

			TempLine = tlootKey.size();

			for (int a = 0; a <= TempLine; a++)
			{
				Input[a] = tlootKey[a];
			}

			RetrieveKey();

			keys[lootKey] = Temporary;

			break;

		case 2:
			
			TempLine = tjumpKey.size();

			for (int a = 0; a <= TempLine; a++)
			{
				Input[a] = tjumpKey[a];
			}

			RetrieveKey();

			keys[jumpKey] = Temporary;

			break;

		case 3:

			TempLine = thealthKey.size();

			for (int a = 0; a <= TempLine; a++)
			{
				Input[a] = thealthKey[a];
			}

			RetrieveKey();

			keys[healthKey] = Temporary;

			break;

		case 4:

			TempLine = tmanaKey.size();

			for (int a = 0; a <= TempLine; a++)
			{
				Input[a] = tmanaKey[a];
			}

			RetrieveKey();

			keys[manaKey] = Temporary;

			break;
		}
	}
}

void RetrieveKey()
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