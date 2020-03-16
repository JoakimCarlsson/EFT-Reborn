using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Visual;
using UnityEngine;

namespace EFT.HideOut
{
    class Settings
    {
        internal static bool DrawLootItems = false;
        internal static KeyCode ItemCategory = KeyCode.Keypad5;

        internal static bool DrawLootableContainers = false;
        internal static bool DrawExfiltrationPoints = true;

        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = true;
        internal static bool DrawPlayerHealth = false;
        internal static bool DrawPlayerBox = true;
        internal static bool DrawPlayerLine = true;

        internal static float DrawLootItemsDistance = 300f;
        internal static float DrawLootableContainersDistance = 10f;
        internal static float DrawPlayersDistance = 200f;

        internal static bool Aimbot = true;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;
        internal static float AimbotFOV = 10f;
        internal static bool NoRecoil = true;
        internal static KeyCode UnlockDoors = KeyCode.Keypad4;
        
        internal static bool MaxSkills = true;
        internal static bool NoVisor = true;
        internal static bool NoSway = true;
        internal static bool SuperBullet = true;
        internal static bool Teleport = true;
        internal static bool SpeedHack = true;
        internal static float SpeedValue = 2f;
        internal static bool FullBright = false;
    }
}