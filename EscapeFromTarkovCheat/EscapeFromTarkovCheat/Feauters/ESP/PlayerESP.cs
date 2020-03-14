using System;
using System.Collections.Generic;
using System.Diagnostics;
using EFT;
using EFT.HealthSystem;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters.ESP
{


    public class PlayerESP : MonoBehaviour
    {
        private static readonly Color _playerColor = Color.green;
        private static readonly Color _deadPlayerColor = Color.gray;
        private static readonly Color _botColor = Color.yellow;
        private static readonly Color _healthColor = Color.green;
        private static readonly Color _bossColor = Color.red;

        public void OnGUI()
        {
            if (!Settings.DrawPlayers)
                return;

            foreach (GamePlayer gamePlayer in Main.GamePlayers)
            {
                if (!gamePlayer.IsOnScreen || gamePlayer.Distance > Settings.DrawPlayersDistance || gamePlayer.Player == Main.LocalPlayer)
                    continue;

                Color playerColor = ((gamePlayer.IsAI) ? _botColor : _playerColor);

                float boxPositionY = (gamePlayer.HeadScreenPosition.y - 10f);
                float boxHeight = (Math.Abs(gamePlayer.HeadScreenPosition.y - gamePlayer.ScreenPosition.y) + 10f);
                float boxWidth = (boxHeight * 0.65f);

                if (Settings.DrawPlayerBox && GameUtils.IsPlayerAlive(gamePlayer.Player))
                {
                    Render.DrawBox((gamePlayer.ScreenPosition.x - (boxWidth / 2f)), boxPositionY, boxWidth, boxHeight, playerColor);
                }

                if (Settings.DrawPlayerHealth && GameUtils.IsPlayerAlive(gamePlayer.Player)) 
                {
                    if (gamePlayer.Player.HealthController.IsAlive)
                    {
                        float currentPlayerHealth = gamePlayer.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;
                        float maximumPlayerHealth = gamePlayer.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Maximum;

                        float healthBarHeight = GameUtils.Map(currentPlayerHealth, 0f, maximumPlayerHealth, 0f, boxHeight);
                        Render.DrawLine(new Vector2((gamePlayer.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight - healthBarHeight)), new Vector2((gamePlayer.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight)), 3f, _healthColor);
                    }
                }


                if (Settings.DrawPlayerName)
                {
                    string playerText = "";

                    if (gamePlayer.Player.Profile.Info.Settings.IsBoss())
                    {
                        playerText = $"Boss [{gamePlayer.FormattedDistance}]";
                        playerColor = _bossColor;
                    }
                    else if (gamePlayer.IsAI)
                    {
                        playerText = $"Bot [{gamePlayer.FormattedDistance}]";
                        playerColor = _botColor;
                    }
                    else
                    {
                        playerText = $"{gamePlayer.Player.Profile.Info.Nickname} [{gamePlayer.FormattedDistance}]";
                        playerColor = _playerColor;
                    }
                    if (!GameUtils.IsPlayerAlive(gamePlayer.Player))
                    {
                        playerText = $"*DEAD* [{gamePlayer.FormattedDistance}]";
                        playerColor = _deadPlayerColor;
                    }

                    var playerTextVector = GUI.skin.GetStyle(playerText).CalcSize(new GUIContent(playerText));
                    Render.DrawString(new Vector2(gamePlayer.ScreenPosition.x - (playerTextVector.x / 2f), (gamePlayer.HeadScreenPosition.y - 20f)), playerText, playerColor);
                }

                if (Settings.DrawPlayerLine && GameUtils.IsPlayerAlive(gamePlayer.Player))
                {
                    Render.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(gamePlayer.ScreenPosition.x, gamePlayer.ScreenPosition.y), 1.5f, gamePlayer.IsVisible ? Color.green : Color.red);
                }
            }
        }
    }
}