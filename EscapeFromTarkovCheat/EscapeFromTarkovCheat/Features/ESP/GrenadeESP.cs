using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Interactive;
using EFT.UI;
using UnityEngine;

namespace EFT.Reborn
{
    class GrenadeESP : MonoBehaviour
    {
        public void OnGUI()
        {
            try
            {
                if (!Main.ShouldUpdate() || !Settings.GrenadeESP)
                    return;

                for (int i = 0; i < Main.GameWorld.LootItems.Count; i++)
                {
                    Throwable throwable = Main.GameWorld.Grenades.GetByIndex(i);
                    if (throwable == null)
                        continue;

                    int distance = (int)Vector3.Distance(Main.LocalPlayer.Transform.position, throwable.transform.position);

                    if (distance > 100f)
                        continue;

                    Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(throwable.transform.position);
                    var text = $"Grenade: {distance}m";

                    Render.DrawBox(screenPosition, new Vector2(10f, 10f), 1f, Settings.SpecialColor);
                    Render.DrawString(new Vector2(screenPosition.x - 50f, screenPosition.y), text, Settings.SpecialColor);

                    if (distance < 10f)
                    {
                        var textStyle = new GUIStyle(GUI.skin.label) { fontSize = 25 };
                        GUI.Label(new Rect(1024, Screen.height - 48, 512, 48), $"Grenade close: {distance}", textStyle);
                    }
                }
            }
            catch
            {
                //
            }
        }
    }
}