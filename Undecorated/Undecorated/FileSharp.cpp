#include "StdAfx.h"
#include "FileSharp.h"
#include <iostream>
#include <fstream>

FileSharp::FileSharp(void)
{
}


FileSharp::~FileSharp(void)
{
}

bool FileSharp::Exists(string fileName)
{
	ifstream file(fileName);
	if (!file)
	{
		// Can't open file
		return false;
	}
	else
	{
		file.close();
		return true;
	}
}
