using EFT.HideOut;
using UnityEngine;

namespace EFT.Reborn
{
#if DEBUG
        public class Loader
    {
        public static GameObject HookObject;

        public static void Load()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Main>();
            Object.DontDestroyOnLoad(HookObject);
        }

        public static void Unload()
        {
            Object.Destroy(Main.HookObject);
            Object.Destroy(HookObject);
        }
    }
#else

    public class Loader : MonoBehaviour
    {
        public GameObject HookObject;
        public void Load()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Main>();
            Object.DontDestroyOnLoad(HookObject);
        }

        public void Unload()
        {
            Object.Destroy(Main.HookObject);
            Object.Destroy(HookObject);
        }
    }
#endif
}
