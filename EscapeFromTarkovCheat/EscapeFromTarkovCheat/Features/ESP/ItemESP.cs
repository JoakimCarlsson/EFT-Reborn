using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EFT;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using UnityEngine;

namespace EFT.Reborn
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
"LedX",
"Virtex",
"VPX",
"Bitcoin",
"intelligence",
"reap-ir",
"defibulator",
"opthomalscope",
"Green keycard",
"blue keycard",
"black keycard",
"red keycard",
"Ultra medical storage key",
"11SR keycard",
"RB-ps81 key",
"T-7 thermal goggles",
"RB-GN",
"RB-MP22",
"Red Rebel Ice pick",
"RB-op",
"RB-PSP2",
"RB-KSM",
"RB-KORL",
"RB-AK",
"Key to kiba store outlet",
"RB-RS",
"RB-OB",
"RB-ORB2",
"21ws Keycard",
"Goshan Cash register key",
"HEP storage Station",
"USEC stash on customs key",
"Shturman key",
"RB-ST Key",
"Labs managers office key",
"Graphics card",
"2nd to top tier",
"All Stims",
"dry fuel - 590a373286f774287540368b",
"fuel conditioner",
"Beard oil",
"klean flame",
"firesteel",
"West wing room 219",
"West wing room 222",
"West wing room 301",
"health resort management office safe key",
"East wing room 313",
"RB-AO",
"East wing room 216",
"Office 112",
"East wing room 206",
"military base checkpoint key",
"Labs acces keycard",
"East wing room 222",
"east wing room 226",
"Factory exit key  ",
"West Wing Room 218",
"East wing room 205",
"West wing room 221 key",
"RB-RH key",
"Bitcoin",
"West wing 216 key",
"Weapons case",
"Money case",
"Documents case",
"Health resort warehouse safe key",
"Prokill",
"West wing room 303 key",
"Dry fuel",
"SSD Drive",
"Items Case",
"KIBA outlet grate door",
"RB-AM",
"RFID",
"Tetriz",
"Roler",
"Military thermal vision module Iridium",
"Paracord",
"OFZ 30x160mm shell",
"Grenade case",
"Military COFDM wireless Signal Transmitter",
"SI Advanced receiver extension buffer tube (anodized red)",
"FLIR RS-32 2.25-9x 35mm 60Hz thermal riflescope",
"Golden Star Balm",
"Gold skull ring",
"Military power filter",
"Golden 1GPhone",
"Phased array element",
"Pressure gauge",
"Ammo case",
"6-STEN-140-M military battery",
"Military circuit board",
"Analog thermometer",
"KEKTAPE duct tape",
"GP coin",
"Battered antique Book",
"Golden neck chain",
"B&T Rotex 2 4.6x30 silencer",
"Daniel Defence Wave QD Sound Suppressor",
"Surefire SOCOM556-MONSTER 5.56x45 silencer",
"Gemtech ONE 7.62x51 Sound Suppressor",
"Rotor 43 7.62x54 muzzle brake",
"NIXXOR lens",
"Broken GPhone X",
"Bronze lion",
"Bottle of Dan Jackiel Whiskey",
"Condensed milk",
"Military gyrotachometer",
"Golden rooster",
"Antique teapot",
"Pestily plague mask",
"FP-100 filter absorber",
"Military cable",
"Wooden clock",
"Bottle of vodka Tarkovskaya",





            }.ConvertAll(x => x.ToLower());
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

            string formattedLootItemName = lootItem.Item.Name.Localized().ToLower();
            string formattedLootItemShortName = lootItem.Item.ShortName.Localized().ToLower();

            return SpecialLootItems.Contains(formattedLootItemName) || SpecialLootItems.Contains(formattedLootItemShortName);
        }
    }
}