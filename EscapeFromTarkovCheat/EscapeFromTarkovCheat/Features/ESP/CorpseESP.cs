﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Interactive;
using EFT.Reborn;
using UnityEngine;

namespace EFT.Reborn
{
    class CorpseESP : MonoBehaviour
    {
        public void OnGUI()
        {
            try
            {
                if (Main.ShouldUpdate() && Settings.DrawPlayerCorpses)
                {

                    List<GInterface7>.Enumerator enumerator = Main.GameWorld.LootList.FindAll((GInterface7 item) => item is Corpse).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Corpse corpse = enumerator.Current as Corpse;
                        if (corpse != null && corpse.gameObject != null && corpse.isActiveAndEnabled)
                        {
                            Vector3 position = corpse.gameObject.transform.position;
                            float num = GameUtils.InPoint(Main.MainCamera.transform.position, position);
                            if (num <= Settings.DrawPlayersRange)
                            {
                                Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(corpse.transform.position);
                                if (GameUtils.IsScreenPointVisible(screenPosition))
                                {
                                    string txt = $"[Dead {(int)Math.Sqrt(num)}m]";
                                    GUI.Label(new Rect(screenPosition.x - 50f, screenPosition.y, 200, 60), txt);
                                }

                            }
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