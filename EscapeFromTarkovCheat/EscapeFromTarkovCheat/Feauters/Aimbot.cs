using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using EFT;
using EFT.Animations;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters
{
    class Aimbot : MonoBehaviour
    {
        private IEnumerable<GamePlayer> _targetList;
        private GamePlayer _target;
        public void Update()
        {
            if ((Main.GameWorld != null))
            {
                if (Settings.NoRecoil)
                    NoRecoil();

                if (Settings.Aimbot && Input.GetKey(Settings.AimbotKey))
                    Aim();

                if (Input.GetKeyUp(Settings.AimbotKey))
                {
                    _target = null;
                }
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
            Vector3 eulerAngles = Quaternion.LookRotation((GameUtils.GetBonePosByID(target.Player, 133) - Main.MainCamera.transform.position).normalized).eulerAngles;
           
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

        private void NoRecoil()
        {
            if (Main.LocalPlayer == null)
                return;

            Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
        }
    }
}
