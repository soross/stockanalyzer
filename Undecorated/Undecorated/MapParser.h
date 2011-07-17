#pragma once

#include <string>

using namespace std;

class MapParser
{
public:
	MapParser(void);
	~MapParser(void);

	static void Parse(string mapfile1, string mapfile2, string outFile);
};

