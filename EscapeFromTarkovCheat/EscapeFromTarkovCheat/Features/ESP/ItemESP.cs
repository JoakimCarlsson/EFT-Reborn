using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EFT;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using UnityEngine;

namespace EFT.HideOut
{
    public class ItemESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 1.5f;
        private float _nextLootItemCacheTime;



        private List<GameLootItem> _gameLootItems = new List<GameLootItem>();
        public static List<string> SpecialLootItems;
        public LootItemRarity LootItemRarity { get; private set; }

        public void Start()
        {
            SpecialLootItems = new List<string>
            {
                "LEDX",
                "Red",
                "Paracord",
                "Keycard",
                "Virtex",
                "Defibrillator",
                "0.2BTC",
                "Prokill",
                "Flash drive",
                "Violet",
                "Blue",
                "RB - PSP2",
                "RB - MP22",
                "RB - GN",
                "RR",
                "T - 7",
                "Green",
                "San.301",
                "Tetriz",
            };
        }

        public void FixedUpdate()
        {
            try
            {
                if (!Settings.DrawLootItems)
                    return;

                if (((int)LootItemRarity) == (Enum.GetNames(typeof(LootItemRarity)).Length - 1))
                    LootItemRarity = LootItemRarity.Common;
                else if (Input.GetKeyUp(Settings.ItemCategory))
                    LootItemRarity++;

                if (Time.time >= _nextLootItemCacheTime)
                {
                    if ((Main.GameWorld != null) && (Main.GameWorld.LootItems != null) && GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                    {
                        _gameLootItems.Clear();

                        for (int i = 0; i < Main.GameWorld.LootItems.Count; i++)
                        {
                            LootItem lootItem = Main.GameWorld.LootItems.GetByIndex(i);

                            if (!GameUtils.IsLootItemValid(lootItem) || (Vector3.Distance(Main.MainCamera.transform.position, lootItem.transform.position) > Settings.DrawLootItemsDistance))
                                continue;

                            _gameLootItems.Add(new GameLootItem(lootItem));
                        }

                        _nextLootItemCacheTime = (Time.time + CacheLootItemsInterval);
                    }
                }

                foreach (GameLootItem gameLootItem in _gameLootItems)
                    gameLootItem.RecalculateDynamics();
            }
            catch
            {
            }


        }

        private void OnGUI()
        {
            try
            {
                if (Settings.DrawLootItems && (GameScene.IsLoaded() && GameScene.InMatch() && Main.LocalPlayer != null && (Main.GameWorld.ExfiltrationController.ExfiltrationPoints != null)) && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.MainCamera != null)
                {
                    foreach (var item in _gameLootItems)
                    {
                        if (!GameUtils.IsLootItemValid(item.LootItem) || !item.IsOnScreen || item.Distance > Settings.DrawLootItemsDistance)
                            continue;

                        bool isSpecialLootItem = IsSpecialLootItem(item.LootItem);

                        Color lootItemColor = Settings.CommonColor;

                        if (isSpecialLootItem)
                            lootItemColor = Settings.SpecialColor;
                        else if (item.LootItem.Item.QuestItem)
                            lootItemColor = Settings.QuestColor;
                        else if (item.LootItem.Item.Template.Rarity == ELootRarity.Rare)
                            lootItemColor = Settings.RareColor;
                        else if (item.LootItem.Item.Template.Rarity == ELootRarity.Superrare)
                            lootItemColor = Settings.SuperRareColor;

                        string lootItemName = $"{item.LootItem.Item.ShortName.Localized()} [{item.FormattedDistance}]";

                        if (LootItemRarity == LootItemRarity.Special && !isSpecialLootItem)
                            continue;
                        if (LootItemRarity == LootItemRarity.GameRare && item.LootItem.Item.Template.Rarity != ELootRarity.Rare)
                            continue;
                        if (LootItemRarity == LootItemRarity.GameSuperRare && item.LootItem.Item.Template.Rarity != ELootRarity.Superrare)
                            continue;
                        if (LootItemRarity == LootItemRarity.Common && item.LootItem.Item.Template.Rarity != ELootRarity.Common)
                            continue;
                        if (LootItemRarity == LootItemRarity.Common && item.LootItem.Item.Template.Rarity != ELootRarity.Common)
                            continue;
                        Render.DrawString(new Vector2(item.ScreenPosition.x - 50f, item.ScreenPosition.y), lootItemName, lootItemColor);
                    }

                    GUI.Label(new Rect(20, 40, 200, 60), LootItemRarity.ToString());
                }
            }
            catch
            {
                
            }
        }

        public bool IsSpecialLootItem(LootItem lootItem)
        {
                if ((lootItem == null) || (lootItem.Item == null))
                    return false;

                string formattedLootItemName = lootItem.Item.Name.Localized();
                string formattedLootItemShortName = lootItem.Item.ShortName.Localized();

                return SpecialLootItems.Contains(formattedLootItemName) || SpecialLootItems.Contains(formattedLootItemShortName);

        }
    }
}