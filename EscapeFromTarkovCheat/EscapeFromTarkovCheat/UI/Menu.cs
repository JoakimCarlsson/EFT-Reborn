using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EFT.HideOut
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _miscVisualWindow;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeatureslVisualWindow;
        private Rect _weaponVisualWindow;

        private bool _visible = true;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;
        private bool _weaponFeatureslVisible;

        private string watermark =
            "<COLOR=#FF0000>F</color><COLOR=#FF4600>U</color><COLOR=#FF8C00>C</color><COLOR=#FFD200>K</color><COLOR=#FFff00></color><COLOR=#B9ff00></color><COLOR=#73ff00></color><COLOR=#2Dff00></color> <COLOR=#00ff00>O</color><COLOR=#00ff46></color><COLOR=#00ff8C>x</color><COLOR=#00ffD2></color><COLOR=#00ffff>F</color><COLOR=#00D2ff>F</color><COLOR=#008Cff> :-)</color>";

        private void Start()
        {
#if DEBUG
            AllocConsoleHandler.Open();
#endif
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(20f, 220f, 250f, 150f);
            _miscVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _aimbotVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _miscFeatureslVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _weaponVisualWindow = new Rect(20f, 260f, 250f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
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
        }

        private void RenderUi(int id)
        {
            GUI.color = new Color(28,36,33);
            switch (id)
            {
                case 0:
                    GUILayout.Label("Insert For Menu");
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
                    break;

                case 1:
                    Settings.DrawPlayers = GUILayout.Toggle(Settings.DrawPlayers, "Draw Players");
                    Settings.DrawPlayerBox = GUILayout.Toggle(Settings.DrawPlayerBox, "Draw Player Box");
                    Settings.DrawPlayerName = GUILayout.Toggle(Settings.DrawPlayerName, "Draw Player Name");
                    Settings.DrawPlayerLine = GUILayout.Toggle(Settings.DrawPlayerLine, "Draw Player Line");
                    Settings.DrawPlayerHealth = GUILayout.Toggle(Settings.DrawPlayerHealth, "Draw Player Health");
                    GUILayout.Label($"Player Distance {(int)Settings.DrawPlayersDistance} m");
                    Settings.DrawPlayersDistance = GUILayout.HorizontalSlider(Settings.DrawPlayersDistance,0f, 2000f);
                    break;

                case 2:
                    Settings.DrawLootItems = GUILayout.Toggle(Settings.DrawLootItems, "Draw Loot Items");
                    GUILayout.Label($"Loot Item Distance {(int)Settings.DrawLootItemsDistance} m");
                    Settings.DrawLootItemsDistance = GUILayout.HorizontalSlider(Settings.DrawLootItemsDistance, 0f, 1000f);

                    Settings.DrawLootableContainers = GUILayout.Toggle(Settings.DrawLootableContainers, "Draw Containers");
                    GUILayout.Label($"Container Distance {(int)Settings.DrawLootableContainersDistance} m");
                    Settings.DrawLootableContainersDistance = GUILayout.HorizontalSlider(Settings.DrawLootableContainersDistance, 0f, 1000f);

                    Settings.DrawExfiltrationPoints = GUILayout.Toggle(Settings.DrawExfiltrationPoints, "Draw Exits");
                    break;

                case 3:
                    Settings.Aimbot = GUILayout.Toggle(Settings.Aimbot, "Aimbot");
                    GUILayout.Label($"Aimbot FOV {(int)Settings.AimbotFOV} m");
                    Settings.AimbotFOV = GUILayout.HorizontalSlider(Settings.AimbotFOV, 0f, 180);
                    break;
                case 4:
                    Settings.MaxSkills = GUILayout.Toggle(Settings.MaxSkills, "Max Skills");
                    Settings.DoorUnlocker = GUILayout.Toggle(Settings.DoorUnlocker, "Door Unlocker. Press numpad 4 for unlock.");
                    Settings.NoVisor = GUILayout.Toggle(Settings.NoVisor, "No Visor");
                    Settings.SpeedHack = GUILayout.Toggle(Settings.SpeedHack, $"Speedhack {Settings.SpeedValue}");
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
            }
            GUI.DragWindow();
        }
    }
}
