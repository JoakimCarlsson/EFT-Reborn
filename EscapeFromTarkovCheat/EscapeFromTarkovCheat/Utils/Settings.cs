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

        //aimboot
        internal static bool Aimbot = true;
        public static int AimBotDistance = 300;
        internal static float AimbotFOV = 10f;

        //misc
        internal static bool DoorUnlocker = true;
        internal static bool MaxSkills = true;
        internal static bool NoVisor = true;
        internal static bool ThermalVison = false;
        internal static bool SpeedHack = true;
        internal static float SpeedValue = 1.3f;

        //Weapon
        internal static bool NoRecoil = true;
        internal static bool DrawWeaponInfo = true;
        internal static bool NoSway = true;
        internal static bool SuperBullet = false;
        internal static bool FastReload = false;
        internal static bool AlwaysAutomatic = false;
        internal static bool FireRate = false;
        internal static int FireRateValue = 1000;
    }
}