#pragma once

#include <string>

using namespace std;

class FileSharp
{
public:
	FileSharp(void);
	~FileSharp(void);

	static bool Exists(string fileName);
};

