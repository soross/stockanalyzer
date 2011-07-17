#pragma once

#include <string>
#include <vector>

using namespace std;

struct DifferentFunction
{
	string FuncDecoratedName;
	string ObjFileName;
};

class MapFileParser
{
public:
	MapFileParser(void);
	~MapFileParser(void);

	void Parse(string mapFile);

	bool FindDecoratedFunction(const string& funcName) const;
	bool FindObjFile(const string& fileName) const;

	vector<DifferentFunction> FindNoExist(const MapFileParser& parser2) const;
	const vector<string>& AllDecoratedFuncs();

	string GetObjName(int pos) const
	{
		if ((pos < 0) || (pos >= ObjNames.size()))
		{
			return "";
		}

		return ObjNames.at(pos);
	}

private:
	void ParseLine(const string& line);

private:
	vector<string> DecoratedNames;
	vector<string> ObjNames;
};

