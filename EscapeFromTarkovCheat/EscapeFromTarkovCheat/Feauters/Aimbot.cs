using System;
using System.Collections.Generic;
using System.Linq;
using EFT;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters
{
    class Aimbot : MonoBehaviour
    {
        private IEnumerable<GamePlayer> _targetList;
        public void Update()
        {
            if ((Main.GameWorld != null))
            {
                if (Settings.NoRecoil)
                    NoRecoil();

                if (Settings.Aimbot && Input.GetKey(Settings.AimbotKey))
                    Aim();
            }
        }

        private void Aim()
        {
            //We make a new list of targets and filter out our LocalPlayer, we can also choose to filter out players that is on our team.
            _targetList = Main.GamePlayers.Where(p => !p.Player.IsYourPlayer());

            foreach (var gamePlayer in _targetList)
            {
                
            }
        }

        private void NoRecoil()
        {
            if (Main.LocalPlayer == null)
                return;

            Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
        }
    }
}
