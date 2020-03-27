using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EFT;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using UnityEngine;

namespace EFT.Reborn
{
    public class LootableContainerESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 10f;
        private float _nextLootContainerCacheTime;
        private List<GameLootContainer> _gameLootContainers;

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
                    if ((Main.GameWorld != null) && (Main.GameWorld.LootItems != null) && GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
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
                int x = -20;

                if (Settings.DrawLootableContainers && (GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && (Main.GameWorld.ExfiltrationController.ExfiltrationPoints != null)) && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                {

                    foreach (var gameLootContainer in _gameLootContainers)
                    {
                        if (!GameUtils.IsLootableContainerValid(gameLootContainer.LootableContainer) || !gameLootContainer.IsOnScreen || gameLootContainer.Distance > Settings.DrawLootableContainersDistance)
                            continue;

                        EFT.InventoryLogic.Item item = gameLootContainer.LootableContainer.ItemOwner.RootItem;

                        if (!Settings.DrawEmptyContainers)
                        {
                            if (item.GetAllItems().Count() == 1)
                                continue;
                        }

                        string lootItemName = item.Name.Localized();

                        if (Settings.DrawContainersContent)
                        {
                            foreach (var allItem in item.GetAllItems())
                            {
                                if (item.GetAllItems().First() == allItem)
                                {
                                    lootItemName = $"{allItem.Name.Localized()} [{gameLootContainer.FormattedDistance}]";
                                    Settings.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                                }
                                else
                                {
                                    lootItemName = allItem.Name.Localized();
                                    Settings.LootableContainerColor = Color.white;
                                }
                                Render.DrawString(new Vector2(gameLootContainer.ScreenPosition.x, gameLootContainer.ScreenPosition.y - x), lootItemName, Settings.LootableContainerColor);
                                x -= 20;
                            }
                        }
                        else
                        {
                            Render.DrawString(new Vector2(gameLootContainer.ScreenPosition.x, gameLootContainer.ScreenPosition.y - x), lootItemName, Settings.LootableContainerColor);
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