using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using JsonType;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feauters.ESP
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
        public LootItemLabelState LootItemLabelState { get; private set; }

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
            if (!Settings.DrawLootItems)
                return;

            if (((int)LootItemLabelState) == (Enum.GetNames(typeof(LootItemLabelState)).Length - 1))
                LootItemLabelState = LootItemLabelState.Common;
            else if (Input.GetKeyDown(Settings.ItemCategory))
                LootItemLabelState++;

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

        private void OnGUI()
        {
            if (Settings.DrawLootItems)
            {
                GUI.Label(new Rect(20, 20, 200, 60), LootItemLabelState.ToString());

                foreach (var gameLootItem in _gameLootItems)
                {
                    if (!GameUtils.IsLootItemValid(gameLootItem.LootItem) || !gameLootItem.IsOnScreen || gameLootItem.Distance > Settings.DrawLootItemsDistance)
                        continue;

                    bool isSpecialLootItem = IsSpecialLootItem(gameLootItem.LootItem);

                    if (LootItemLabelState == LootItemLabelState.Special && !isSpecialLootItem)
                        continue;
                    if (LootItemLabelState == LootItemLabelState.GameRare && gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Rare)
                        continue;
                    if (LootItemLabelState == LootItemLabelState.GameSuperRare && gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Superrare)
                        continue;

                    string lootItemName = $"{gameLootItem.LootItem.Item.ShortName.Localized()} [{gameLootItem.FormattedDistance}]";
                    Color lootItemColor = CommonColor;

                    if (isSpecialLootItem)
                        lootItemColor = SpecialColor;
                    else if (gameLootItem.LootItem.Item.QuestItem)
                        lootItemColor = QuestColor;
                    else if (gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Rare)
                        lootItemColor = RareColor;
                    else if (gameLootItem.LootItem.Item.Template.Rarity == ELootRarity.Superrare)
                        lootItemColor = SuperRareColor;

                    Render.DrawString(new Vector2(gameLootItem.ScreenPosition.x - 50f, gameLootItem.ScreenPosition.y), lootItemName, lootItemColor);
                }
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