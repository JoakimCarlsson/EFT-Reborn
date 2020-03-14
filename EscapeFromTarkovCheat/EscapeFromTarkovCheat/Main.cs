﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Feauters;
using EscapeFromTarkovCheat.Feauters.ESP;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat
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
            hookObject.AddComponent<Menu.UI.Menu>();
            hookObject.AddComponent<PlayerESP>();
            hookObject.AddComponent<ItemESP>();
            hookObject.AddComponent<LootableContainerESP>();
            hookObject.AddComponent<ExfiltrationPointsESP>();
            hookObject.AddComponent<Aimbot>();
            DontDestroyOnLoad(hookObject);
        }

        public void FixedUpdate()
        {
            if (Time.time >= _nextCameraCacheTime)
            {
                GameWorld = Singleton<GameWorld>.Instance;
                MainCamera = Camera.main;

                _nextCameraCacheTime = (Time.time + _cacheCameraInterval);
            }

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
                            if ((Vector3.Distance(MainCamera.transform.position, player.Transform.position) > Settings.DrawPlayersDistance))
                                continue;

                            GamePlayers.Add(new GamePlayer(player));
                        }

                        _nextPlayerCacheTime = (Time.time + _cachePlayersInterval);
                    }
                }

                foreach (GamePlayer gamePlayer in GamePlayers)
                    gamePlayer.RecalculateDynamics();
            }

            if (Input.GetKeyDown(Settings.UnlockDoors))
            {
                foreach (var door in FindObjectsOfType<Door>())
                {
                    if (door.DoorState == EDoorState.Open || Vector3.Distance(door.transform.position, LocalPlayer.Position) > 20f)
                        continue;

                    door.DoorState = EDoorState.Shut;
                }
            }
        }
    }
}
