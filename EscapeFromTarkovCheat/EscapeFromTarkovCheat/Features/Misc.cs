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
                    SuperWeapon();
                    NoRecoil();
                    NoSway();
                    SuperBullet();
                    DoorUnlock();
                    NoVisor();
                    MaxStats();
                    SpeedHack();
                    PrepareHud();
                    ThermalVison();
                }
            }
            catch
            {
            }
        }

        private void SuperWeapon()
        {
            if (Main.LocalPlayer == null || !(Main.LocalPlayer.HandsController.Item is Weapon))
                return;

            if (Settings.FastReload)
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.isFastReload = true;

            if (Settings.AlwaysAutomatic)
            {
                Main.LocalPlayer.Weapon.GetItemComponent<FireModeComponent>().FireMode = Weapon.EFireMode.fullauto;
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.BoltAction = false;
            }

            if (Settings.FireRate)
            {
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.bFirerate = Settings.FireRateValue;
                var weapon = Main.LocalPlayer.Weapon;
            }
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

        private void SpeedHack()
        {
            if (Settings.SpeedHack)
            {
                Time.timeScale = Settings.SpeedValue;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        private static void SuperBullet()
        {
            if (Settings.SuperBullet && Main.LocalPlayer != null && Main.LocalPlayer.Weapon != null)
            {
                Main.LocalPlayer.Weapon.Template.DefAmmoTemplate.PenetrationChance = 1000;
                Main.LocalPlayer.Weapon.Template.DefAmmoTemplate.PenetrationPower = 1000;
                Main.LocalPlayer.Weapon.Template.DefAmmoTemplate.InitialSpeed = 1000f;
            }
            else
            {
                Main.LocalPlayer.Weapon.Template.DefAmmoTemplate.PenetrationChance = 1;
                Main.LocalPlayer.Weapon.Template.DefAmmoTemplate.PenetrationPower = 50;
            }
        }

        private static void NoSway()
        {
            if (Settings.NoSway)
            {
                Main.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 0;
                Main.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
                Main.LocalPlayer.ProceduralWeaponAnimation.Walk.Intensity = 0;
                Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0;
                Main.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0;
            }
        }

        private void NoRecoil()
        {
            if (Main.LocalPlayer == null && !Settings.NoRecoil)
                return;

            Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
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
            if (Settings.MaxSkills && Main.LocalPlayer != null && Main.MainCamera != null)
            {
                Main.LocalPlayer.Physical.StaminaRestoreRate = 10000f;
                Main.LocalPlayer.Physical.Stamina.Current = 100f;
                Main.LocalPlayer.Physical.HandsRestoreRate = 1000f;
                Main.LocalPlayer.Physical.HandsStamina.Current = 1000f;
                Main.LocalPlayer.Physical.CapacityBuff = 1000f;
            }
        }
    }
}
