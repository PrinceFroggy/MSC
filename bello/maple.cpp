#include "basic.h"
#include "functions.h"
#include "initialize.h"
#include "maple.h"
#include "memory.h"
#include "pointers.h"

HANDLE thread;

template <typename T>
struct ZXString
{
	T* _m_pStr;
};

typedef void (__cdecl* CHATLOG_ADD_t)(ZXString<char> *sString, unsigned short nType);
CHATLOG_ADD_t CHATLOG_ADD = reinterpret_cast<CHATLOG_ADD_t>(msCHATLOG_ADD);

void ShowMessage(std::string const& strMessage, MessageType nType)
{
	ZXString<char> str;
	str._m_pStr = const_cast<char*>(strMessage.c_str());
	CHATLOG_ADD(&str, static_cast<unsigned short>(nType));
}