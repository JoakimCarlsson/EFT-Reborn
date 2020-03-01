﻿using EscapeFromTarkovCheat;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace Menu.UI
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _miscVisualWindow;
        private Rect _aimbotVisualWindow;

        private bool _visible = true;
        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(20f, 220f, 250f, 150f);
            _miscVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _aimbotVisualWindow = new Rect(20f, 260f, 250f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;

            if (Input.GetKeyDown(KeyCode.Delete))
                Loader.Unload();
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 200, 60), "Carlsson");
            GUI.Label(new Rect(20, 40, 200, 60), "Escape From Tarkov");
            GUI.Label(new Rect(20, 60, 200, 60), "Verison 0.2");

            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, "Menu");

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _miscVisualWindow = GUILayout.Window(2, _playerVisualWindow, RenderUi, "Misc Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _playerVisualWindow, RenderUi, "Aimbot");
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    GUILayout.Label("Insert For Menu");
                    GUILayout.Label("Delete For Unload Menu");

                    if (GUILayout.Button("Player Visual"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Misc Visual"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    break;

                case 1:
                    //GUILayout.Label("Placeholder");
                    //GUILayout.Space(5f);
                    Settings.DrawPlayers = GUILayout.Toggle(Settings.DrawPlayers, "Draw Players");
                    Settings.DrawPlayerBox = GUILayout.Toggle(Settings.DrawPlayerBox, "Draw Player Box");
                    Settings.DrawPlayerName = GUILayout.Toggle(Settings.DrawPlayerName, "Draw Player Name");
                    Settings.DrawPlayerSkeleton = GUILayout.Toggle(Settings.DrawPlayerSkeleton, "Draw Player Skeleton");
                    Settings.DrawPlayerHealth = GUILayout.Toggle(Settings.DrawPlayerHealth, "Draw Player Health");
                    GUILayout.Label($"Player Distance {(int)Settings.DrawPlayersDistance} m");
                    Settings.DrawPlayersDistance = GUILayout.HorizontalSlider(Settings.DrawPlayersDistance,0f, 2000f);
                    break;

                case 2:
                    //GUILayout.Label("Misc Visual");
                    //GUILayout.Space(5f);

                    Settings.DrawLootItems = GUILayout.Toggle(Settings.DrawLootItems, "Draw Loot Items");
                    GUILayout.Label($"Loot Item Distance {(int)Settings.DrawLootItemsDistance} m");
                    Settings.DrawLootItemsDistance = GUILayout.HorizontalSlider(Settings.DrawLootItemsDistance, 0f, 2000f);

                    Settings.DrawLootableContainers = GUILayout.Toggle(Settings.DrawLootableContainers, "Draw Containers");
                    GUILayout.Label($"Container Distance {(int)Settings.DrawLootableContainersDistance} m");
                    Settings.DrawLootableContainersDistance = GUILayout.HorizontalSlider(Settings.DrawLootableContainersDistance, 0f, 2000f);

                    Settings.DrawExfiltrationPoints = GUILayout.Toggle(Settings.DrawExfiltrationPoints, "Draw Exits");
                    break;

                case 3:
                    Settings.Aimbot = GUILayout.Toggle(Settings.Aimbot, "Aimbot");
                    Settings.AimbotDrawFov = GUILayout.Toggle(Settings.AimbotDrawFov, "Aimbot Draw FOV");
                    GUILayout.Label($"Aimbot FOV {(int)Settings.AimbotFOV} m");
                    Settings.AimbotFOV = GUILayout.HorizontalSlider(Settings.AimbotFOV, 0f, 360);

                    Settings.AimbotSmooth = GUILayout.Toggle(Settings.AimbotSmooth, "Aimbot Smooth");
                    GUILayout.Label($"Aimbot Smooth {(int)Settings.AimbotSmoothValue} m");
                    Settings.AimbotSmoothValue = GUILayout.HorizontalSlider(Settings.AimbotSmoothValue, 0f, 360);
                    break;
            }
            GUI.DragWindow();
        }
    }
}