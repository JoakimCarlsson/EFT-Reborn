using System;
using System.Collections.Generic;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace EFT.Reborn
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
        private static readonly float _cachePlayersInterval = 10f;
        private static readonly float _cacheCameraInterval = 10f;
        private IEnumerator _coroutineUpdateMain;
        public void Awake()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Menu>();
            HookObject.AddComponent<PlayerESP>();
            HookObject.AddComponent<ItemESP>();
            HookObject.AddComponent<LootableContainerESP>();
            HookObject.AddComponent<ExfiltrationPointsESP>();
            HookObject.AddComponent<Aimbot>();
            HookObject.AddComponent<GrenadeESP>();
            HookObject.AddComponent<Misc>();
            DontDestroyOnLoad(HookObject);
            GameScene.CurrentGameScene = new Scene();

        }

        public void Start()
        {
            _coroutineUpdateMain = UpdateMain(10f);
            StartCoroutine(_coroutineUpdateMain);
        }

        public IEnumerator UpdateMain(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                try
                {
                    GameScene.GetScene();
                    if (Time.time >= _nextCameraCacheTime && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                    {
                        GameWorld = Singleton<GameWorld>.Instance;
                        MainCamera = Camera.main;
                        _nextCameraCacheTime = (Time.time + _cacheCameraInterval);

                        foreach (var player in GameWorld.RegisteredPlayers)
                        {
                            if (player.IsYourPlayer())
                            {
                                LocalPlayer = player;
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        private void FixedUpdate()
        {
            try
            {
                if (ShouldUpdate() && Settings.DrawPlayers)
                {
                    if (Time.time >= _nextPlayerCacheTime)
                    {
                        if ((GameWorld != null) && (GameWorld.RegisteredPlayers != null))
                        {
                            GamePlayers.Clear();

                            foreach (Player player in GameWorld.RegisteredPlayers)
                            {
                                GamePlayers.Add(new GamePlayer(player));
                            }

                            _nextPlayerCacheTime = (Time.time + _cachePlayersInterval);
                        }
                    }

                    foreach (GamePlayer gamePlayer in GamePlayers)
                        gamePlayer.RecalculateDynamics();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        internal static bool ShouldUpdate()
        {
            //We don't need this many checks.
            return GameScene.IsLoaded() && GameScene.InMatch() && LocalPlayer != null && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MainCamera != null;
        }
    }
}