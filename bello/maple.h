#pragma once

#ifndef MAPLE_H
#define MAPLE_H

#include <windows.h>
#include <string>

enum MessageType
{
        White = 0,
        Green = 1,
        Lightpurple = 2,
        Orange = 3,
        Purple = 4,
        Lightgreen = 5,
        Special_PurpleGrey = 6,
        Grey = 7,
        Yellow = 8,
        Lightyellow = 9,
        Blue = 10,
        Special_BlackWhite = 11,
        Red = 12
};

void ShowMessage(std::string const& strMessage, MessageType nType);

#endif