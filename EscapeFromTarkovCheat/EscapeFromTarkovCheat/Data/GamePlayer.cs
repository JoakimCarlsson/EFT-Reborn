using System;
using System.Runtime.CompilerServices;
using EFT;
using UnityEngine;

namespace EFT.HideOut
{

    public class GamePlayer
    {

        public Player Player { get; }

        public Vector3 ScreenPosition => _screenPosition;

        public Vector3 HeadScreenPosition => _headScreenPosition;

        public bool IsOnScreen { get; private set; }

        public bool IsVisible { get; private set; }
        public float Fov { get; set; }
        public float Distance { get; private set; }

        public bool IsAI { get; private set; }

        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";

        private Vector3 _screenPosition;
        private Vector3 _headScreenPosition;



        public enum PlayerType
        {
            Scav,
            PlayerScav,
            Player,
            TeamMate,
            Boss
        }

        public GamePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Player = player;
            _screenPosition = default;
            _headScreenPosition = default;
            IsOnScreen = false;
            Distance = 0f;
            IsAI = true;
            Fov = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(Player))
                return;

            _screenPosition = GameUtils.WorldPointToScreenPoint(Player.Transform.position);

            if (Player.PlayerBones != null)
                _headScreenPosition = GameUtils.WorldPointToScreenPoint(Player.PlayerBones.Head.position);

            IsOnScreen = GameUtils.IsScreenPointVisible(_screenPosition);
            Distance = Vector3.Distance(Main.MainCamera.transform.position, Player.Transform.position);
            IsVisible = IsVisibles();
            Fov = GetFov();

            if ((Player.Profile != null) && (Player.Profile.Info != null))
                IsAI = (Player.Profile.Info.RegistrationDate <= 0);

        }

        private bool IsVisibles()
        {
            if (!IsOnScreen)
                return false;
            //This is realy bad and needs to be improved
            if (Physics.Linecast(Main.MainCamera.transform.position, GameUtils.GetBonePosByID(Player, 133), out var hit))
            {
                if (hit.transform.gameObject == Player.gameObject)
                    return true;
            }
            return false;
        }

        public float GetFov()
        {
            Vector3 myPos = Main.MainCamera.transform.position;
            Vector3 forward = Main.MainCamera.transform.forward;
            Vector3 normalized = (GameUtils.GetBonePosByID(Player, Settings.AimBotBone) - myPos).normalized;
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
        }
    }

}