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
        private bool _visible = true;
        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;

        private string watermark =
            "<COLOR=#FF0000>O</color><COLOR=#FF4600>m</color><COLOR=#FF8C00>n</color><COLOR=#FFD200>i</color><COLOR=#FFff00></color><COLOR=#B9ff00>t</color><COLOR=#73ff00>r</color><COLOR=#2Dff00></color><COLOR=#00ff00>i</color><COLOR=#00ff46></color><COLOR=#00ff8C>x</color><COLOR=#00ffD2> </color><COLOR=#00ffff>2</color><COLOR=#00D2ff>.</color><COLOR=#008Cff>0</color>";

        private void Start()
        {
#if DEBUG
            AllocConsoleHandler.Open();
#endif
            //AllocConsoleHandler.Open();
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(20f, 220f, 250f, 150f);
            _miscVisualWindow = new Rect(20f, 260f, 250f, 150f);
            _aimbotVisualWindow = new Rect(20f, 260f, 250f, 150f);
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
        }

        private void RenderUi(int id)
        {
            GUI.color = new Color(28,36,33);
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

                    //Move into another window.
                    Settings.NoRecoil = GUILayout.Toggle(Settings.NoRecoil, "No Recoil");
                    Settings.SuperBullet = GUILayout.Toggle(Settings.SuperBullet, "Super Bullet");
                    Settings.NoSway = GUILayout.Toggle(Settings.NoSway, "No Sway");
                    Settings.MaxSkills = GUILayout.Toggle(Settings.MaxSkills, "Max Skills");
                    Settings.NoVisor = GUILayout.Toggle(Settings.NoVisor, "No Visor");
                    break;
            }
            GUI.DragWindow();
        }
    }
}
