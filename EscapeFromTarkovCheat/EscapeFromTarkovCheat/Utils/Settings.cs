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
        //Keybinds
        internal static KeyCode ItemCategory = KeyCode.Keypad5;
        internal static KeyCode UnlockDoors = KeyCode.Keypad4;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;

        //players
        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = true;
        internal static bool DrawPlayerHealth = false;
        internal static bool DrawPlayerBox = true;
        internal static bool DrawPlayerLine = true;
        internal static bool DrawPlayerSkeleton = false;

        //misc visuals
        internal static bool DrawLootItems = false;
        internal static float DrawLootItemsDistance = 300f;
        internal static float DrawLootableContainersDistance = 10f;
        internal static float DrawPlayersDistance = 200f;
        internal static bool DrawLootableContainers = false;
        internal static bool DrawExfiltrationPoints = true;

        //aimboot
        internal static bool Aimbot = true;
        internal static float AimbotFOV = 10f;
        internal static bool NoRecoil = true;


        //misc
        internal static bool DoorUnlocker = true;
        internal static bool MaxSkills = true;
        internal static bool NoVisor = true;
        internal static bool SpeedHack = true;
        internal static float SpeedValue = 2f;

        //Weapon
        internal static bool DrawWeaponInfo = true;
        internal static bool NoSway = true;
        internal static bool SuperBullet = true;
        internal static bool FastReload = true;
        internal static bool AlwaysAutomatic = true;
        internal static bool FireRate = true;
        internal static int FireRateValue = 1000;
    }
}