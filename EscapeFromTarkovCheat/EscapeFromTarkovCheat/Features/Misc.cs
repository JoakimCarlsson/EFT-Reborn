using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSG.CameraEffects;
using EFT.HideOut;
using EFT.Interactive;
using EFT.InventoryLogic;
using UnityEngine;

namespace EFT.HideOut
{
    class Misc : MonoBehaviour
    {
        private string _hud = string.Empty;
        public void Update()
        {
            try
            {
                if (GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && Main.LocalPlayer.Weapon != null)
                {
                    NoRecoil();
                    DoorUnlock();
                    NoVisor();
                    MaxStats();
                    PrepareHud();
                    ThermalVison();
                    HotKeys();
                    InfiniteStamina();
                }
            }
            catch
            {
            }
        }

        private void InfiniteStamina()
        {
            if (Settings.InfiniteStamina && Main.LocalPlayer != null && Main.MainCamera != null)
            {
                Main.LocalPlayer.Physical.StaminaRestoreRate = 10000f;
                Main.LocalPlayer.Physical.Stamina.Current = 100f;
                Main.LocalPlayer.Physical.HandsRestoreRate = 1000f;
                Main.LocalPlayer.Physical.HandsStamina.Current = 100f;
            }
        }
        private void HotKeys()
        {
            if (Input.GetKeyDown(Settings.TogglePlayerESP))
                Settings.DrawPlayers = !Settings.DrawPlayers;
            if (Input.GetKeyDown(Settings.ToggleItemESP))
                Settings.DrawLootItems = !Settings.DrawLootItems;
            if (Input.GetKeyDown(Settings.ToggleLootableContainerESP))
                Settings.DrawLootableContainers = !Settings.DrawLootableContainers;
            if (Input.GetKeyDown(Settings.ToggleExitPoints))
                Settings.DrawExfiltrationPoints = !Settings.DrawExfiltrationPoints;
            if (Input.GetKeyDown(Settings.ToggleThermalVison))
                Settings.ThermalVison = !Settings.ThermalVison;
        }
        public void OnGUI()
        {
            if (Settings.DrawWeaponInfo)
            {
                var textStyle = new GUIStyle(GUI.skin.label) { fontSize = 25 };
                GUI.Label(new Rect(512, Screen.height - 48, 512, 48), _hud, textStyle);
            }

        }
        private void PrepareHud()
        {
            if (Main.LocalPlayer.HandsController == null || !(Main.LocalPlayer.HandsController.Item is Weapon) || !Settings.DrawWeaponInfo)
                return;

            var weapon = Main.LocalPlayer.Weapon;

            var mag = weapon?.GetCurrentMagazine();
            if (mag != null)
            {
                _hud = $"{mag.Count}+{weapon.ChamberAmmoCount}/{mag.MaxCount} [{weapon.SelectedFireMode.ToString()}]";

            }
        }

        private void NoRecoil()
        {
            if (Main.LocalPlayer == null)
                return;

            if (Settings.NoRecoil)
            {
                Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
            }
            else
            {
                Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 1f;
            }
        }

        private static void NoVisor()
        {
            try
            {
                if (Main.LocalPlayer == null || Main.MainCamera == null)
                    return;

                if (Settings.NoVisor)
                {
                    Main.MainCamera.GetComponent<VisorEffect>().Intensity = 0f;
                    Main.MainCamera.GetComponent<VisorEffect>().enabled = true;
                }
                else
                {
                    Main.MainCamera.GetComponent<VisorEffect>().Intensity = 1f;
                    Main.MainCamera.GetComponent<VisorEffect>().enabled = true;
                }
            }
            catch
            {

            }
        }

        private static void ThermalVison()
        {
            if (Main.LocalPlayer == null || Main.MainCamera == null)
                return;

            if (Settings.ThermalVison)
            {
                Main.MainCamera.GetComponent<ThermalVision>().On = true;
                Main.MainCamera.GetComponent<ThermalVision>().enabled = true;
            }
            else
            {
                Main.MainCamera.GetComponent<ThermalVision>().On = false;
                Main.MainCamera.GetComponent<ThermalVision>().enabled = true;
            }
        }

        private static void DoorUnlock()
        {
            try
            {
                if (Main.LocalPlayer == null || Main.MainCamera == null || !Settings.DoorUnlocker)
                    return;

                if (Input.GetKeyDown(Settings.UnlockDoors))
                {
                    foreach (var door in FindObjectsOfType<Door>())
                    {
                        if (door.DoorState == EDoorState.Open ||
                            Vector3.Distance(door.transform.position, Main.LocalPlayer.Position) > 20f)
                            continue;

                        door.DoorState = EDoorState.Shut;
                    }
                }
            }
            catch
            {
            }
        }

        private void MaxStats()
        {
            try
            {
                //if (_oldLevels.Count == 0)
                //{
                //    _oldLevels = Main.LocalPlayer.Skills.Skills.ToList();
                //}

                if (Settings.MaxSkills && Main.LocalPlayer != null && Main.MainCamera != null)
                {
                    foreach (var skill in Main.LocalPlayer.Skills.Skills)
                    {
                        if (skill.Level == 51)
                            continue;
                        skill.SetLevel(51);
                    }
                }
                //else if (!Settings.MaxSkills && Main.LocalPlayer != null && Main.MainCamera != null)
                //{
                //    foreach (var skill in Main.LocalPlayer.Skills.Skills)
                //    {
                //        foreach (var oldLevel in _oldLevels)
                //        {
                //            if (skill == oldLevel)
                //            {
                //                skill.SetLevel(oldLevel.Level);
                //            }
                //        }
                //    }
                //}
            }
            catch
            {

            }
        }
    }
}
