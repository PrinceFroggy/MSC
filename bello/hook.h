#ifndef HOOK_H
#define HOOK_H

#include "basic.h"

// make a ptr

#define MakePointer(base, offset, cast) (cast) ( (DWORD) (base) + (DWORD) (offset) )

// vtable offsets

#define CREATEDEVICE 0x0C
#define GETDEVICESTATE 0x24
 
// function typedefs

typedef HWND (WINAPI* CreateWindowExPtr)(unsigned long ulExStyle, const char* lpcszClassName, const char* lpcszWindowName, unsigned long uStyle, int x, int y, int iWidth, int iHeight, HWND hWndParent, HMENU hMenu, HINSTANCE hInstance, void* lpvParam);

typedef HRESULT (*DirectInput8Create_T) (
    HINSTANCE hinst,
    DWORD dwVersion,
    REFIID riidltf,
    LPVOID * ppvOut,
    LPUNKNOWN punkOuter);

typedef HMODULE (WINAPI *LoadLibraryA_T) (
    __in  LPCTSTR lpFileName);

typedef HRESULT (*CreateDevice_T) (
    LPVOID *plpvOut,
    REFGUID rguid,
    LPDIRECTINPUTDEVICE *lplpDirectInputDevice,
    LPUNKNOWN pUnkOuter);

typedef HRESULT (*GetDeviceState_T) (
    LPVOID *plpvOut,
    DWORD cbData,
    LPVOID lpvData);
 
// old functions

DirectInput8Create_T oldDirectInput8Create;
LoadLibraryA_T oldLoadLibraryA;
CreateDevice_T oldCreateDevice;
GetDeviceState_T oldGetDeviceState;
 
// function prototypes

HWND WINAPI CreateWindowEx_Hook(unsigned long ulExStyle, const char* lpcszClassName, const char* lpcszWindowName, unsigned long ulStyle, int x, int y, int iWidth, int iHeight, HWND hWndParent, HMENU hMenu, HINSTANCE hInstance, void* lpvParam);

HRESULT HookDirectInput8Create(
    HINSTANCE hinst,
    DWORD dwVersion,
    REFIID riidltf,
    LPVOID *ppvOut,
    LPUNKNOWN punkOuter);

HMODULE WINAPI HookLoadLibraryA(
    __in  LPCTSTR lpFileName);

HRESULT HookCreateDevice(
    LPVOID *plpvOut,
    REFGUID rguid,
    LPDIRECTINPUTDEVICE *lplpDirectInputDevice,
    LPUNKNOWN pUnkOuter);

HRESULT HookGetDeviceState(
    LPVOID *plpvOut,
    DWORD cbData,
    LPVOID lpvData);

DWORD DetourFunction(
    DWORD *address,
    DWORD *jmpAddress,
    int len);

#endif