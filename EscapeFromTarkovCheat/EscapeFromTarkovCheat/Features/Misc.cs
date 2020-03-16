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
                    ChangeFOV();
                    NoRecoil();
                    NoSway();
                    SuperBullet();
                    DoorUnlock();
                    NoVisor();
                    MaxStats();
                    //Teleport();
                    SpeedHack();
                    PrepareHud();
                }
            }
            catch
            {
            }
        }
        public void OnGUI()
        {
            var textStyle = new GUIStyle(GUI.skin.label) {fontSize = 32 };
            GUI.Label(new Rect(512, Screen.height - 48, 512, 48), _hud, textStyle);
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

        private static void ChangeFOV()
        {
            if (Settings.ChangeFOV)
            {
                GClass436.SetFov(Settings.FOVValue, 1f);
                GClass436.ApplyFoV(Settings.FOVValue, 100, 120);
            }

        }

        //private void Teleport()
        //{
        //    if (Settings.Teleport)
        //    {
        //        if (Input.GetKeyDown(KeyCode.UpArrow))
        //        {
        //            Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.forward * 1;
        //        }
        //    }
        //}

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
                Main.LocalPlayer.Weapon.Template.DefAmmoTemplate.InitialSpeed = 10000f;
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

        private static void DoorUnlock()
        {
            try
            {
                if (Main.LocalPlayer == null || Main.MainCamera == null)
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
                Main.LocalPlayer.Skills.Metabolism.Buff = 10000;
                Main.LocalPlayer.Physical.Sprinting.RestoreRate = 10000.0f;
                Main.LocalPlayer.Physical.Sprinting.DrainRate = 0.0f;
                Main.LocalPlayer.Skills.StrengthBuffSprintSpeedInc.Value = 10000f;
                Main.LocalPlayer.Skills.StrengthBuffLiftWeightInc.Value = -100f;
                Main.LocalPlayer.Skills.StrengthBuffMeleeCrits.Value = true;
                Main.LocalPlayer.Skills.StrengthBuffMeleePowerInc.Value = 100f;
                Main.LocalPlayer.Skills.StrengthBuffThrowDistanceInc.Value = 1f;
                Main.LocalPlayer.Skills.AttentionLootSpeed.Value = 5000f;
                Main.LocalPlayer.Skills.MagDrillsLoadSpeed.Value = 500f;
                Main.LocalPlayer.Skills.MagDrillsUnloadSpeed.Value = 500f;
                Main.LocalPlayer.Skills.SearchBuffSpeed.Value = 500f;
                Main.LocalPlayer.Skills.AttentionLootSpeed.Value = 500f;
            }
        }
    }
}
