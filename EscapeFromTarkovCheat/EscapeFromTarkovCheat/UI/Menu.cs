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

        private bool _visible = true;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;

        private string _watermark = "Reborn";

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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 1000, 500), _watermark);
            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, _watermark);

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _miscVisualWindow = GUILayout.Window(2, _miscVisualWindow, RenderUi, "Misc Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _aimbotVisualWindow, RenderUi, "Aimbot");
            if (_miscFeatureslVisible)
                _miscFeatureslVisualWindow = GUILayout.Window(4, _miscFeatureslVisualWindow, RenderUi, "Misc");
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
                    break;

                case 1:
                    Settings.DrawPlayers = GUILayout.Toggle(Settings.DrawPlayers, "Draw Players (F1)");
                    Settings.DrawPlayerBox = GUILayout.Toggle(Settings.DrawPlayerBox, "Draw Player Box");
                    Settings.DrawPlayerName = GUILayout.Toggle(Settings.DrawPlayerName, "Draw Player Name");
                    Settings.DrawPlayerLine = GUILayout.Toggle(Settings.DrawPlayerLine, "Draw Player Line");
                    Settings.DrawPlayerHealth = GUILayout.Toggle(Settings.DrawPlayerHealth, "Draw Player Health");
                    GUILayout.Label($"Player Distance {(int)Settings.DrawPlayersDistance} m");
                    Settings.DrawPlayersDistance = GUILayout.HorizontalSlider(Settings.DrawPlayersDistance,0f, 2000f);
                    break;

                case 2:
                    Settings.DrawLootItems = GUILayout.Toggle(Settings.DrawLootItems, "Draw Loot Items (F2)");
                    GUILayout.Label($"Loot Item Distance {(int)Settings.DrawLootItemsDistance} m");
                    Settings.DrawLootItemsDistance = GUILayout.HorizontalSlider(Settings.DrawLootItemsDistance, 0f, 1000f);
                    Settings.DrawLootableContainers = GUILayout.Toggle(Settings.DrawLootableContainers, "Draw Containers (F4)");
                    Settings.DrawContainersContent = GUILayout.Toggle(Settings.DrawContainersContent, "Draw Containers Content");
                    Settings.DrawEmptyContainers = GUILayout.Toggle(Settings.DrawEmptyContainers, "Draw Empty Containers");
                    GUILayout.Label($"Container Distance {(int)Settings.DrawLootableContainersDistance} m");
                    Settings.DrawLootableContainersDistance = GUILayout.HorizontalSlider(Settings.DrawLootableContainersDistance, 0f, 1000f);
                    Settings.DrawExfiltrationPoints = GUILayout.Toggle(Settings.DrawExfiltrationPoints, "Draw Exits (F6)");
                    break;

                case 3:
                    Settings.Aimbot = GUILayout.Toggle(Settings.Aimbot, "Aimbot");
                    Settings.DrawAimbotPoint = GUILayout.Toggle(Settings.DrawAimbotPoint, "Aimbot Point");
                    GUILayout.Label($"Aimbot FOV {(int)Settings.AimbotFOV} m");
                    Settings.AimbotFOV = GUILayout.HorizontalSlider(Settings.AimbotFOV, 0f, 180);
                    GUILayout.Label($"Aimbot Distance {Settings.AimBotDistance} m");
                    Settings.AimBotDistance = (int)GUILayout.HorizontalSlider(Settings.AimBotDistance, 0, 1000);
                    break;

                case 4:
                    Settings.MaxSkills = GUILayout.Toggle(Settings.MaxSkills, "Max Skills");
                    Settings.InfiniteStamina = GUILayout.Toggle(Settings.InfiniteStamina, "Infinite Stamina");
                    Settings.DoorUnlocker = GUILayout.Toggle(Settings.DoorUnlocker, "Door Unlocker. (F8)");
                    Settings.NoVisor = GUILayout.Toggle(Settings.NoVisor, "No Visor");
                    Settings.ThermalVison = GUILayout.Toggle(Settings.ThermalVison, "Thermal Vison");
                    Settings.NoRecoil = GUILayout.Toggle(Settings.NoRecoil, "No Recoil");
                    break;
            }
            GUI.DragWindow();
        }
    }
}
