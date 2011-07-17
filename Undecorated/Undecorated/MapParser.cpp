#include "StdAfx.h"
#include "MapParser.h"
#include "MapFileParser.h"
#include "StringUtil.h"
#include "UndecoratedUtil.h"
#include <fstream>

MapParser::MapParser(void)
{
}


MapParser::~MapParser(void)
{
}

void MapParser::Parse(string mapfile1, string mapfile2, string outFile)
{
	MapFileParser parser1;
	parser1.Parse(mapfile1);

	MapFileParser parser2;
	parser2.Parse(mapfile2);

	vector<DifferentFunction> differentFuncs = parser1.FindNoExist(parser2);

	if (StringUtil::IsNullOrEmpty(outFile))
	{
		return;
	}

	cout << "Output file: " << outFile << endl;
	ofstream ofs(outFile);
	if (ofs.bad())
	{
		cout << "Cannot open file for write: " << outFile << "!" << endl;
		return ;
	}

	int totalCount = differentFuncs.size();
	for (int i = 0; i < totalCount; i++)
	{
		const DifferentFunction& func = differentFuncs.at(i);

		string undName = UndecoratedUtil::Und(func.FuncDecoratedName.c_str());
		if (undName != "")
		{
			ofs << undName << " in " << func.ObjFileName << endl;
		}
	}

	ofs.close();
}
