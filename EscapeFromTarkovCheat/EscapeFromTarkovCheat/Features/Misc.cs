using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.HideOut;
using EFT.Interactive;
using UnityEngine;

namespace EFT.HideOut
{
    class Misc : MonoBehaviour
    {
        public void Update()
        {
            try
            {
                if (GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && Main.LocalPlayer.Weapon != null)
                {
                    NoRecoil();
                    NoSway();
                    SuperBullet();
                    DoorUnlock();
                    NoVisor();
                    MaxStats();
                    Teleport();
                    SpeedHack();
                }
            }
            catch
            {
            }
        }

        private void Teleport()
        {
            if (Settings.Teleport)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.forward * 1;
                }
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
            }
        }

    }
}
