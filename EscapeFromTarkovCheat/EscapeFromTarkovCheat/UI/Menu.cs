using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EFT.Reborn
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _miscVisualWindow;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeatureslVisualWindow;
        private Rect _weaponVisualWindow;
        private Rect _hotKeysVisualWindow;

        private bool _visible = false;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;
        private bool _weaponFeatureslVisible;
        private bool _hotKeysVisualVisible;

        private string watermark = "Reborn Elite";

        private void Start()
        {
#if DEBUG
            AllocConsoleHandler.Open();
#endif
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(20f, 220f, 250f, 150f);
            _miscVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _aimbotVisualWindow = new Rect(20f, 260f, 250, 150f);
            _miscFeatureslVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _weaponVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _hotKeysVisualWindow = new Rect(20f, 260f, 250f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(Settings.ToggleMenu))
                _visible = !_visible;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 1000, 500), watermark);
            
            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, watermark);

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _miscVisualWindow = GUILayout.Window(2, _miscVisualWindow, RenderUi, "Misc Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _aimbotVisualWindow, RenderUi, "Aimbot");
            if (_miscFeatureslVisible)
                _miscFeatureslVisualWindow = GUILayout.Window(4, _miscFeatureslVisualWindow, RenderUi, "Misc");
            if (_weaponFeatureslVisible)
                _weaponVisualWindow = GUILayout.Window(5, _weaponVisualWindow, RenderUi, "Weapon");
            if (_hotKeysVisualVisible)
                _hotKeysVisualWindow = GUILayout.Window(6, _hotKeysVisualWindow, RenderUi, "Hot Keys");
        }

        private void RenderUi(int id)
        {
            GUI.color = new Color(28, 36, 33);
            switch (id)
            {
                case 0:
                    GUILayout.Label($"{Settings.ToggleMenu} For Menu");
                    if (GUILayout.Button("Player Visual"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Misc Visual"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    if (GUILayout.Button("Misc"))
                        _miscFeatureslVisible = !_miscFeatureslVisible;
                    if (GUILayout.Button("Weapon Shit"))
                        _weaponFeatureslVisible = !_weaponFeatureslVisible;
                    if (GUILayout.Button("HotKeys"))
                        _hotKeysVisualVisible = !_hotKeysVisualVisible;
                    //if (GUILayout.Button("Colors"))
                    //{
                    //    //Do Colours here.
                    //}
                    //GUILayout.Space(20);
                    //GUILayout.BeginHorizontal();
                    //if (GUILayout.Button("Save"))
                    //{
                    //    //Do Save here
                    //}
                    //if (GUILayout.Button("Load"))
                    //{
                    //    //Do Load here
                    //}
                    //GUILayout.EndHorizontal();
                    break;

                case 1:
                    Settings.DrawPlayers = GUILayout.Toggle(Settings.DrawPlayers, $"Draw Players {Settings.TogglePlayerESP}");
                    Settings.DrawPlayerBox = GUILayout.Toggle(Settings.DrawPlayerBox, "Draw Player Box");
                    Settings.DrawPlayerName = GUILayout.Toggle(Settings.DrawPlayerName, "Draw Player Name");
                    Settings.DrawPlayerDistance = GUILayout.Toggle(Settings.DrawPlayerDistance, "Draw Player Distance");
                    Settings.DrawPlayerLine = GUILayout.Toggle(Settings.DrawPlayerLine, "Draw Player Line");
                    Settings.DrawPlayerHealthBar = GUILayout.Toggle(Settings.DrawPlayerHealthBar, "Draw Player Health Bar");
                    Settings.DrawPlayerHealth = GUILayout.Toggle(Settings.DrawPlayerHealth, "Draw Player Health Number");
                    Settings.DrawPlayerSkeleton = GUILayout.Toggle(Settings.DrawPlayerSkeleton, "Draw Player Skeleton");
                    Settings.DrawPlayerWeapon = GUILayout.Toggle(Settings.DrawPlayerWeapon, "Draw Player Weapon");
                    Settings.DrawPlayerLevel = GUILayout.Toggle(Settings.DrawPlayerLevel, "Draw Player Level");
                    Settings.DrawPlayerCorpses = GUILayout.Toggle(Settings.DrawPlayerCorpses, "Draw Player Corpses");
                    GUILayout.Label($"Player Skeleton Distance {(int)Settings.DrawPlayerSkeletonDistance} m");
                    Settings.DrawPlayerSkeletonDistance = GUILayout.HorizontalSlider(Settings.DrawPlayerSkeletonDistance, 0f, 2000f);
                    GUILayout.Label($"Player Distance {(int)Settings.DrawPlayersRange} m");
                    Settings.DrawPlayersRange = GUILayout.HorizontalSlider(Settings.DrawPlayersRange, 0f, 2000f);

                    GUILayout.Label("");
                    GUILayout.Label("Scavs");
                    Settings.DrawScavs = GUILayout.Toggle(Settings.DrawScavs, "Draw Scavs");
                    Settings.DrawScavBox = GUILayout.Toggle(Settings.DrawScavBox, "Draw Scav Box");
                    Settings.DrawScavName = GUILayout.Toggle(Settings.DrawScavName, "Draw Scav Name");
                    Settings.DrawScavDistance = GUILayout.Toggle(Settings.DrawScavDistance, "Draw Scav Distance");
                    Settings.DrawScavLine = GUILayout.Toggle(Settings.DrawScavLine, "Draw Scav Line");
                    Settings.DrawScavHealthBar = GUILayout.Toggle(Settings.DrawScavHealthBar, "Draw Scav Health Bar");
                    Settings.DrawScavHealth = GUILayout.Toggle(Settings.DrawScavHealth, "Draw Scav Health Number");
                    Settings.DrawScavWeapon = GUILayout.Toggle(Settings.DrawScavWeapon, "Draw Scav Weapon");
                    Settings.DrawScavSkeleton = GUILayout.Toggle(Settings.DrawScavSkeleton, "Draw Scav Skeleton");
                    GUILayout.Label($"Scav Skeleton Distance {(int)Settings.DrawScavSkeletonDistance} m");
                    Settings.DrawScavSkeletonDistance = GUILayout.HorizontalSlider(Settings.DrawScavSkeletonDistance, 0f, 2000f);
                    GUILayout.Label($"Scav Distance {(int)Settings.DrawScavRange} m");
                    Settings.DrawScavRange = GUILayout.HorizontalSlider(Settings.DrawScavRange, 0f, 2000f);
                    break;

                case 2:
                    Settings.DrawLootItems = GUILayout.Toggle(Settings.DrawLootItems, $"Draw Loot Items {Settings.ToggleItemESP}");
                    GUILayout.Label($"Loot Item Distance {(int)Settings.DrawLootItemsDistance} m");
                    Settings.DrawLootItemsDistance = GUILayout.HorizontalSlider(Settings.DrawLootItemsDistance, 0f, 1000f);
                    Settings.DrawLootableContainers = GUILayout.Toggle(Settings.DrawLootableContainers, $"Draw Containers {Settings.ToggleLootableContainerESP}");
                    Settings.DrawContainersContent = GUILayout.Toggle(Settings.DrawContainersContent, "Draw Containers Content");
                    Settings.DrawEmptyContainers = GUILayout.Toggle(Settings.DrawEmptyContainers, "Draw Empty Containers");
                    GUILayout.Label($"Container Distance {(int)Settings.DrawLootableContainersDistance} m");
                    Settings.DrawLootableContainersDistance = GUILayout.HorizontalSlider(Settings.DrawLootableContainersDistance, 0f, 1000f);
                    Settings.GrenadeESP = GUILayout.Toggle(Settings.GrenadeESP, "Grenade ESP");
                    Settings.DrawExfiltrationPoints = GUILayout.Toggle(Settings.DrawExfiltrationPoints, $"Draw Exits {Settings.ToggleExitPoints}");
                    break;

                case 3:
                    Settings.Aimbot = GUILayout.Toggle(Settings.Aimbot, "Aimbot");
                    GUILayout.Label($"Aimbot FOV {(int)Settings.AimbotFOV} m");
                    Settings.AimbotFOV = GUILayout.HorizontalSlider(Settings.AimbotFOV, 0f, 180);
                    GUILayout.Label($"Aimbot Distance {Settings.AimBotDistance} m");
                    Settings.AimBotDistance = (int)GUILayout.HorizontalSlider(Settings.AimBotDistance, 0, 1000);
                    if (GUILayout.Button("Aimbot Key: " + Settings.AimbotKey))
                        Settings.AimbotKey = KeyCode.None;
                    GUILayout.Space(20);
                    //GUILayout.BeginHorizontal();

                    //if (GUILayout.Button("Head"))
                    //    Settings.AimBotBone = 133;
                    //if (GUILayout.Button("Neck"))
                    //    Settings.AimBotBone = 132;
                    //if (GUILayout.Button("Stomach"))
                    //    Settings.AimBotBone = 66;


                    //GUILayout.EndHorizontal();
                    break;

                case 4:
                    Settings.MaxSkills = GUILayout.Toggle(Settings.MaxSkills, "Max Skills");
                    Settings.InfiniteStamina = GUILayout.Toggle(Settings.InfiniteStamina, "Infinite Stamina");
                    Settings.DoorUnlocker = GUILayout.Toggle(Settings.DoorUnlocker, $"Door Unlocker. {Settings.DoorUnlocker}");
                    Settings.NoVisor = GUILayout.Toggle(Settings.NoVisor, "No Visor");
                    Settings.ThermalVison = GUILayout.Toggle(Settings.ThermalVison, "Thermal Vison");
                    Settings.SpeedHack = GUILayout.Toggle(Settings.SpeedHack, $"Speedhack {Settings.SpeedValue} {Settings.ToggleSpeedHack}");
                    Settings.SpeedValue = GUILayout.HorizontalSlider(Settings.SpeedValue, 1f, 3);
                    break;
                case 5:
                    Settings.FastReload = GUILayout.Toggle(Settings.FastReload, "Fast Reload");
                    Settings.AlwaysAutomatic = GUILayout.Toggle(Settings.AlwaysAutomatic, "Always Automatic");
                    Settings.FireRate = GUILayout.Toggle(Settings.FireRate, $"Change Fire Rate {Settings.FireRateValue}");
                    Settings.FireRateValue = (int)GUILayout.HorizontalSlider(Settings.FireRateValue, 1000, 3000);
                    Settings.DrawWeaponInfo = GUILayout.Toggle(Settings.DrawWeaponInfo, "Draw Weapon Info");
                    Settings.NoRecoil = GUILayout.Toggle(Settings.NoRecoil, "No Recoil");
                    Settings.SuperBullet = GUILayout.Toggle(Settings.SuperBullet, "Super Bullet");
                    Settings.NoSway = GUILayout.Toggle(Settings.NoSway, "No Sway");
                    break;

                case 6:
                    if (GUILayout.Button("Toogle Menu: " + Settings.ToggleMenu))
                        Settings.ToggleMenu = KeyCode.None;
                    if (GUILayout.Button("Player ESP: " + Settings.TogglePlayerESP))
                        Settings.TogglePlayerESP = KeyCode.None;
                    if (GUILayout.Button("Item ESP: " + Settings.ToggleItemESP))
                        Settings.ToggleItemESP = KeyCode.None;
                    if (GUILayout.Button("Item ESP Cycles: " + Settings.ItemCategory))
                        Settings.ItemCategory = KeyCode.None;
                    if (GUILayout.Button("Lootable Container: " + Settings.ToggleLootableContainerESP))
                        Settings.ToggleLootableContainerESP = KeyCode.None;
                    if (GUILayout.Button("Exit Points: " + Settings.ToggleExitPoints))
                        Settings.ToggleExitPoints = KeyCode.None;
                    if (GUILayout.Button("Speed Hack: " + Settings.ToggleSpeedHack))
                        Settings.ToggleSpeedHack = KeyCode.None;
                    if (GUILayout.Button("Unlock Doors: " + Settings.UnlockDoors))
                        Settings.UnlockDoors = KeyCode.None;
                    if (GUILayout.Button("Thermal Vison: " + Settings.ToggleThermalVison))
                        Settings.ToggleThermalVison = KeyCode.None;
                    break;
            }
            GUI.DragWindow();

            if (Settings.AimbotKey == KeyCode.None)
            {
                Event e = Event.current;
                Settings.AimbotKey = e.keyCode;
            }
            if (Settings.TogglePlayerESP == KeyCode.None)
            {
                Event e = Event.current;
                Settings.TogglePlayerESP = e.keyCode;
            }
            if (Settings.ToggleMenu == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ToggleMenu = e.keyCode;
            }
            if (Settings.ToggleItemESP == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ToggleItemESP = e.keyCode;
            }
            if (Settings.ItemCategory == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ItemCategory = e.keyCode;
            }
            if (Settings.ToggleLootableContainerESP == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ToggleLootableContainerESP = e.keyCode;
            }
            if (Settings.ToggleExitPoints == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ToggleExitPoints = e.keyCode;
            }
            if (Settings.ToggleSpeedHack == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ToggleSpeedHack = e.keyCode;
            }
            if (Settings.UnlockDoors == KeyCode.None)
            {
                Event e = Event.current;
                Settings.UnlockDoors = e.keyCode;
            }
            if (Settings.ToggleThermalVison == KeyCode.None)
            {
                Event e = Event.current;
                Settings.ToggleThermalVison = e.keyCode;
            }
        }
    }
}
