#pragma once

#include <string>

using namespace std;

class ObjFileJudger
{
public:
	ObjFileJudger(void);
	~ObjFileJudger(void);

	static bool IsSystemFile(const string& fileName);
};

