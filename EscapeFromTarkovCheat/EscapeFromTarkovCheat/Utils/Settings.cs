using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Visual;
using UnityEngine;

namespace EFT.Reborn
{
    class Settings
    {
        //Colours
        internal static Color AimbotLockedColor = Color.white;
		internal static Color SpecialColor = new Color(1f, 0.2f, 0.09f);
        internal static Color QuestColor = Color.yellow;
        internal static Color CommonColor = Color.white;
        internal static Color RareColor = new Color(0.38f, 0.43f, 1f);
        internal static Color SuperRareColor = new Color(1f, 0.29f, 0.36f);
        internal static Color LootableContainerColor = new Color(1f, 0.2f, 0.09f);
        internal static Color ExfiltrationPointColour = Color.green;

        internal static Color PlayerColor = new Color(0f, 0.7f, 0f, 1f);
        internal static Color PlayerScavColor = new Color(1, 0.5f, 0, 1);
        //internal static Color DeadPlayerColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        internal static Color BotColor = new Color(1, 1, 0, 1f);
        internal static Color HealthColor = new Color(0f, 0.7f,0f, 1f);
        internal static Color BossColor = new Color(0.7f, 0f,0f,1f);
        internal static Color FriendColor = Color.magenta;

        //Keybinds
        internal static KeyCode ToggleMenu = KeyCode.Insert;
        internal static KeyCode TogglePlayerESP = KeyCode.F1;
        internal static KeyCode ToggleItemESP = KeyCode.F2;
        internal static KeyCode ItemCategory = KeyCode.F3;
        internal static KeyCode ToggleLootableContainerESP = KeyCode.F4;
        internal static KeyCode ToggleExitPoints = KeyCode.F6;
        internal static KeyCode ToggleSpeedHack = KeyCode.F7;
        internal static KeyCode UnlockDoors = KeyCode.F8;
        internal static KeyCode AimbotKey = KeyCode.LeftControl;
        internal static KeyCode ToggleThermalVison = KeyCode.F9;


        //players
        internal static bool DrawPlayers = true;
        internal static bool DrawPlayerName = true;
        internal static bool DrawPlayerHealthBar = false;
        internal static bool DrawPlayerHealth = false;
        internal static bool DrawPlayerBox = true;
        internal static bool DrawPlayerLine = true;
        internal static bool DrawPlayerSkeleton = true;
        internal static bool DrawPlayerWeapon = true;
        internal static bool DrawPlayerLevel = true;
        internal static bool DrawPlayerDistance = true;
        internal static float DrawPlayersRange = 200f;
        internal static float DrawPlayerSkeletonDistance = 100f;
        internal static bool DrawPlayerCorpses = true;
        internal static bool DrawScavs = true;
        internal static bool DrawScavName = true;
        internal static bool DrawScavWeapon = true;
        internal static bool DrawScavDistance = true;
        internal static bool DrawScavHealth = false;
        internal static bool DrawScavBox = true;
        internal static bool DrawScavHealthBar = false;
        internal static bool DrawScavLine = false;
        internal static bool DrawScavSkeleton = false;
        internal static float DrawScavSkeletonDistance = 100f;
        internal static float DrawScavRange = 200f;


        //misc visuals
        internal static bool DrawLootItems = false;
        internal static float DrawLootItemsDistance = 300f;
        internal static float DrawLootableContainersDistance = 10f;
        internal static bool DrawLootableContainers = true;
        internal static bool DrawExfiltrationPoints = true;
        internal static bool DrawEmptyContainers = false;
        internal static bool DrawContainersContent = true;
        internal static bool GrenadeESP = true;

        //aimboot
        internal static bool Aimbot = true;
        internal static int AimBotDistance = 300;
        internal static int AimBotBone = 66;
        internal static float AimbotFOV = 10f;

        //misc
        internal static bool InfiniteStamina = true;
        internal static bool DoorUnlocker = true;
        internal static bool MaxSkills = false;
        internal static bool NoVisor = true;
        internal static bool ThermalVison = false;
        internal static bool SpeedHack = true;
        internal static float SpeedValue = 1.3f;

        //Weapon
        internal static bool NoRecoil = true;
        internal static bool DrawWeaponInfo = true;
        internal static bool NoSway = true;
        internal static bool SuperBullet = false;
        internal static bool FastReload = false;
        internal static bool AlwaysAutomatic = false;
        internal static bool FireRate = false;
        internal static int FireRateValue = 1000;

        internal static readonly List<string> _rareItems = new List<string>(new string[]
{
			"5448ba0b4bdc2d02308b456c",
			"544a5caa4bdc2d1a388b4568",
			"545cdb794bdc2d3a198b456a",
			"56e294cdd2720b603a8b4575",
			"57235b6f24597759bf5a30f1",
			"5733279d245977289b77ec24",
			"57347ca924597744596b4e71",
			"5780cf942459777df90dcb72",
			"590c392f86f77444754deb29",
			"590c60fc86f77412b13fddcf",
			"590c621186f774138d11ea29",
			"590c645c86f77412b01304d9",
			"5bc9b720d4351e450201234b",
			"590c651286f7741e566b6461",
			"590de7e986f7741b096e5f32",
			"591094e086f7747caa7bb2ef",
			"5913915886f774123603c392",
			"59bffbb386f77435b379b9c2",
			"59e3639286f7741777737013",
			"59e3647686f774176a362507",
			"59faf7ca86f7740dbe19f6c2",
			"59fafd4b86f7745ca07e1232",
			"59faff1d86f7746c51718c9c",
			"59fb016586f7746d0d4b423a",
			"59fb023c86f7746d0d4b423c",
			"59fb042886f7746c5005a7b2",
			"5a0dc45586f7742f6b0b73e3",
			"5a0dc95c86f77452440fc675",
			"5a0ea69f86f7741cd5406619",
			"5a0ec70e86f7742c0b518fba",
			"59ef13ca86f77445fd0e2483",
			"5a0ee30786f774023b6ee08f",
			"5a0ee34586f774023b6ee092",
			"5a0ee37f86f774023657a86f",
			"5a0ee4b586f7743698200d22",
			"5a0ee62286f774369454a7ac",
			"5a0ee72c86f77436955d3435",
			"5a0ee76686f7743698200d5c",
			"5a0eeb1a86f774688b70aa5c",
			"5a0eeb8e86f77461257ed71a",
			"5a0eebed86f77461230ddb3d",
			"5a0eec9686f77402ac5c39f2",
			"5a0eecf686f7740350630097",
			"5a0eedb386f77403506300be",
			"5a0eee1486f77402aa773226",
			"5a0f006986f7741ffd2fe484",
			"5a0f08bc86f77478f33b84c2",
			"5a0f0f5886f7741c4e32a472",
			"5a13ee1986f774794d4c14cd",
			"5a13eebd86f7746fd639aa93",
			"5a13ef0686f7746e5a411744",
			"5a13ef7e86f7741290491063",
			"5a13f24186f77410e57c5626",
			"5a13f35286f77413ef1436b0",
			"5a144bdb86f7741d374bbde0",
			"5a1452ee86f7746f33111763",
			"5a154d5cfcdbcb001a3b00da",
			"5a1eaa87fcdbcb001865f75e",
			"5a34fe59c4a282000b1521a2",
			"5aa66be6e5b5b0214e506e97",
			"5aa7d193e5b5b000171d063f",
			"5aa7e276e5b5b000171d0647",
			"5aafbcd986f7745e590fff23",
			"5aafbde786f774389d0cbc0f",
			"5ab8dced86f774646209ec87",
			"5ab8e79e86f7742d8b372e78",
			"5ac8d6885acfc400180ae7b0",
			"5ad5cfbd86f7742c825d6104",
			"5ad5d64486f774079b080af8",
			"5ad5d7d286f77450166e0a89",
			"5ad5db3786f7743568421cce",
			"5ad7217186f7746744498875",
			"5ad7242b86f7740a6a3abd43",
			"5ad7247386f7747487619dc3",
			"5addaffe86f77470b455f900",
			"5af0534a86f7743b6f354284",
			"5b40e1525acfc4771e1c6611",
			"5b40e2bc5acfc40016388216",
			"5b4329f05acfc47a86086aa1",
			"5b43575a86f77424f443fe62",
			"5b44cad286f77402a54ae7e5",
			"5b44cd8b86f774503d30cba2",
			"5b44cf1486f77431723e3d05",
			"5b44d0de86f774503d30cba8",
			"5b6d9ce188a4501afc1b2b25",
			"5b7c710788a4506dec015957",
			"5bc9b9ecd4351e3bac122519",
			"5bc9bc53d4351e00367fbcee",
			"5bc9bdb8d4351e003562b8a1",
			"5bc9c049d4351e44f824d360",
			"5bc9c1e2d4351e00367fbcf0",
			"5bffdd7e0db834001b734a1a",
			"5bffe7930db834001b734a39",
			"5c010e350db83400232feec7",
			"5c052e6986f7746b207bc3c9",
			"5c052f6886f7746b1e3db148",
			"5c05308086f7746b2101e90b",
			"5c0530ee86f774697952d952",
			"5c0558060db834001b735271",
			"5c066e3a0db834001b7353f0",
			"5c091a4e0db834001d5addc8",
			"5c093db286f7740a1b2617e3",
			"5c093e3486f77430cb02e593",
			"5c0a840b86f7742ffa4f2482",
			"5c0e51be86f774598e797894",
			"5c0e53c886f7747fa54205c7",
			"5c0e541586f7747fa54205c9",
			"5c0e57ba86f7747fa141986d",
			"5c0e625a86f7742d77340f62",
			"5c0e655586f774045612eeb2",
			"5c0e66e2d174af02a96252f4",
			"5c0e722886f7740458316a57",
			"5c0e746986f7741453628fe5",
			"5c0e774286f77468413cc5b2",
			"5d8e3ecc86f774414c78d05e",
			"5c0e842486f77443a74d2976",
			"5c0e874186f7745dc7616606",
			"5c110624d174af029e69734c",
			"5c12620d86f7743f8b198b72",
			"5c1267ee86f77416ec610f72",
			"5c12688486f77426843c7d32",
			"5c127c4486f7745625356c13",
			"5c0126f40db834002a125382",
			"5c17a7ed2e2216152142459c",
			"5c1d0c5f86f7744bb2683cf0",
			"5c1d0dc586f7744baf2e7b79",
			"5c1d0efb86f7744baf2e7b7b",
			"5c1d0f4986f7744bb01837fa",
			"5c1e2a1e86f77431ea0ea84c",
			"5c1e2d1f86f77431e9280bee",
			"5d95d6fa86f77424484aa5e9",
			"5c1e495a86f7743109743dfb",
			"5c1f79a086f7746ed066fb8f",
			"5c94bbff86f7747ee735c08f",
			"5d80cb3886f77440556dbf09",
			"5ca20ee186f774799474abc2",
			"5ca2151486f774244a3b8d30",
			"5ca21c6986f77479963115a7",
			"5cadfbf7ae92152ac412eeef",
			"5cebec00d7f00c065c53522a",
			"5d03775b86f774203e7e0c4b",
			"5d0377ce86f774186372f689",
			"5d03784a86f774203e7e0c4d",
			"5d03794386f77420415576f5",
			"5d08d21286f774736e7c94c3",
			"5d1b2f3f86f774252167a52c",
			"5d1b327086f7742525194449",
			"5d1b32c186f774252167a530",
			"5d1b376e86f774252519444e",
			"5d1b5e94d7ad1a2b865a96b0",
			"5d235bb686f77443f4331278",
			"5d5d87f786f77427997cfaef",
			"5d6d3716a4b9361bc8618872",
			"5d80c60f86f77440373c4ece",
			"5d80c62a86f7744036212b3f",
			"5d947d4e86f774447b415895",
			"5d80c78786f774403a401e3e",
			"5d80c88d86f77440556dbf07",
			"5d80c8f586f77440373c4ed0",
			"5d80c93086f7744036212b41",
			"5d80c95986f77440351beef3",
			"5d80ca9086f774403a401e40",
			"5d80c6fc86f774403a401e3c",
			"5d80cab086f77440535be201",
			"5d95d6be86f77424444eb3a7",
			"5d80cbd886f77470855c26c2",
			"5d80ccdd86f77474f7575e02",
			"5d80cb5686f77440545d1286",
			"5d80cd1a86f77402aa362f42",
			"5d8e0db586f7744450412a42",
			"5d8e15b686f774445103b190",
			"590a373286f774287540368b",
			"59e3577886f774176a362503",
			"5d8e0e0e86f774321140eb56",
			"5d9f1fa686f774726974a992",
			"5bc9c377d4351e3bac12251b",
			"5da46e3886f774653b7a83fe",
			"5da5cdcd86f774529238fb9b",
			"5d1b33a686f7742523398398",
			"5c12613b86f7743bbe2c3f76",
			"5d80c66d86f774405611c7d6",
			"5da743f586f7744014504f72",
			"5df8a4d786f77412672a1e3b",
			"5e00c1ad86f774747333222c",
			"5e01ef6886f77445f643baa4"
});

    }
}