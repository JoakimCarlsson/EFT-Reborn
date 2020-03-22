using System;
using System.Collections.Generic;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using UnityEngine;

namespace EFT.HideOut
{
    public class ExfiltrationPointsESP : MonoBehaviour
    {
        private List<GameExfiltrationPoint> _gameExfiltrationPoints = new List<GameExfiltrationPoint>();
        private static readonly float CacheExfiltrationPointInterval = 1.5f;
        private float _nextLootItemCacheTime;

        private static readonly Color ExfiltrationPointColour = Color.green;

        public void FixedUpdate()
        {
            try
            {
                if (!Settings.DrawExfiltrationPoints)
                    return;

                if (Time.time >= _nextLootItemCacheTime)
                {
                    var world = Singleton<GameWorld>.Instance;
                    var waypointOfLife = world?.ExfiltrationController;
                    if (waypointOfLife?.ExfiltrationPoints == null)
                        return;

                    // Exfiltration points
                    var playerinfo = Main.LocalPlayer?.Profile;
                    var teamName = playerinfo.Info?.Side;

                    int scavMask = 0;
                    if (Main.LocalPlayer is ClientPlayer clientPlayer)
                        scavMask = clientPlayer.ScavExfilMask;
                    ExfiltrationPoint[] points;

                    if (teamName == EPlayerSide.Savage)
                        points = waypointOfLife.ScavExfiltrationClaim(scavMask, playerinfo.Id);
                    else
                        points = waypointOfLife.EligiblePoints(playerinfo);

                    if (Main.ShouldUpdate())
                    {
                        _gameExfiltrationPoints.Clear();
                        foreach (var exfiltrationPoint in points)
                        {
                            if (!GameUtils.IsExfiltrationPointValid(exfiltrationPoint))
                                continue;

                            _gameExfiltrationPoints.Add(new GameExfiltrationPoint(exfiltrationPoint));
                        }

                        _nextLootItemCacheTime = (Time.time + CacheExfiltrationPointInterval);
                    }
                }

                foreach (GameExfiltrationPoint gameExfiltrationPoint in _gameExfiltrationPoints)
                    gameExfiltrationPoint.RecalculateDynamics();
            }
            catch
            {
            }

        }

        private void OnGUI()
        {

            try
            {
                if (Settings.DrawExfiltrationPoints && (GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && (Main.GameWorld.ExfiltrationController.ExfiltrationPoints != null)) && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                {
                    foreach (var exfiltrationPoint in _gameExfiltrationPoints)
                    {
                        if (!GameUtils.IsExfiltrationPointValid(exfiltrationPoint.ExfiltrationPoint) || !exfiltrationPoint.IsOnScreen)
                            continue;

                        string exfiltrationPointText = $"{exfiltrationPoint.ExfiltrationPoint.Settings.Name} [{exfiltrationPoint.FormattedDistance}]";

                        Render.DrawString(new Vector2(exfiltrationPoint.ScreenPosition.x - 50f, exfiltrationPoint.ScreenPosition.y), exfiltrationPointText, ExfiltrationPointColour);
                    }
                }
            }
            catch
            {
            }
        }
    }
}