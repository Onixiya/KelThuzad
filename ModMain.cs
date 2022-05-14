#pragma warning disable
global using Assets.Scripts.Models;
global using Assets.Scripts.Models.Towers;
global using Assets.Scripts.Models.GenericBehaviors;
global using Assets.Scripts.Models.Map;
global using Assets.Scripts.Unity.Display;
global using Assets.Scripts.Utils;
global using Assets.Scripts.Models.Towers.Behaviors.Attack;
global using Assets.Scripts.Models.Towers.Behaviors;
global using Assets.Scripts.Models.Towers.Behaviors.Emissions;
global using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
global using Assets.Scripts.Unity;
global using Assets.Scripts.Models.Towers.Behaviors.Abilities;
global using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
global using Assets.Scripts.Simulation.Towers;
global using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
global using Assets.Scripts.Models.Towers.TowerFilters;
global using Assets.Scripts.Simulation.Towers.Weapons;
global using Assets.Scripts.Models.Towers.Filters;
global using Assets.Scripts.Simulation.Towers.Behaviors.Abilities;
global using Assets.Scripts.Unity.Audio;
global using Assets.Scripts.Models.Bloons.Behaviors;
global using Assets.Scripts.Models.Towers.Weapons.Behaviors;
global using BTD_Mod_Helper;
global using BTD_Mod_Helper.Extensions;
global using BTD_Mod_Helper.Api.Towers;
global using BTD_Mod_Helper.Api.ModOptions;
global using UnhollowerBaseLib;
global using static WarExpansion.WarExpansionMain;
global using System.Linq;
global using uObject=UnityEngine.Object;
global using HarmonyLib;
global using MelonLoader;
global using System.Collections.Generic;
global using UnityEngine;
global using UnityEngine.UI;
global using System;
global using Assets.Scripts.Data.MapSets;
global using Assets.Scripts.Models.Map.Gizmos;
global using Assets.Scripts.Models.Map.Triggers;
global using Assets.Scripts.Simulation.SMath;
global using Assets.Scripts.Unity.Map;
global using UnityEngine.SceneManagement;
global using Vector2=Assets.Scripts.Simulation.SMath.Vector2;
global using Assets.Scripts.Data;
global using Assets.Scripts.Unity.UI_New.InGame;
global using Assets.Scripts.Models.Effects;
global using Assets.Scripts.Models.Audio;
global using Assets.Scripts.Unity.UI_New.Main.HeroSelect;
global using System.IO;
global using Assets.Scripts.Unity.UI_New.InGame.StoreMenu;
global using Assets.Scripts.Models.Towers.Weapons;
global using Assets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
global using System.Reflection;
global using Assets.Scripts.Models.Towers.Projectiles;
global using Assets.Scripts.Simulation.Towers.Projectiles;
global using Assets.Scripts.Simulation.Objects;
global using UnhollowerRuntimeLib;
global using Assets.Scripts.Models.Profile;
global using Assets.Scripts.Unity.UI_New.TrophyStore;
global using Assets.Scripts.Unity.Player;
global using Assets.Scripts.Models.Towers.Pets;
global using Assets.Scripts.Simulation.Towers.Pets;
global using Assets.Scripts.Simulation.Towers;
[assembly:MelonGame("Ninja Kiwi","BloonsTD6")]
[assembly:MelonInfo(typeof(WarExpansion.WarExpansionMain),"WarExpansion","1.0","Silentstorm#5336")]
namespace WarExpansion{
    public class WarExpansionMain:BloonsTD6Mod{
        /*public override void OnApplicationQuit(){
            if(Maps.XelNagaTemple.InGameUpdate_Patch.areatowrite.Count!=0)Log("\n"+string.Join("\n",Maps.XelNagaTemple.InGameUpdate_Patch.areatowrite));
            if(Maps.XelNagaTemple.InGameUpdate_Patch.pathtowrite.Count!=0)Log("\n"+string.Join("\n",Maps.XelNagaTemple.InGameUpdate_Patch.pathtowrite));
        }*/
        public static void Log(object thingtolog,string type="msg"){
            switch(type){
                case"msg":
                    mllog.Msg(thingtolog);
                    break;
                case"warn":
                    mllog.Warning(thingtolog);
                    break;
                 case"error":
                    mllog.Error(thingtolog);
                    break;
            }
        }
        public override void OnApplicationStart(){
            List<string>BlacklistedModNames=new List<string>(){"monkeymoney","monkeyknowledge","trophy","unlock","monkey-knowledge","monkey-money","monkey money","monkey knowledge"};
            List<string>BlacklistedModAuthors=new List<string>(){"kenx00x","maliciousfiles","mr.nuke","turkey","true north coding / elite modding","elite modding","true north coding"};
            bool quit=false;
            foreach(MelonMod mod in MelonHandler.Mods){
                if(BlacklistedModNames.Contains(mod.Info.Name.ToLower())||BlacklistedModAuthors.Contains(mod.Info.Name.ToLower())){
                    File.Delete(mod.Location);
                    quit=true;
                }
            }
            if(quit){
                Application.Quit(0);
            }
        }
        public static void SetSounds(TowerModel towerModel,string asset){
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound.assetId=asset+"-upgrade";
            towerModel.RemoveBehavior<CreateSoundOnSelectedModel>();
            towerModel.AddBehavior(new CreateSoundOnSelectedModel("CreateSoundOnSelectedModel",new SoundModel("SoundModel1",asset+"-select"),new SoundModel("",""),new SoundModel("",""),
                new SoundModel("",""),new SoundModel("",""),new SoundModel("",""),new SoundModel("",""),new SoundModel("","")));
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId=asset+"-birth";
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound2.assetId=towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId;
        }
        public static void SetTowerAssets(){
            foreach(Type type in TowerTypes){
                TowerBundles.Add(type.Name,(AssetBundle)type.GetFields().First(a=>a.Name=="TowerAssets").GetValue(type.GetFields().First(a=>a.Name=="TowerAssets")));
            }
        }
        [HarmonyPatch(typeof(Factory),"FindAndSetupPrototypeAsync")]
        public class FactoryFindAndSetupPrototypeAsync_Patch{
            [HarmonyPrefix]
            public static bool Prefix(Factory __instance,string objectId,Il2CppSystem.Action<UnityDisplayNode>onComplete){
                string bundle=objectId.Split('-')[0];
                if(TowerBundles.ContainsKey(bundle)){
                    UnityDisplayNode udn=null;
                    try{
                        udn=uObject.Instantiate(TowerBundles[bundle].LoadAsset(objectId).Cast<GameObject>(),__instance.PrototypeRoot).AddComponent<UnityDisplayNode>();
                    }catch{
                       SetTowerAssets();
                    }
                    if(udn==null){
                        udn=uObject.Instantiate(TowerBundles[bundle].LoadAsset(objectId).Cast<GameObject>(),__instance.PrototypeRoot).AddComponent<UnityDisplayNode>();
                    }
                    udn.transform.position=new(-30000,-30000);
                    udn.name=objectId;
                    if(File.Exists(MelonHandler.ModsDirectory+"\\warhirestextures.bundle")){
                        if(HiResTextures==null||HiResTextures[0]==null){
                            HiResTextures.Clear();
                            AssetBundle HiResBundle=AssetBundle.LoadFromFile(MelonHandler.ModsDirectory+"\\warhirestextures.bundle");
                            HiResTextures=HiResBundle.LoadAllAssets();
                            HiResBundle.Unload(false);
                        }
                        foreach(Renderer renderer in udn.genericRenderers){
                            try{
                                renderer.material.SetTexture("_EmissionMap",HiResTextures.First(a=>a.name=="HiRes"+renderer.material.GetTexture("_EmissionMap").name).Cast<Texture2D>());
                            }catch{
                            }
                            try{
                                renderer.material.SetTexture("_MainTex",HiResTextures.First(a=>a.name=="HiRes"+renderer.material.GetTexture("_MainTex").name).Cast<Texture2D>());
                            }catch{
                            }
                        }
                    }
                    onComplete.Invoke(udn);
                    DisplayDict.Add(objectId,udn);
                    return false;
                }
                if(DisplayDict.ContainsKey(objectId)){
                    onComplete.Invoke(DisplayDict[objectId]);
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(ResourceLoader),"LoadSpriteFromSpriteReferenceAsync")]
        public class ResourceLoaderLoadSpriteFromSpriteReferenceAsync_Patch{
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference,ref Image image){
                if(reference!=null){
                    string asset=reference.guidRef.Split('-')[0];
                    if(TowerBundles.ContainsKey(asset)){
                        Texture2D texture=null;
                        try{
                            texture=TowerBundles[asset].LoadAsset(reference.guidRef).Cast<Texture2D>();
                        }catch{
                            SetTowerAssets();
                        }
                        if(texture==null){
                            texture=TowerBundles[asset].LoadAsset(reference.guidRef).Cast<Texture2D>();
                        }
                        image.canvasRenderer.SetTexture(texture);
                        image.sprite=Sprite.Create(texture,new(0,0,texture.width,texture.height),new());
                    }
                }
            }
        }
        public override void OnUpdate(){
            if(DisplayDict.Count!=0){
                foreach(var proto in DisplayDict.Values)uObject.Destroy(proto.gameObject);
                DisplayDict.Clear();
            }
        }
        public static Dictionary<string,AssetBundle>TowerBundles=new Dictionary<string,AssetBundle>();
        public static string CurrentHero="";
        public static byte[]HiResBundle=new byte[]{};
        public static Il2CppReferenceArray<uObject>HiResTextures=new Il2CppReferenceArray<uObject>(99999);
        private static MelonLogger.Instance mllog=new MelonLogger.Instance("WarExpansion");
        public static Dictionary<string,UnityDisplayNode>DisplayDict=new();
        public static AudioFactory AudioFactoryInstance;
        public static Il2CppStructArray<AreaType>FlyingAreaType=new(4);
        public static Dictionary<string,Il2CppSystem.Type>CustomComponents=new Dictionary<string,Il2CppSystem.Type>();
        public static List<Type>TowerTypes=new List<Type>();
        public static AbilityModel blankAbilityModel=null;
        public static string LastSoundPlayed;
        [HarmonyPatch(typeof(AudioFactory),"Start")]
        public class AudioFactoryStart_Patch{
            [HarmonyPostfix]
            public static void Postfix(ref AudioFactory __instance){
                AssetBundle bundle=AssetBundle.LoadFromMemory(Bundles.Bundles.audioclips);
                foreach(string asset in bundle.GetAllAssetNames()){
                    string audioClip=asset.Split('/').Last().Remove(asset.Split('/').Last().Length-4);
                    __instance.RegisterAudioClip(audioClip,bundle.LoadAsset(audioClip).Cast<AudioClip>());
                }
                AudioFactoryInstance=__instance;
                foreach(Type type in MelonHandler.Mods.First(a=>a.Info.Name=="WarExpansion").Assembly.GetTypes()){
                    if(type.BaseType==typeof(ModHero)||type.BaseType==typeof(ModTower)){
                        TowerTypes.Add(type);
                        TowerBundles.Add(type.Name,(AssetBundle)type.GetFields().First(a=>a.Name=="TowerAssets").GetValue(type.GetFields().First(a=>a.Name=="TowerAssets")));
                    }
                }
                FlyingAreaType[0]=AreaType.land;
                FlyingAreaType[1]=AreaType.water;
                FlyingAreaType[2]=AreaType.ice;
                FlyingAreaType[3]=AreaType.track;
            }
        }
        [HarmonyPatch(typeof(CommonForegroundScreenHeroButton),"LoadIcon")]
        public class CommonForegroundScreenHeroButtonLoadIcon_Patch{
            [HarmonyPostfix]
            public static void Postfix(string heroSkin){
                CurrentHero=heroSkin;
            }
        }
        [HarmonyPatch(typeof(TowerManager),"UpgradeTower")]
        public class TowerManagerUpgradeTower_Patch{
            [HarmonyPostfix]
            public static void Postfix(TowerModel def){
                if(TowerBundles.ContainsKey(def.name.Split(' ')[0].Split('-')[1])){
                    string sound=def.GetBehavior<CreateSoundOnUpgradeModel>().sound.assetId+new System.Random().Next(1,5);
                    while(sound==LastSoundPlayed){
                        sound=def.GetBehavior<CreateSoundOnUpgradeModel>().sound.assetId+new System.Random().Next(1,5);
                    }
                    LastSoundPlayed=sound;
                    AudioFactoryInstance.PlaySoundFromUnity(null,sound,"FX",1,1);
                }
            }
        }
        [HarmonyPatch(typeof(TowerSelectionMenu),"Show")]
        public class CreateSoundOnSelected_Patch{
            [HarmonyPostfix]
            public static void Postfix(TowerSelectionMenu __instance){
                TowerModel towerModel=__instance.selectedTower.tower.towerModel;
                if(TowerBundles.ContainsKey(towerModel.name.Split('-')[1])){
                    string sound=null;
                    if(towerModel.HasBehavior<HeroModel>()){
                        sound=towerModel.GetBehavior<CreateSoundOnSelectedModel>().sound1.assetId+new System.Random().Next(1,7);
                        while(sound==LastSoundPlayed){
                            sound=towerModel.GetBehavior<CreateSoundOnSelectedModel>().sound1.assetId+new System.Random().Next(1,7);
                        }
                        LastSoundPlayed=sound;
                        AudioFactoryInstance.PlaySoundFromUnity(null,sound,"FX",1,1);
                    }else{
                        sound=towerModel.GetBehavior<CreateSoundOnSelectedModel>().sound1.assetId+new System.Random().Next(1,5);
                        while(sound==LastSoundPlayed){
                            sound=towerModel.GetBehavior<CreateSoundOnSelectedModel>().sound1.assetId+new System.Random().Next(1,5);
                        }
                        LastSoundPlayed=sound;
                        AudioFactoryInstance.PlaySoundFromUnity(null,sound,"FX",1,1);
                    }
                }
            }
        }
    }
}