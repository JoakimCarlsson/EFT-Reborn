﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EFT;
using EFT.Interactive;
using JsonType;
using UnityEngine;

namespace EFT.HideOut
{
    public class LootableContainerESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 1.5f;
        private float _nextLootContainerCacheTime;
        private List<GameLootContainer> _gameLootContainers;
        private static readonly Color LootableContainerColor = new Color(1f, 0.2f, 0.09f);

        public void Start()
        {
            _gameLootContainers = new List<GameLootContainer>();
        }

        public void FixedUpdate()
        {
            try
            {
                if (!Settings.DrawLootableContainers)
                    return;
            
                if (Time.time >= _nextLootContainerCacheTime)
                {
                    if ((Main.GameWorld != null) && (Main.GameWorld.LootItems != null) && GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null)
                    {
                        _gameLootContainers.Clear();

                        foreach (LootableContainer lootableContainer in FindObjectsOfType<LootableContainer>())
                        {
                            if (!GameUtils.IsLootableContainerValid(lootableContainer) || (Vector3.Distance(Main.MainCamera.transform.position, lootableContainer.transform.position) > Settings.DrawLootableContainersDistance))
                                continue;
                            _gameLootContainers.Add(new GameLootContainer(lootableContainer));
                        }
                        _nextLootContainerCacheTime = (Time.time + CacheLootItemsInterval);
                    }
                }

                foreach (GameLootContainer gameLootContainer in _gameLootContainers)
                    gameLootContainer.RecalculateDynamics();
            }
            catch
            {

            }
        }

        public void OnGUI()
        {
            try
            {
                if (!Settings.DrawLootableContainers)
                    return;

                foreach (var gameLootContainer in _gameLootContainers)
                {
                    if (!GameUtils.IsLootableContainerValid(gameLootContainer.LootableContainer) || !gameLootContainer.IsOnScreen || gameLootContainer.Distance > Settings.DrawLootableContainersDistance)
                        continue;

                    EFT.InventoryLogic.Item rootItem = gameLootContainer.LootableContainer.ItemOwner.RootItem;

                    if (rootItem.GetAllItems().Count() == 1)
                        continue;

                    //foreach (var allItem in rootItem.GetAllItems())
                    //{
                    //    Console.WriteLine(allItem.Name.Localized());
                    //}

                    string lootItemName = $"{rootItem.Name.Localized()} [{gameLootContainer.FormattedDistance}]";
                    Render.DrawString(new Vector2(gameLootContainer.ScreenPosition.x - 50f, gameLootContainer.ScreenPosition.y), lootItemName, LootableContainerColor);
                }
            }
            catch
            {
                
            }
        }
    }
}