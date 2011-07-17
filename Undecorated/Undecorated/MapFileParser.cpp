#include "StdAfx.h"
#include "MapFileParser.h"
#include "FileSharp.h"
#include "StringUtil.h"
#include "ObjFileJudger.h"
#include <iostream>
#include <fstream>
#include <algorithm>

MapFileParser::MapFileParser(void)
{
}


MapFileParser::~MapFileParser(void)
{
}

void MapFileParser::Parse(string mapFile)
{
	if (!FileSharp::Exists(mapFile))
	{
		cout << "File " << mapFile << " does not exist!" << endl;
		return;
	}

	ifstream ifs(mapFile);
	if (ifs.bad())
	{
		cout << "Cannot open file for read: " << mapFile << "!" << endl;
		return ;
	}

	bool isFunctionLines = false;
	string line;
	while(getline(ifs, line ))
	{
		if (StringUtil::IsNullOrEmpty(line))
		{
			continue;
		}
		
		if (!isFunctionLines && (line.find("Rva+Base") != string::npos))
		{
			isFunctionLines = true;
			continue;
		}

		if (isFunctionLines)
		{
			ParseLine(line);
		}
	}

	ifs.close();
}

void MapFileParser::ParseLine(const string& line)
{
	string newline(line);
	newline = StringUtil::ltrim(newline);

	if (StringUtil::IsNullOrEmpty(newline))
	{
		return;
	}

	vector<string> arr;
	StringUtil::Split(newline, ' ', arr, true);

	if (arr.size() <= 3)
	{
		return;
	}

	string decoratedName = arr.at(1);
	string objName = arr.at(arr.size() - 1);

	if (!ObjFileJudger::IsSystemFile(objName))
	{
		DecoratedNames.push_back(decoratedName);
		ObjNames.push_back(objName);
	}	
}

bool MapFileParser::FindDecoratedFunction(const string& funcName) const
{
	return (find(DecoratedNames.begin(), DecoratedNames.end(), funcName) != DecoratedNames.end());
}

bool MapFileParser::FindObjFile(const string& fileName) const
{
	return (find(ObjNames.begin(), ObjNames.end(), fileName) != ObjNames.end());
}

vector<DifferentFunction> MapFileParser::FindNoExist(const MapFileParser& parser2) const
{
	vector<DifferentFunction> arr;
	
	int totalCount = DecoratedNames.size();
	for (int i = 0; i < totalCount; i++)
	{
		const string& str = DecoratedNames.at(i);
		const string& objName = ObjNames.at(i);
		if (!parser2.FindDecoratedFunction(str))
		{
			DifferentFunction diffFunc;
			diffFunc.FuncDecoratedName = str;
			diffFunc.ObjFileName = objName;

			arr.push_back(diffFunc);
		}
	}

	return arr;
}

const vector<string>& MapFileParser::AllDecoratedFuncs()
{
	return DecoratedNames;
}
