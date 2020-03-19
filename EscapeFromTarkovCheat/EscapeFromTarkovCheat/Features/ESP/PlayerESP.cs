using System;
using System.Collections.Generic;
using System.Diagnostics;
using EFT;
using EFT.HealthSystem;
using EFT.Interactive;
using EFT.UI;
using UnityEngine;

namespace EFT.HideOut
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
            try
            {
                if (Settings.DrawPlayers && GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                {
                    foreach (GamePlayer player in Main.GamePlayers)
                    {
                        if (!player.IsOnScreen || player.Distance > Settings.DrawPlayersDistance || player.Player == Main.LocalPlayer)
                            continue;

                        if (true)
                        {
                            Color playerColor = ((player.IsAI) ? _botColor : _playerColor);

                            float boxPositionY = (player.HeadScreenPosition.y - 10f);
                            float boxHeight = (Math.Abs(player.HeadScreenPosition.y - player.ScreenPosition.y) + 10f);
                            float boxWidth = (boxHeight * 0.65f);

                            if (Settings.DrawPlayerBox && GameUtils.IsPlayerAlive(player.Player))
                            {
                                Render.DrawBox((player.ScreenPosition.x - (boxWidth / 2f)), boxPositionY, boxWidth, boxHeight, playerColor);
                            }

                            if (Settings.DrawPlayerHealth && GameUtils.IsPlayerAlive(player.Player))
                            {
                                if (player.Player.HealthController.IsAlive)
                                {
                                    float currentPlayerHealth = player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;
                                    float maximumPlayerHealth = player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Maximum;

                                    float healthBarHeight = GameUtils.Map(currentPlayerHealth, 0f, maximumPlayerHealth, 0f, boxHeight);
                                    Render.DrawLine(new Vector2((player.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight - healthBarHeight)), new Vector2((player.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight)), 3f, _healthColor);
                                }
                            }


                            if (Settings.DrawPlayerName)
                            {
                                string playerText;
                                if (player.Player.Profile.Info.Settings.IsBoss())
                                {
                                    playerText = $"Boss [{player.FormattedDistance}]";
                                    playerColor = _bossColor;
                                }
                                else if (player.IsAI)
                                {
                                    playerText = $"Bot [{player.FormattedDistance}]";
                                    playerColor = _botColor;
                                }
                                else
                                {
                                    playerText = $"{player.Player.Profile.Info.Nickname} [{player.FormattedDistance}]";
                                    playerColor = _playerColor;
                                }
                                if (!GameUtils.IsPlayerAlive(player.Player))
                                {
                                    playerText = $"*DEAD* [{player.FormattedDistance}]";
                                    playerColor = _deadPlayerColor;
                                }

                                var playerTextVector = GUI.skin.GetStyle(playerText).CalcSize(new GUIContent(playerText));
                                Vector3 boundingVector = Camera.main.WorldToScreenPoint(player.Player.Transform.position);
                                var playerHeadVector = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.Head.position);
                                float boxVectorY = playerHeadVector.y + 10f;

                                Render.DrawLabel(new Rect(boundingVector.x - playerTextVector.x / 2f, Screen.height - boxVectorY - 20f, 300f, 50f), playerText, playerColor);
                            }

                            if (Settings.DrawPlayerLine && GameUtils.IsPlayerAlive(player.Player))
                            {
                                Render.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(player.ScreenPosition.x, player.ScreenPosition.y), 1.5f, Aimbot.IsVisible(GameUtils.GetBonePosByID(player.Player, 132)) ? Color.green : Color.red);
                            }

                            if (Settings.DrawPlayerSkeleton && GameUtils.IsPlayerAlive(player.Player) && player.Distance < 100f)
                            {
                                var rightPalm = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.RightPalm.position);
                                var leftPalm = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.LeftPalm.position);
                                var leftShoulder = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.LeftShoulder.position);
                                var rightShoulder = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.RightShoulder.position);
                                var neck = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.Neck.position);
                                var pelvis = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.Pelvis.position);
                                var rightFoot = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.KickingFoot.position);
                                var leftFoot = Main.MainCamera.WorldToScreenPoint(GameUtils.GetBonePosByID(player.Player, 18));
                                var pllBowVect = Main.MainCamera.WorldToScreenPoint(GameUtils.GetBonePosByID(player.Player, 91));
                                var plrBowVect = Main.MainCamera.WorldToScreenPoint(GameUtils.GetBonePosByID(player.Player, 112));
                                var pllKneeVect = Main.MainCamera.WorldToScreenPoint(GameUtils.GetBonePosByID(player.Player, 17));
                                var plrKneeVect = Main.MainCamera.WorldToScreenPoint(GameUtils.GetBonePosByID(player.Player, 22));

                                Render.DrawLine(new Vector2(neck.x, Screen.height - neck.y), new Vector2(pelvis.x, Screen.height - pelvis.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(leftShoulder.x, Screen.height - leftShoulder.y), new Vector2(pllBowVect.x, Screen.height - pllBowVect.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(rightShoulder.x, Screen.height - rightShoulder.y), new Vector2(plrBowVect.x, Screen.height - plrBowVect.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(pllBowVect.x, Screen.height - pllBowVect.y), new Vector2(leftPalm.x, Screen.height - leftPalm.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(plrBowVect.x, Screen.height - plrBowVect.y), new Vector2(rightPalm.x, Screen.height - rightPalm.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(rightShoulder.x, Screen.height - rightShoulder.y), new Vector2(leftShoulder.x, Screen.height - leftShoulder.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(pllKneeVect.x, Screen.height - pllKneeVect.y), new Vector2(pelvis.x, Screen.height - pelvis.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(plrKneeVect.x, Screen.height - plrKneeVect.y), new Vector2(pelvis.x, Screen.height - pelvis.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(pllKneeVect.x, Screen.height - pllKneeVect.y), new Vector2(leftFoot.x, Screen.height - leftFoot.y), 1.5f, Color.white);
                                Render.DrawLine(new Vector2(plrKneeVect.x, Screen.height - plrKneeVect.y), new Vector2(rightFoot.x, Screen.height - rightFoot.y), 1.5f, Color.white);
                            }
                        }

                    }
                }
            }
            catch
            {

            }
        }
    }
}