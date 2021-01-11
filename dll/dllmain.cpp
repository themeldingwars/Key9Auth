// Simple dll replacement to read login data from the launcher and pass it to the client
// Reads the values from a memory mapped file
#include "pch.h"
#include <stdio.h>
#include <string.h>
#include <windows.h>

#define ENABLE_LOGGING

#ifdef ENABLE_LOGGING
#include "ThirdParty/easylogging/easylogging++.h"
INITIALIZE_EASYLOGGINGPP
#define LOG(msg, ...) if (IsLoggingEnabled) { Logger->info(msg, __VA_ARGS__); }
el::Logger* Logger = el::Loggers::getLogger("default");
#else
#define LOG(msg, ...)
#endif

#include "ThirdParty/inih/INIReader.h"

#pragma comment(lib, "user32.lib")
#pragma warning(disable:4996)

#define DLL_EXPORT extern "C" __declspec(dllexport)

struct LauncherLoginToken
{
    int TokenLength;
    char Token[512];
    int UsernameLength;
    char Username[256];
};

bool IsLoggingEnabled       = false;
const char* defaultToken    = "testToken :>";
const char* defaultUsername = "TMW Deafult User";
const char* INIPath         = "../../firefall.ini";
TCHAR MemMapedFileName[]    = TEXT("TMW_LAUNCHER_LOGIN_DATA");
HANDLE MemMapedFile;
LPVOID MemMapedFileBuff;
LauncherLoginToken* LoginInfo;

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }

    return TRUE;
}

void LoadFromIni()
{
    INIReader reader(INIPath);

    if (reader.ParseError() != 0) {
#ifdef ENABLE_LOGGING
        Logger->info("Can't load '%v'", INIPath);
#endif
    }

    IsLoggingEnabled = reader.GetBoolean("key9", "enablelogging", false);
}

DLL_EXPORT int LoginToken_Init()
{
    LoadFromIni();

    MemMapedFile     = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, MemMapedFileName);
    if (MemMapedFile == NULL)
    {
        LOG("Error opening mem maped file D:");
        return 1;
    }

    MemMapedFileBuff = MapViewOfFile(MemMapedFile, FILE_MAP_ALL_ACCESS, 0, 0, 1000);
    if (MemMapedFileBuff == NULL)
    {
        LOG("Error reading from mem maped file D:");
        return 1;
    }

    LOG("Reading login data");
    LoginInfo = (LauncherLoginToken*)MemMapedFileBuff;
    LOG("Login data, Token: %v TokenLength: %v, Username Length: %v, Username: %v", LoginInfo->Token, LoginInfo->TokenLength, LoginInfo->UsernameLength, LoginInfo->Username);

    return 1;
}

DLL_EXPORT char LoginToken_UnInit()
{
    LOG("LoginToken_UnInit");
    return 1;
}

DLL_EXPORT int GetCurUserName(char* username, int length)
{
    LOG("GetCurUserName username Length: %v", length);
    if (MemMapedFileBuff != NULL)
    {
        memcpy(username, LoginInfo->Username, LoginInfo->UsernameLength);
        return LoginInfo->UsernameLength;
    }
    else
    {
        int strLen = strlen(defaultUsername);
        memcpy(username, defaultUsername, strLen);
        return strLen;
    }
}

DLL_EXPORT int GetCurToken(char* token, int length)
{
    LOG("GetCurToken length: %v", length);
    if (MemMapedFileBuff != NULL)
    {
        memcpy(token, LoginInfo->Token, LoginInfo->TokenLength);
        return LoginInfo->TokenLength;
    }
    else
    {
        int strLen = strlen(defaultToken);
        memcpy(token, defaultToken, strLen);
        return strLen;
    }
}

DLL_EXPORT char GetCurServerInfo(char* serverInfo)
{
    LOG("GetCurServerInfo");
    return 4;
}

DLL_EXPORT char GetServerInfo(char* serverInfo, int length)
{
    LOG("GetServerInfo");
    return 1;
}

DLL_EXPORT int GetUserTypeID()
{
    LOG("GetUserTypeID");
    return 100;
}

DLL_EXPORT int GetCurServerName(char* serverName, int length, char format)
{
    LOG("GetCurServerName");
    return 1;
}

DLL_EXPORT int GetServerCount()
{
    LOG("GetServerCount");
    return 1;
}