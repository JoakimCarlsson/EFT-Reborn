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
        internal static bool DrawPlayerDistance = true;
        internal static float DrawPlayersRange = 200f;
        internal static float DrawPlayerSkeletonDistance = 100f;
        internal static bool DrawPlayerCorpses = true;
        internal static bool DrawScavCorpses = true;
        internal static bool DrawScavs = true;
        internal static bool DrawScavName = true;
        internal static bool DrawScavWeapon = true;
        internal static bool DrawScavDistance = true;
        internal static bool DrawScavHealth = false;
        internal static bool DrawScavBox = true;
        internal static bool DrawScavHealthBar = false;
        internal static bool DrawScavLine = false;
        internal static bool DrawScavSkeleton = false;
        internal static float DrawScavSkeletonDistance = 100f;
        internal static float DrawScavRange = 200f;


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
        internal static bool DrawAimbotPoint = true;
        internal static int AimBotDistance = 300;
        internal static int AimBotBone = 132;
        internal static float AimbotFOV = 10f;

        //misc
        internal static bool InfiniteStamina = true;
        internal static bool DoorUnlocker = true;
        internal static bool MaxSkills = false;
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