using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace EFT.HideOut
{
    class GameScene
    {
        public static Scene CurrentGameScene;
        private static string GetSceneName()
        {
            return CurrentGameScene.name;
        }
        public static bool IsLoaded()
        {
            return CurrentGameScene.isLoaded;
        }
        public static bool InMatch()
        {
            return GetSceneName() != "EnvironmentUIScene" &&
                   GetSceneName() != "MenuUIScene" &&
                   GetSceneName() != "CommonUIScene" &&
                   GetSceneName() != "MainScene" &&
                   GetSceneName() != "";
        }
        public static void GetScene()
        {
            CurrentGameScene = SceneManager.GetActiveScene();
        }
    }
}
