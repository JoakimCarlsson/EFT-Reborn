using System;
using System.Collections.Generic;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using UnityEngine;

namespace EFT.HideOut
{
    class Main : MonoBehaviour
    {
        public static List<GamePlayer> GamePlayers = new List<GamePlayer>();
        public static Player LocalPlayer;
        public static GameWorld GameWorld;
        public static Camera MainCamera;
        public static GameObject hookObject;
        private float _nextPlayerCacheTime;
        private float _nextCameraCacheTime;
        private static readonly float _cachePlayersInterval = 5f;
        private static readonly float _cacheCameraInterval = 10f;

        public void Awake()
        {
            hookObject = new GameObject();
            hookObject.AddComponent<Menu>();
            hookObject.AddComponent<PlayerESP>();
            hookObject.AddComponent<ItemESP>();
            hookObject.AddComponent<LootableContainerESP>();
            hookObject.AddComponent<ExfiltrationPointsESP>();
            hookObject.AddComponent<Aimbot>();
            DontDestroyOnLoad(hookObject);
        }

        public void FixedUpdate()
        {
            try
            {
                if (Time.time >= _nextCameraCacheTime)
                {
                    GameWorld = Singleton<GameWorld>.Instance;
                    MainCamera = Camera.main;

                    _nextCameraCacheTime = (Time.time + _cacheCameraInterval);
                }

                UpdatePlayers();

                DoorUnlock();

                NoVisor();
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

        private static void NoVisor()
        {
            try
            {
                if (LocalPlayer == null || MainCamera == null)
                    return;

                if (Settings.NoVisor)
                {
                    MainCamera.GetComponent<VisorEffect>().Intensity = 0f;
                    MainCamera.GetComponent<VisorEffect>().enabled = true;
                }
                else
                {
                    MainCamera.GetComponent<VisorEffect>().Intensity = 1f;
                    MainCamera.GetComponent<VisorEffect>().enabled = true;
                }
            }
            catch
            {
                
            }
        }

        private static void DoorUnlock()
        {
            try
            {
                if (LocalPlayer == null || MainCamera == null)
                    return;

                if (Input.GetKeyDown(Settings.UnlockDoors))
                {
                    foreach (var door in FindObjectsOfType<Door>())
                    {
                        if (door.DoorState == EDoorState.Open ||
                            Vector3.Distance(door.transform.position, LocalPlayer.Position) > 20f)
                            continue;

                        door.DoorState = EDoorState.Shut;
                    }
                }
            }
            catch
            {
            }
        }
    }
}
