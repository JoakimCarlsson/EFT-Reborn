using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EFT;
using EFT.Interactive;
using JsonType;
using UnityEngine;

namespace EFT.HideOut
{
    public class ItemESP : MonoBehaviour
    {
        private static readonly float CacheLootItemsInterval = 4f;
        private float _nextLootItemCacheTime;

        private static readonly Color SpecialColor = new Color(1f, 0.2f, 0.09f);
        private static readonly Color QuestColor = Color.yellow;
        private static readonly Color CommonColor = Color.white;
        private static readonly Color RareColor = new Color(0.38f, 0.43f, 1f);
        private static readonly Color SuperRareColor = new Color(1f, 0.29f, 0.36f);

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
                else if (Input.GetKeyDown(Settings.ItemCategory))
                    LootItemRarity++;

                if (Time.time >= _nextLootItemCacheTime)
                {
                    if ((Main.GameWorld != null) && (Main.GameWorld.LootItems != null))
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
                if (Settings.DrawLootItems)
                {
                    foreach (var item in _gameLootItems)
                    {
                        if (!GameUtils.IsLootItemValid(item.LootItem) || !item.IsOnScreen || item.Distance > Settings.DrawLootItemsDistance)
                            continue;

                        bool isSpecialLootItem = IsSpecialLootItem(item.LootItem);

                        Color lootItemColor = CommonColor;

                        if (isSpecialLootItem)
                            lootItemColor = SpecialColor;
                        else if (item.LootItem.Item.QuestItem)
                            lootItemColor = QuestColor;
                        else if (item.LootItem.Item.Template.Rarity == ELootRarity.Rare)
                            lootItemColor = RareColor;
                        else if (item.LootItem.Item.Template.Rarity == ELootRarity.Superrare)
                            lootItemColor = SuperRareColor;

                        string lootItemName = $"{item.LootItem.Item.ShortName.Localized()} [{item.FormattedDistance}]";

                        if (LootItemRarity == LootItemRarity.Special && !isSpecialLootItem)
                            continue;
                        if (LootItemRarity == LootItemRarity.GameRare && item.LootItem.Item.Template.Rarity != ELootRarity.Rare)
                            continue;
                        if (LootItemRarity == LootItemRarity.GameSuperRare && item.LootItem.Item.Template.Rarity != ELootRarity.Superrare)
                            continue;
                        if (LootItemRarity == LootItemRarity.Common && item.LootItem.Item.Template.Rarity != ELootRarity.Common)
                            continue;

                        GUI.Label(new Rect(20, 40, 200, 60), LootItemRarity.ToString());
                        Render.DrawString(new Vector2(item.ScreenPosition.x - 50f, item.ScreenPosition.y), lootItemName, lootItemColor);
                    }
                }
            }
            catch
            {
            }
        }

        public bool IsSpecialLootItem(LootItem lootItem)
        {
            try
            {
                if ((lootItem == null) || (lootItem.Item == null))
                    return false;

                string formattedLootItemName = lootItem.Item.Name.Localized();
                string formattedLootItemShortName = lootItem.Item.ShortName.Localized();

                return SpecialLootItems.Contains(formattedLootItemName) || SpecialLootItems.Contains(formattedLootItemShortName);
            }
            catch 
            {
            }
        }
    }
}