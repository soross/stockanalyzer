// Undecorated.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "CPPFunc.h"
#include <iostream>
#include <fstream>
#include <string>
#include "../Include/DbgHelp.h"
#include "MapParser.h"

using namespace std;

void ParseFunction(char* pStr)
{
	CPPFunc func;
	if (!func.ParseName(pStr))
	{
		cout << "Cannot parse " << pStr << endl;
		return;
	}

	char funcName[1000];

	func.PrintName(funcName, true);

	cout << funcName << endl;
}

void ParseFunctionDbgHelp(char* pStr)
{
	char funcName[1000];

	if (UnDecorateSymbolName(pStr, funcName, 
		1000, UNDNAME_COMPLETE))
	{
		// UnDecorateSymbolName returned success
		printf ("Symbol : %s\n", funcName);
	}
	else
	{
		// UnDecorateSymbolName failed
		DWORD error = GetLastError();
		printf("UnDecorateSymbolName returned error %d\n", error);
	}
}

string ParseFunctionByDbgHelp(const char* pStr)
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

void ParseTmpFile(const string& inFile, const string& outFile)
{
	cout << "Input file: " << inFile << endl;
	ifstream ifs(inFile);
	if (ifs.bad())
	{
		cout << "Cannot open file for read: " << inFile << "!" << endl;
		return ;
	}

	cout << "Output file: " << outFile << endl;
	ofstream ofs(outFile);
	if (ofs.bad())
	{
		cout << "Cannot open file for write: " << outFile << "!" << endl;
		return ;
	}

	string line;
	while(getline(ifs, line ))
	{
		string undecoratedFunction = ParseFunctionByDbgHelp(line.c_str());

		if (undecoratedFunction != "")
		{
			ofs << undecoratedFunction << endl;
		}
	}

	ifs.close();
	ofs.close();
}


int _tmain(int argc, _TCHAR* argv[])
{
	//ParseFunctionDbgHelp("?errorAv@CheckAutoVariables@@AAE_NPBVToken@@0@Z");
	//ParseFunctionDbgHelp("?varId@Token@@QBEIXZ");
	//ParseFunctionDbgHelp("?isArray@Variable@@QBE_NXZ");

	if (argc < 4)
	{
		cout << "argument less than 4. " << endl;
		return 0;
	}
	
	string mapFile1(argv[1]);
	string mapFile2(argv[2]);
	string outFile(argv[3]);
	MapParser::Parse(mapFile1, mapFile2, outFile);

	//ParseTmpFile(inFile, outFile);

	cout << "Press any key to continue..." << endl;
	char c;
	cin >> c;
	return 0;
}
