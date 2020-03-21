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
        private static readonly Color _playerScavColor = new Color(241, 0, 35, 1);
        private static readonly Color _deadPlayerColor = Color.gray;
        private static readonly Color _botColor = Color.yellow;
        private static readonly Color _healthColor = Color.green;
        private static readonly Color _bossColor = Color.red;

        public void OnGUI()
        {
            try
            {
                if (!Main.ShouldUpdate())
                    return;

                if (Settings.DrawPlayers)
                    DrawPlayers();

                if (Settings.DrawScavs)
                    DrawScavs();
            }
            catch
            {

            }
        }

        private static void DrawPlayers()
        {
            foreach (GamePlayer player in Main.GamePlayers)
            {
                if (!player.IsOnScreen || player.Player.IsYourPlayer() || player.IsAI)
                    continue;

                if (player.Distance > Settings.DrawPlayersRange)
                    continue;

                Color playerColor;
                string playerTextLabel1 = string.Empty;
                string playerTextLabel2 = string.Empty;

                playerColor = player.Player.Profile.Info.Side == EPlayerSide.Savage ? _playerScavColor : _playerColor;

                if (!GameUtils.IsPlayerAlive(player.Player))
                {
                    playerColor = _deadPlayerColor;
                }

                float boxPositionY = (player.HeadScreenPosition.y - 10f);
                float boxHeight = (Math.Abs(player.HeadScreenPosition.y - player.ScreenPosition.y) + 10f);
                float boxWidth = (boxHeight * 0.65f);

                if (Settings.DrawPlayerName && GameUtils.IsPlayerAlive(player.Player))
                {
                    playerTextLabel1 = $"{player.Player.Profile.Info.Nickname} ]";
                }

                if (Settings.DrawPlayerCorpses && !GameUtils.IsPlayerAlive(player.Player))
                {
                    playerTextLabel1 = "* Dead * ";
                }

                if (Settings.DrawPlayerDistance)
                {
                    playerTextLabel1 += $" [{player.FormattedDistance}] ";
                }

                if (Settings.DrawPlayerHealth && GameUtils.IsPlayerAlive(player.Player))
                {
                    float currentPlayerHealth =
                        player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;
                    playerTextLabel1 += $"[HP: {(int)currentPlayerHealth}]";
                }

                if (Settings.DrawPlayerWeapon && GameUtils.IsPlayerAlive(player.Player))
                {
                    try
                    {
                        var weapon = player.Player.Weapon.GetRootItem();
                        string weaponName = weapon.ShortName.Localized();
                        var mag = weapon?.GetCurrentMagazine();

                        if (mag != null)
                        {
                            playerTextLabel2 = $"{weaponName} {mag.Count}/{mag.MaxCount} ";
                        }
                    }
                    catch
                    {
                        playerTextLabel2 = $"Unkown Weapon ";
                    }
                }

                if (Settings.DrawPlayerLevel && GameUtils.IsPlayerAlive(player.Player))
                {
                    playerTextLabel2 += $"Lvl: {player.Player.Profile.Info.Level}";
                }

                var playerTextVectorLabel1 = GUI.skin.GetStyle(playerTextLabel1).CalcSize(new GUIContent(playerTextLabel1));
                var playerTextVectorLabel2 = GUI.skin.GetStyle(playerTextLabel2).CalcSize(new GUIContent(playerTextLabel2));
                Vector3 boundingVector = Camera.main.WorldToScreenPoint(player.Player.Transform.position);
                var playerHeadVector = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.Head.position);
                float boxVectorY = playerHeadVector.y + 10f;

                if (playerTextLabel1 != "")
                {
                    Render.DrawLabel(
                        new Rect(boundingVector.x - playerTextVectorLabel1.x / 2f, Screen.height - boxVectorY - 20f,
                            300f, 50f), playerTextLabel1, playerColor);
                }

                if (playerTextLabel2 != "")
                {
                    Render.DrawLabel(
                        new Rect(boundingVector.x - playerTextVectorLabel2.x / 2f, Screen.height - boundingVector.y,
                            300f, 50f), playerTextLabel2, playerColor);
                }

                if (Settings.DrawPlayerLine && GameUtils.IsPlayerAlive(player.Player))
                {
                    DrawSnapLine(player);
                }

                if (Settings.DrawPlayerSkeleton && GameUtils.IsPlayerAlive(player.Player) && player.Distance < Settings.DrawPlayerSkeletonDistance)
                {
                    DrawSkeleton(player);
                }

                if (Settings.DrawPlayerBox && GameUtils.IsPlayerAlive(player.Player))
                {
                    Render.DrawBox((player.ScreenPosition.x - (boxWidth / 2f)), boxPositionY, boxWidth, boxHeight, playerColor);
                }

                if (Settings.DrawPlayerHealthBar && GameUtils.IsPlayerAlive(player.Player))
                {
                    DrawHealthBar(player, boxHeight, boxWidth, boxPositionY);
                }
            }
        }

        private void DrawScavs()
        {
            foreach (GamePlayer player in Main.GamePlayers)
            {
                if (!player.IsOnScreen || player.Distance > Settings.DrawScavRange || player.Player.IsYourPlayer() || !player.IsAI)
                    continue;

                string playerTextLabel1 = string.Empty;
                string playerTextLabel2 = string.Empty;

                Color playerColor;

                playerColor = player.Player.Profile.Info.Settings.IsBoss() ? _bossColor : _botColor;

                if (!GameUtils.IsPlayerAlive(player.Player))
                {
                    playerColor = _deadPlayerColor;
                }

                float boxPositionY = (player.HeadScreenPosition.y - 10f);
                float boxHeight = (Math.Abs(player.HeadScreenPosition.y - player.ScreenPosition.y) + 10f);
                float boxWidth = (boxHeight * 0.65f);

                if (Settings.DrawScavName && GameUtils.IsPlayerAlive(player.Player))
                {
                    if (player.Player.Profile.Info.Settings.IsBoss())
                    {
                        playerTextLabel1 = $"Boss ";
                        playerColor = _bossColor;
                    }
                    else
                    {
                        playerTextLabel1 = "Bot ";
                        playerColor = _botColor;
                    }
                }

                if (Settings.DrawScavCorpses && !GameUtils.IsPlayerAlive(player.Player))
                {
                    playerTextLabel1 = "* Dead *";
                    playerColor = _deadPlayerColor;
                }

                if (Settings.DrawScavDistance)
                {
                    playerTextLabel1 += $"[{player.FormattedDistance}] ";
                }

                if (Settings.DrawScavHealth && GameUtils.IsPlayerAlive(player.Player))
                {
                    float currentPlayerHealth = player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;
                    playerTextLabel1 += $"[HP: {(int)currentPlayerHealth}]";
                }

                if (Settings.DrawScavWeapon && GameUtils.IsPlayerAlive(player.Player))
                {
                    try
                    {
                        var weapon = player.Player.Weapon.GetRootItem();
                        string weaponName = weapon.ShortName.Localized();
                        var mag = weapon?.GetCurrentMagazine();

                        if (mag != null)
                        {
                            playerTextLabel2 = $"{weaponName} {mag.Count}/{mag.MaxCount} ";
                        }
                    }
                    catch
                    {
                        playerTextLabel2 = $"Unkown Weapon ";
                    }
                }

                var playerTextVectorLabel1 = GUI.skin.GetStyle(playerTextLabel1).CalcSize(new GUIContent(playerTextLabel1));
                var playerTextVectorLabel2 = GUI.skin.GetStyle(playerTextLabel2).CalcSize(new GUIContent(playerTextLabel2));
                Vector3 boundingVector = Camera.main.WorldToScreenPoint(player.Player.Transform.position);
                var playerHeadVector = Main.MainCamera.WorldToScreenPoint(player.Player.PlayerBones.Head.position);
                float boxVectorY = playerHeadVector.y + 10f;

                if (playerTextLabel1 != "")
                {
                    Render.DrawLabel(new Rect(boundingVector.x - playerTextVectorLabel1.x / 2f, Screen.height - boxVectorY - 20f, 300f, 50f), playerTextLabel1, playerColor);
                }

                if (playerTextLabel2 != "")
                {
                    Render.DrawLabel(new Rect(boundingVector.x - playerTextVectorLabel2.x / 2f, Screen.height - boundingVector.y, 300f, 50f), playerTextLabel2, playerColor);
                }

                if (Settings.DrawScavLine && GameUtils.IsPlayerAlive(player.Player))
                {
                    DrawSnapLine(player);
                }

                if (Settings.DrawScavSkeleton && GameUtils.IsPlayerAlive(player.Player) && player.Distance < Settings.DrawScavSkeletonDistance)
                {
                    DrawSkeleton(player);
                }

                if (Settings.DrawScavBox && GameUtils.IsPlayerAlive(player.Player))
                {
                    Render.DrawBox((player.ScreenPosition.x - (boxWidth / 2f)), boxPositionY, boxWidth, boxHeight, playerColor);
                }

                if (Settings.DrawScavHealthBar && GameUtils.IsPlayerAlive(player.Player))
                {
                    DrawHealthBar(player, boxHeight, boxWidth, boxPositionY);
                }
            }
        }

        private static void DrawHealthBar(GamePlayer player, float boxHeight, float boxWidth, float boxPositionY)
        {
            float currentPlayerHealth = player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Current;
            float maximumPlayerHealth = player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common).Maximum;

            float healthBarHeight = GameUtils.Map(currentPlayerHealth, 0f, maximumPlayerHealth, 0f, boxHeight);
            Render.DrawLine(new Vector2((player.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight - healthBarHeight)), new Vector2((player.ScreenPosition.x - (boxWidth / 2f) - 3f), (boxPositionY + boxHeight)), 3f, _healthColor);
        }

        private static void DrawSkeleton(GamePlayer player)
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

            Render.DrawLine(new Vector2(neck.x, Screen.height - neck.y), new Vector2(pelvis.x, Screen.height - pelvis.y), 1.5f,
                Color.white);
            Render.DrawLine(new Vector2(leftShoulder.x, Screen.height - leftShoulder.y),
                new Vector2(pllBowVect.x, Screen.height - pllBowVect.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(rightShoulder.x, Screen.height - rightShoulder.y),
                new Vector2(plrBowVect.x, Screen.height - plrBowVect.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(pllBowVect.x, Screen.height - pllBowVect.y),
                new Vector2(leftPalm.x, Screen.height - leftPalm.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(plrBowVect.x, Screen.height - plrBowVect.y),
                new Vector2(rightPalm.x, Screen.height - rightPalm.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(rightShoulder.x, Screen.height - rightShoulder.y),
                new Vector2(leftShoulder.x, Screen.height - leftShoulder.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(pllKneeVect.x, Screen.height - pllKneeVect.y),
                new Vector2(pelvis.x, Screen.height - pelvis.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(plrKneeVect.x, Screen.height - plrKneeVect.y),
                new Vector2(pelvis.x, Screen.height - pelvis.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(pllKneeVect.x, Screen.height - pllKneeVect.y),
                new Vector2(leftFoot.x, Screen.height - leftFoot.y), 1.5f, Color.white);
            Render.DrawLine(new Vector2(plrKneeVect.x, Screen.height - plrKneeVect.y),
                new Vector2(rightFoot.x, Screen.height - rightFoot.y), 1.5f, Color.white);
        }

        private static void DrawSnapLine(GamePlayer player)
        {
            Render.DrawLine(new Vector2(Screen.width / 2, Screen.height),
                new Vector2(player.ScreenPosition.x, player.ScreenPosition.y), 1.5f,
                Aimbot.IsVisible(GameUtils.GetBonePosByID(player.Player, 132)) ? Color.green : Color.red);
        }


    }
}