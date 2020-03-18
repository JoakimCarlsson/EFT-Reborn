using System;
using System.Runtime.CompilerServices;
using EFT;
using UnityEngine;

namespace EFT.HideOut
{

    public class GamePlayer
    {

        public Player Player { get; }

        public Vector3 ScreenPosition => screenPosition;

        public Vector3 HeadScreenPosition => headScreenPosition;

        public bool IsOnScreen { get; private set; }

        public bool IsVisible { get; private set; }
        public float Fov { get; set; }
        public float Distance { get; private set; }

        public bool IsAI { get; private set; }

        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";

        private Vector3 screenPosition;
        private Vector3 headScreenPosition;

        public GamePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Player = player;
            screenPosition = default;
            headScreenPosition = default;
            IsOnScreen = false;
            Distance = 0f;
            IsAI = true;
            Fov = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(Player))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(Player.Transform.position);

            if (Player.PlayerBones != null)
                headScreenPosition = GameUtils.WorldPointToScreenPoint(Player.PlayerBones.Head.position);

            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
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
            Vector3 normalized = (Player.Transform.position - myPos).normalized;
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
        }
    }

}