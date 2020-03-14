﻿using BehaviourMachine;
using EFT.InventoryLogic;
using EscapeFromTarkovCheat.Feauters.ESP;
using UnityEngine;

namespace EscapeFromTarkovCheat
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
            Object.Destroy(Main.hookObject);
            Object.Destroy(HookObject);
        }
    }
#else

    public class Loader : MonoBehaviour
    {
        public GameObject HookObject;

        public void Start()
        {
            Load();
        }

        public void Load()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Main>();
            Object.DontDestroyOnLoad(HookObject);
        }

        public void Unload()
        {
            Object.Destroy(Main.hookObject);
            Object.Destroy(HookObject);
        }
    }
#endif
}
