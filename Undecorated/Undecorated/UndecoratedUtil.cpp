#include "StdAfx.h"
#include "UndecoratedUtil.h"
#include "../Include/DbgHelp.h"

UndecoratedUtil::UndecoratedUtil(void)
{
}


UndecoratedUtil::~UndecoratedUtil(void)
{
}

string UndecoratedUtil::Und(const char* pStr)
{
	char funcName[1000];

	if (UnDecorateSymbolName(pStr, funcName, 
		1000, UNDNAME_COMPLETE))
	{
		// UnDecorateSymbolName returned success
		//printf ("Symbol : %s\n", funcName);
		string func(funcName);
		return func;
	}
	else
	{
		// UnDecorateSymbolName failed
		DWORD error = GetLastError();
		printf("UnDecorateSymbolName returned error %d\n", error);
		return "";
	}
}
