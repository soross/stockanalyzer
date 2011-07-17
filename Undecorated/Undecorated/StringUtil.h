#pragma once

#include <string>
#include <vector>

using namespace std;

class StringUtil
{
public:
	StringUtil(void);
	~StringUtil(void);

	static vector<string>& split(const string &s, char delim, vector<string> &elems);
	static vector<string> StringUtil::split(const string &s, char delim);

	static vector<string>& Split(const string &s, char delim, vector<string> &elems, bool removeEmptyElements);
	static vector<string> StringUtil::Split(const string &s, char delim, bool removeEmptyElements);

	static string& trim(string& s);
	static string& ltrim(string& s);
	static string& rtrim(string& s);

	static string& Trim(string& s);
	static string& TrimLeft(string& s);
	static string& TrimRight(string& s);

	static bool IsNullOrEmpty(const string& s);
};

