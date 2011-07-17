#include "StdAfx.h"
#include "StringUtil.h"
#include <iostream>
#include <sstream>
#include <algorithm>
#include <locale> 

StringUtil::StringUtil(void)
{
}

StringUtil::~StringUtil(void)
{
}

vector<string>& StringUtil::split(const string &s, char delim, vector<string> &elems) 
{
	stringstream ss(s);
	string item;
	while(getline(ss, item, delim)) 
	{
		elems.push_back(item);
	}
	return elems;
}

vector<string> StringUtil::split(const string &s, char delim) 
{
	vector<string> elems;
	return split(s, delim, elems);
}

vector<string>& StringUtil::Split(const string &s, char delim, vector<string> &elems, bool removeEmptyElements)
{
	stringstream ss(s);
	string item;
	while(getline(ss, item, delim)) 
	{
		if (item.length() != 0)
		{
			elems.push_back(item);
		}
	}
	return elems;
}

vector<string> StringUtil::Split(const string &s, char delim, bool removeEmptyElements)
{
	vector<string> elems;
	return Split(s, delim, elems, removeEmptyElements);
}

string& StringUtil::trim(string& s)
{
	return ltrim(rtrim(s));
}

string& StringUtil::ltrim(string& s)
{
	s.erase(s.begin(), find_if(s.begin(), s.end(), not1(ptr_fun<int, int>(isspace))));
	return s;
}

string& StringUtil::rtrim(string& s)
{
	s.erase(find_if(s.rbegin(), s.rend(), not1(ptr_fun<int, int>(isspace))).base(), s.end());
	return s;
}

string& StringUtil::Trim(string& s)
{
	return ltrim(rtrim(s));
}

string& StringUtil::TrimLeft(string& s)
{
	s.erase(s.begin(), find_if(s.begin(), s.end(), not1(ptr_fun<int, int>(isspace))));
	return s;
}

string& StringUtil::TrimRight(string& s)
{
	s.erase(find_if(s.rbegin(), s.rend(), not1(ptr_fun<int, int>(isspace))).base(), s.end());
	return s;
}

bool StringUtil::IsNullOrEmpty(const string& s)
{
	return (s.length() == 0);
}
