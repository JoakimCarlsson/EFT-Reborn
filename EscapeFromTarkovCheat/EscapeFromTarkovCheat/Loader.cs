using BehaviourMachine;
using EFT.InventoryLogic;
using EscapeFromTarkovCheat.Feauters.ESP;
using UnityEngine;

namespace EscapeFromTarkovCheat
{
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

}
