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
        internal static KeyCode ItemCategory = KeyCode.F2;
        internal static KeyCode UnlockDoors = KeyCode.F1;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;

        //players
        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = true;
        internal static bool DrawPlayerHealth = false;
        internal static bool DrawPlayerBox = true;
        internal static bool DrawPlayerLine = true;
        internal static bool DrawPlayerSkeleton = true;
        internal static float DrawPlayersDistance = 200f;


        //misc visuals
        internal static bool DrawLootItems = false;
        internal static float DrawLootItemsDistance = 300f;
        internal static float DrawLootableContainersDistance = 10f;
        internal static bool DrawLootableContainers = true;
        internal static bool DrawExfiltrationPoints = true;
        internal static bool DrawEmptyContainers = false;
        internal static bool DrawContainersContent = true;

        //aimbot
        internal static bool Aimbot = true;
        internal static float AimbotFOV = 10f;
        internal static int AimBotDistance = 300;
        internal static bool DrawAimbotPoint = true;
        internal static int AimBotBone = 133;

        //misc
        internal static bool DoorUnlocker = true;
        internal static bool MaxSkills = true;
        internal static bool NoVisor = true;
        internal static bool ThermalVison = false;

        //Weapon
        internal static bool NoRecoil = true;
        internal static bool DrawWeaponInfo = true;
    }
}