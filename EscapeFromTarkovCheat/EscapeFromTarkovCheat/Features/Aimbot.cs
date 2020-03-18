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
        public void Update()
        {
            try
            {
                if (Main.GameWorld != null && Main.LocalPlayer != null && Main.LocalPlayer.Weapon != null)
                {
                    if (Settings.Aimbot && Input.GetKey(Settings.AimbotKey))
                        Aim();

                    if (Input.GetKeyUp(Settings.AimbotKey))
                        _target = null;
                }
            }
            catch
            {
                
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
            //target.Player.PlayerBones.Head.Original.transform.position
            Vector3 eulerAngles = Quaternion.LookRotation((GameUtils.GetBonePosByID(target.Player, 132) - Main.MainCamera.transform.position).normalized).eulerAngles;

            if (eulerAngles.x > 180f)
                eulerAngles.x -= 360f;
            Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
        }

        private GamePlayer GetTarget()
        {
            _targetList = Main.GamePlayers.Where(p => !p.Player.IsYourPlayer() && GameUtils.IsPlayerAlive(p.Player));
            _targetList = _targetList.OrderBy(p => p.Distance);

            foreach (var gamePlayer in _targetList)
            {
                float fov = Fov(GameUtils.GetBonePosByID(gamePlayer.Player, 133));

                if (fov <= Settings.AimbotFOV)
                    return gamePlayer;
            }

            return null;
        }

        public static float Fov(Vector3 position)
        {
            Vector3 myPos = Main.MainCamera.transform.position;
            Vector3 forward = Main.MainCamera.transform.forward;
            Vector3 normalized = (position - myPos).normalized;
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
        }

        private static RaycastHit raycastHit;
        private static LayerMask layerMask = 1 << 12 | 1 << 16 | 1 << 18;
        public static Vector3 BarrelRaycast()
        {
            try
            {
                if (Main.LocalPlayer != null && Main.LocalPlayer.Fireport == null)
                    return Vector3.zero;
                Physics.Linecast(Main.LocalPlayer.Fireport.position, Main.LocalPlayer.Fireport.position - Main.LocalPlayer.Fireport.up * 1000f, out raycastHit, layerMask);
                Console.WriteLine(raycastHit.transform.name);
                return raycastHit.point;
            }
            catch { return Vector3.zero; }
        }

        public static bool IsVisible(Vector3 Position)
        {
            return Physics.Linecast(GetShootPos(), Position, out var raycastHit, layerMask) && raycastHit.transform.name.Contains("Human");
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
            return firearmController.Fireport.position + Camera.main.transform.forward * 1f;
        }
    }
}
