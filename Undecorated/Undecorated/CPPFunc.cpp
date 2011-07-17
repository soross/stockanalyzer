#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include "CPPFunc.h"

#define MAX_PARAMS 32
#define MAX_NAME   512

#define ZEROMEM(x) memset(&x,0,sizeof(x))
#define MATCH_TYPE(t,x) \
    if (*szBuffer == #@t) \
    { \
        strcat(type->rgchName,#x); \
        szBuffer++; \
        return szBuffer; \
    }

char* CPPFunc::GetName(char *szBuffer)
{
    char* p;

    p = rgchName;
    while(*szBuffer)
    {
        if (*szBuffer == '@')
        {
            if (*(szBuffer+1) && *(szBuffer+1)=='@')
            {
                *p = 0;
                return szBuffer+2;
            }
            else
            {
                *p=0;
                p = rgchClass;
                szBuffer++;
            }
        }
        else
            *p++ = *szBuffer++;
    }
    return 0;
}

char* CPPFunc::GetType(char *szBuffer,TYPE* type)
{
    bool bModifiers = true;
    int nUserTypeIndex,z;
    char *p;

    ZEROMEM(*type);

    if (isdigit(*szBuffer))
    {
        memcpy(type,&rgtypeParam[atoi(szBuffer)+1],sizeof(*type));
        while(isdigit(*szBuffer)) szBuffer++;
        return szBuffer+1;
    }
    while(bModifiers)
    {
        bModifiers = false;
        if (strncmp(szBuffer,"PA",2)==0)
        {
            strcat(type->rgchModifiers,"*");
            szBuffer+=2;
            bModifiers = true;
        }
        if (strncmp(szBuffer,"AA",2)==0)
        {
            strcat(type->rgchModifiers,"&");
            szBuffer+=2;
            bModifiers = true;
        }
        if (strncmp(szBuffer,"PB",2)==0)
        {
            strcat(type->rgchModifiers,"const *");
            szBuffer+=2;
            bModifiers = true;
        }
        if (strncmp(szBuffer,"QA",2)==0)
        {
            strcat(type->rgchModifiers,"*const");
            szBuffer+=2;
            bModifiers = true;
        }
        if (strncmp(szBuffer,"AB",2)==0)
        {
            strcat(type->rgchModifiers,"const&");
            szBuffer+=2;
            bModifiers = true;
        }
    }
    if (strncmp(szBuffer,"P6",2)==0)
    {
        // Function pointer
        szBuffer+=2;
        type->bFuncPointer = true;
        type->pFunc = new CPPFunc;
        type->pFunc->Init();
        szBuffer = type->pFunc->ParseParams(szBuffer);
        if (!szBuffer)
        {
            delete type->pFunc;
            type->pFunc = NULL;
            return NULL;
        }
        return szBuffer;
    }
    MATCH_TYPE(D,char);
    MATCH_TYPE(X,void);
    MATCH_TYPE(F,short);
    MATCH_TYPE(J,long);
    MATCH_TYPE(H,int);
    MATCH_TYPE(M,float);
    MATCH_TYPE(N,double);
    MATCH_TYPE(O,long double);
    MATCH_TYPE(E,unsigned char);
    MATCH_TYPE(G,unsigned short);
    MATCH_TYPE(I,unsigned int);
    MATCH_TYPE(K,unsigned long);
    if (*szBuffer == '_')
    {
        szBuffer++;
        MATCH_TYPE(J,__int64);
        MATCH_TYPE(F,__int16);
        MATCH_TYPE(H,__int32);
        MATCH_TYPE(D,__int8);
    }

    if (*szBuffer == '?')
    {
        szBuffer++;
        p = type->rgchName;
        while(strncmp(szBuffer,"@@",2))
        {
            *p++=*szBuffer++;
        }
        *p=0;
        szBuffer+=2;
        return szBuffer;
    }
    if (*szBuffer == 'V')
    {
        szBuffer++;
        if (isdigit(*szBuffer))
        {
            strcpy(type->rgchName,rgchClass);
            szBuffer++;
        }
        else
        {
            type->bUser = true;
            p = type->rgchName;
            while(strncmp(szBuffer,"@@",2))
            {
                *p++=*szBuffer++;
            }
            *p=0;
            szBuffer+=2;
        }
        return szBuffer;


    }
    if (*szBuffer == 'U')
    {
        szBuffer++;

        if (isdigit(*szBuffer))
        {
            nUserTypeIndex = atoi(szBuffer);
            for(z=0;z<nParams;z++)
                if (rgtypeParam[z].bUser)
                {
                    nUserTypeIndex--;
                    if (!nUserTypeIndex)
                        break;
                }

            if (z>=nParams)
                return NULL;


            
            strcat(type->rgchName,rgtypeParam[z].rgchName);
            while(isdigit(*szBuffer)) szBuffer++;
            szBuffer++;

        }
        else
        {
            type->bUser = true;
            p = type->rgchName;
            while(strncmp(szBuffer,"@@",2))
            {
                *p++=*szBuffer++;
            }
            *p=0;
            szBuffer+=2;
        }

        return szBuffer;
    }
    return NULL;
}

void CPPFunc::Init()
{
    ZEROMEM(rgchName);
    ZEROMEM(rgchClass);
    ZEROMEM(rgtypeParam);
    bThisPointer = false;
    nVisibility = VIS_PUBLIC;
    nParams = 0;
}

char* CPPFunc::ParseParams(char *szParams)
{
    TYPE type;
    if (*szParams == 'E')
    {
        bThisPointer = true;
        szParams++;
    }

    while(*szParams!='Z' && *szParams!='@')
    {
        szParams = GetType(szParams,&type);
        if (!szParams)
            return NULL;
        rgtypeParam[nParams++]=type;
    }
    return szParams;
}

VISIBILITY CPPFunc::GetVisibility()
{
    return nVisibility;
}

char* CPPFunc::GetClassName()
{
    return rgchClass;
}

char* CPPFunc::GetDecoratedName()
{
    return rgchDecoratedName;
}

char* CPPFunc::GetName()
{
    return rgchName;
}

int CPPFunc::GetNumberOfParams()
{
    return nParams;
}

char* CPPFunc::GetParamName(int nParam)
{
    return rgtypeParam[nParam].rgchName;
}

bool CPPFunc::ParseName(char *szName)
{
    Init();
    strcpy(rgchDecoratedName,szName);
    if (*szName == '?')
        szName++;
    else
    {
        strcpy(rgchName,szName);
        return true;
    }

    szName = GetName(szName);
    switch(*szName)
    {
        case 'A':nVisibility = VIS_PRIVATE;break; 
        case 'I':nVisibility = VIS_PROTECTED;break; 
        case 'Q':nVisibility = VIS_PUBLIC;break;
    }
    szName +=2;
    return ParseParams(szName)!=NULL;
}

void CPPFunc::PrintAsPointer(char *szBuffer, char *szNameModifier)
{
    int z;
    char buf[MAX_NAME];

    sprintf(buf,"%s%s (*%s%s)(",rgtypeParam[0].rgchName,rgtypeParam[0].rgchModifiers,szNameModifier,rgchName);
    strcat(szBuffer,buf);
    for(z=1;z<nParams;z++)
    {
        strcat(szBuffer,rgtypeParam[z].rgchName);
        strcat(szBuffer," ");
        strcat(szBuffer,rgtypeParam[z].rgchModifiers);
        if (z+1<nParams)
            strcat(szBuffer,",");
    }
    strcat(szBuffer,")");
}

void CPPFunc::PrintName(char *szBuffer, bool bPrintClass)
{
    int z;
    char buf[MAX_NAME];

    if (bPrintClass)
        sprintf(szBuffer,"%s%s %s::%s(",rgtypeParam[0].rgchName,rgtypeParam[0].rgchModifiers,rgchClass,rgchName);
    else
        sprintf(szBuffer,"%s%s %s(",rgtypeParam[0].rgchName,rgtypeParam[0].rgchModifiers,rgchName);
    for(z=1;z<nParams;z++)
    {
        if (rgtypeParam[z].bFuncPointer)
        {
            rgtypeParam[z].pFunc->PrintAsPointer(szBuffer,"");
        }
        else
        {
            strcat(szBuffer,rgtypeParam[z].rgchName);
            strcat(szBuffer," ");
            strcat(szBuffer,rgtypeParam[z].rgchModifiers);
            sprintf(buf," param%d",z);
            strcat(szBuffer,buf);
        }
        if (z+1<nParams)
            strcat(szBuffer,",");
    }
    strcat(szBuffer,")");
}
