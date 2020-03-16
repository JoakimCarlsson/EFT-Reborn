using System;
using System.Collections.Generic;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EFT.HideOut
{
    class Main : MonoBehaviour
    {
        public static List<GamePlayer> GamePlayers = new List<GamePlayer>();
        public static Player LocalPlayer;
        public static GameWorld GameWorld;
        public static Camera MainCamera;
        public static GameObject HookObject;
        private float _nextPlayerCacheTime;
        private float _nextCameraCacheTime;
        private static readonly float _cachePlayersInterval = 5f;
        private static readonly float _cacheCameraInterval = 10f;

        public void Awake()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Menu>();
            HookObject.AddComponent<PlayerESP>();
            HookObject.AddComponent<ItemESP>();
            HookObject.AddComponent<LootableContainerESP>();
            HookObject.AddComponent<ExfiltrationPointsESP>();
            HookObject.AddComponent<Aimbot>();
            HookObject.AddComponent<Misc>();
            DontDestroyOnLoad(HookObject);
            GameScene.CurrentGameScene = new Scene();
        }

        public void FixedUpdate()
        {
            try
            {
                GameScene.GetScene();
                if (Time.time >= _nextCameraCacheTime)
                {
                    GameWorld = Singleton<GameWorld>.Instance;
                    MainCamera = Camera.main;

                    _nextCameraCacheTime = (Time.time + _cacheCameraInterval);
                }

                UpdatePlayers();
            }
            catch
            {
            }
        }

        private void UpdatePlayers()
        {
            try
            {
                if (Settings.DrawPlayers)
                {
                    if (Time.time >= _nextPlayerCacheTime)
                    {
                        if ((GameWorld != null) && (GameWorld.RegisteredPlayers != null))
                        {
                            GamePlayers.Clear();

                            foreach (Player player in FindObjectsOfType<Player>())
                            {
                                if (player.IsYourPlayer())
                                {
                                    LocalPlayer = player;
                                    continue;
                                }

                                if ((Vector3.Distance(MainCamera.transform.position, player.Transform.position) >
                                     Settings.DrawPlayersDistance))
                                    continue;

                                GamePlayers.Add(new GamePlayer(player));
                            }

                            _nextPlayerCacheTime = (Time.time + _cachePlayersInterval);
                        }
                    }

                    foreach (GamePlayer gamePlayer in GamePlayers)
                        gamePlayer.RecalculateDynamics();
                }
            }
            catch 
            {
                
            }

        }
    }
}
