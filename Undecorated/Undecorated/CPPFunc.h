#ifndef __CPPFUNC_H_
#define __CPPFUNC_H_

#define MAX_PARAMS 32
#define MAX_NAME   512

enum VISIBILITY
{
    VIS_PUBLIC,
    VIS_PRIVATE,
    VIS_PROTECTED,
};

class CPPFunc;

struct TYPE
{
    char        rgchName[MAX_NAME];
    char        rgchModifiers[MAX_NAME];
    bool        bUser;
    bool        bFuncPointer;
    CPPFunc*    pFunc;
};

class CPPFunc
{
    char        rgchDecoratedName[MAX_NAME];
    char        rgchName[MAX_NAME];
    char        rgchClass[MAX_NAME];
    TYPE        rgtypeParam[MAX_PARAMS];
    bool        bThisPointer;
    bool        bStatic;
    VISIBILITY  nVisibility;
    int         nParams;

    char*       GetName(char *szBuffer);
    char*       GetType(char *szBuffer,TYPE* type);
    void        Init();
    char*       ParseParams(char *szParams);
public:

    VISIBILITY  GetVisibility();
    char*       GetClassName();
    char*       GetDecoratedName();
    char*       GetName();
    int         GetNumberOfParams();
    char*       GetParamName(int nParam);
    bool        ParseName(char *szName);
    void        PrintAsPointer(char *szBuffer, char *szNameModifier);
    void        PrintName(char *szBuffer, bool bPrintClass);
};

#endif //__CPPFUNC_H_
