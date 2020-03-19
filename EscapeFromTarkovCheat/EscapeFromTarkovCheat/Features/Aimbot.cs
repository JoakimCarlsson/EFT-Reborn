using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using BehaviourMachine;
using EFT;
using EFT.Animations;
using JsonType;
using UnityEngine;

namespace EFT.HideOut
{
    class Aimbot : MonoBehaviour
    {
        private IEnumerable<GamePlayer> _targetList;
        private GamePlayer _target;
        private Vector3 _aimTarget = Vector3.zero;


            //public void Update()
        //{
        //    try
        //    {
        //        if (Main.GameWorld != null && Main.LocalPlayer != null && Main.LocalPlayer.Weapon != null)
        //        {
        //            if (Settings.Aimbot && Input.GetKey(Settings.AimbotKey))
        //                Aim();

        //            if (Input.GetKeyUp(Settings.AimbotKey))
        //                _target = null;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        public void OnGUI()
        {
            if (Input.GetKey(Settings.AimbotKey))
            {
                AimLock();
            }
            else
            {
                _aimTarget = Vector3.zero;
            }
        }

        private void Aim()
        {
            if (_target == null)
                _target = GetTarget();
            else
                AimAtTarget(_target);
        }

        private void AimAtTarget(GamePlayer target)
        {
            Vector3 eulerAngles = Quaternion
                .LookRotation((GameUtils.GetBonePosByID(target.Player, 132) - Main.MainCamera.transform.position)
                    .normalized).eulerAngles;

            if (eulerAngles.x > 180f)
                eulerAngles.x -= 360f;
            Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }

        private GamePlayer GetTarget()
        {
            _targetList = Main.GamePlayers.Where(p => !p.Player.IsYourPlayer() && GameUtils.IsPlayerAlive(p.Player));
            _targetList = _targetList.OrderBy(p => p.Fov).ThenBy(p => p.Distance);

            foreach (var gamePlayer in _targetList)
            {
                if (gamePlayer.Fov <= Settings.AimbotFOV)
                    return gamePlayer;
            }

            return null;
        }

        private static LayerMask layerMask = 1 << 12 | 1 << 16 | 1 << 18;

        public static bool IsVisible(Vector3 Position)
        {
            return Physics.Linecast(GetShootPos(), Position, out var raycastHit, layerMask) &&
                   raycastHit.transform.name.Contains("Human");
        }

        public static Vector3 GetShootPos()
        {
            if (Main.LocalPlayer == null)
            {
                return Vector3.zero;
            }

            Player.FirearmController firearmController = Main.LocalPlayer.HandsController as Player.FirearmController;
            if (firearmController == null)
            {
                return Vector3.zero;
            }

            return firearmController.Fireport.position + Main.MainCamera.transform.forward * 1f;
        }

        public void AimLock()
        {
            var targetList = Main.GamePlayers.OrderBy(p => p.Fov);

            foreach (var player in targetList)
            {
                if (player != null && player.Player != Main.LocalPlayer && GameUtils.IsPlayerAlive(player.Player))
                {
                    //if (!Main.LocalPlayer.isInYourGroup(player))
                    //{
                        Vector3 aimPos = GameUtils.GetBonePosByID(player.Player, (int) GamePlayer.BodyPart.Head);

                        if (player.Distance > Settings.AimBotDistance) //change 300 hot a value ewe store in settings.
                            continue;

                        if (aimPos != Vector3.zero && player.Fov <= Settings.AimbotFOV)
                        {
                            float travelTime = player.Distance / Main.LocalPlayer.Weapon.CurrentAmmoTemplate.InitialSpeed;

                            aimPos.x += (player.Player.Velocity.x * travelTime);
                            aimPos.y += (player.Player.Velocity.y * travelTime);

                            _aimTarget = aimPos;
                        }
                    //}
                }
            }

            if (_aimTarget != Vector3.zero)
            {
                AimAtTarget(_aimTarget);
                Render.Circle(Main.MainCamera.WorldToScreenPoint(_aimTarget).x - 5f, Main.MainCamera.WorldToScreenPoint(_aimTarget).y - 5f, 10);
            }
        }

        private void AimAtTarget(Vector3 pos)
        {
            Vector3 eulerAngles = Quaternion.LookRotation((pos - Main.MainCamera.transform.position).normalized).eulerAngles;

            if (eulerAngles.x > 180f)
                eulerAngles.x -= 360f;
            Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }

    }
}
