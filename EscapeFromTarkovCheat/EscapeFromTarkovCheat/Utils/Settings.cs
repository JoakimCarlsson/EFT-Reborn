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
        internal static KeyCode ItemCategory = KeyCode.F3;
        internal static KeyCode UnlockDoors = KeyCode.F8;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;

        //players
        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = true;
        internal static bool DrawPlayerHealthBar = false;
        internal static bool DrawPlayerHealth = false;
        internal static bool DrawPlayerBox = true;
        internal static bool DrawPlayerLine = true;
        internal static bool DrawPlayerSkeleton = true;
        internal static bool DrawPlayerWeapon = true;
        internal static bool DrawPlayerLevel = true;
        internal static float DrawPlayersDistance = 200f;
        internal static float DrawPlayerSkeletonDistance = 100f;
        internal static bool DrawCorpses = true;
        internal static bool DrawBots = true;
        internal static bool DrawBotsName = true;
        internal static bool DrawBotHealth = false;
        internal static bool DrawBotBox = true;
        internal static bool DrawBotHealthBar = false;
        internal static bool DrawBotLine = false;
        internal static bool DrawBotSkeleton = false;
        internal static float DrawBotSkeletonDistance = 100f;
        internal static float DrawBotDistance = 200f;


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
        internal static bool InfiniteStamina = true;
        internal static bool MaxSkills = false;
        internal static bool NoVisor = true;
        internal static bool ThermalVison = false;

        //Weapon
        internal static bool NoRecoil = true;
        internal static bool DrawWeaponInfo = true;
    }
}