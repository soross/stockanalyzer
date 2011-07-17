#include "StdAfx.h"
#include "ObjFileJudger.h"
#include <vector>

ObjFileJudger::ObjFileJudger(void)
{
}


ObjFileJudger::~ObjFileJudger(void)
{
}

bool ObjFileJudger::IsSystemFile(const string& fileName)
{
	if (fileName.find("MSVC") != string::npos)
	{
		return true;
	}

	if (fileName.find("msvc") != string::npos)
	{
		return true;
	}

	if (fileName.find("kernel") != string::npos)
	{
		return true;
	}

	if (fileName.find("shlwapi") != string::npos)
	{
		return true;
	}

	return false;
}
