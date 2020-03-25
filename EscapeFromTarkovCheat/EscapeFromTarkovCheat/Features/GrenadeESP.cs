using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.UI;
using UnityEngine;

namespace EFT.Reborn
{
    class GrenadeESP : MonoBehaviour
    {
        private List<Throwable> _grenadeList;
        private float _nextCameraCacheTime;
        private static readonly float _cacheCameraInterval = 0.5f;


        public void FixedUpdate()
        {
            try
            {
                if (!Main.ShouldUpdate())
                    return;

                if (Time.time >= _nextCameraCacheTime && !MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                {
                    _nextCameraCacheTime = (Time.time + _cacheCameraInterval);
                    _grenadeList = FindObjectsOfType<Throwable>().ToList();
                }

            }
            catch
            {
                
            }

        }

        public void OnGUI()
        {
            try
            {
                if (!Main.ShouldUpdate())
                    return;

                string text;

                foreach (var throwable in _grenadeList)
                {
                    if (throwable == null)
                        continue;
                    int distance = (int)Vector3.Distance(Main.LocalPlayer.Transform.position, throwable.transform.position);

                    if (distance > 100f)
                     continue;   
                    
                    Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(throwable.transform.position);
                    text = $"Grenade: {distance}m";

                    Render.DrawBox(screenPosition, new Vector2(10f, 10f),1f, Color.red );
                    Render.DrawString(new Vector2(screenPosition.x - 50f, screenPosition.y), text, Color.red);
                }
            }
            catch
            {
                //
            }
        }
    }
}
