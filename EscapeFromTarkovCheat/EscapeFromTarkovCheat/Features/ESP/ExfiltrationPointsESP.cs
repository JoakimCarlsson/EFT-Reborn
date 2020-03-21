using System;
using System.Collections.Generic;
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
                    if ((GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && (Main.GameWorld.ExfiltrationController.ExfiltrationPoints != null)) && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                    {
                        _gameExfiltrationPoints.Clear();
                        foreach (var exfiltrationPoint in Main.GameWorld.ExfiltrationController.ExfiltrationPoints)
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