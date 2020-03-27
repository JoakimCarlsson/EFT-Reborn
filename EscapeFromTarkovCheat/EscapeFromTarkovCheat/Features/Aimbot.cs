using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using BehaviourMachine;
using EFT;
using EFT.Animations;
using EFT.UI;
using JsonType;
using UnityEngine;

namespace EFT.Reborn
{
    class Aimbot : MonoBehaviour
    {
        private IEnumerable<GamePlayer> _targetList;
        private GamePlayer _target;
        public void FixedUpdate()
        {
            try
            {
                if (GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && Main.LocalPlayer.Weapon != null && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                {

                    if (Input.GetKey(Settings.AimbotKey) && _target == null)
                    {
                        _target = GetTarget();

                        if (_target != null)
                        {
                            Vector3 aimPos = _target.Player.PlayerBones.Head.position;
                            float travelTime = _target.Distance / Main.LocalPlayer.Weapon.CurrentAmmoTemplate.InitialSpeed;
                            aimPos.x += (_target.Player.Velocity.x * travelTime);
                            aimPos.y += (_target.Player.Velocity.y * travelTime);

                            Vector3 eulerAngles = Quaternion.LookRotation((aimPos - Main.LocalPlayer.PlayerBones.Fireport.position).normalized).eulerAngles;

                            if (eulerAngles.x > 180f)
                                eulerAngles.x -= 360f;
                            Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);

                        }
                    }
                    else
                    {
                        _target = null;
                    }
                }
            }
            catch
            {

            }

        }

        private GamePlayer GetTarget()
        {
            _targetList = Main.GamePlayers.Where(p => !p.Player.IsYourPlayer() && GameUtils.IsPlayerAlive(p.Player));
            _targetList = _targetList.OrderBy(p => p.Fov).ThenBy(p => p.Distance);

            foreach (var gamePlayer in _targetList)
            {
                if (GameUtils.IsFriend(gamePlayer.Player))
                    continue;

                if (gamePlayer.Distance > Settings.AimBotDistance) //change 300 hot a value ewe store in settings.
                    continue;

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
    }
}