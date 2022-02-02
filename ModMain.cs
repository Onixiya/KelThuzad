//saves so much space with global using, no need to put separate usings in each file, just put them all in the main one
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
[assembly:MelonGame("Ninja Kiwi","BloonsTD6")]
[assembly:MelonInfo(typeof(WarExpansion.WarExpansionMain),"WarExpansion","1.0","Silentstorm#5336")]
namespace WarExpansion{
    public class WarExpansionMain:BloonsTD6Mod{
        /*public override void OnApplicationQuit(){
            if(Maps.XelNagaTemple.InGameUpdate_Patch.areatowrite.Count!=0)Log("\n"+string.Join("\n",Maps.XelNagaTemple.InGameUpdate_Patch.areatowrite));
            if(Maps.XelNagaTemple.InGameUpdate_Patch.pathtowrite.Count!=0)Log("\n"+string.Join("\n",Maps.XelNagaTemple.InGameUpdate_Patch.pathtowrite));
        }*/
        public static void Log(object obj){
            mllog.Msg(obj);
        }
        public static void SetSounds(TowerModel towerModel,string asset){
            towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound.assetId=asset+"upgrade";
            towerModel.RemoveBehavior<CreateSoundOnSelectedModel>();
            towerModel.AddBehavior(new CreateSoundOnSelectedModel("CreateSoundOnSelectedModel",new SoundModel("SoundModel1",asset+"select"),new SoundModel("SoundModel2",asset+"select1"),
                new SoundModel("SoundModel3",asset+"select2"),new SoundModel("SoundModel4",asset+"select3"),new SoundModel("SoundModel5",asset+"select"),
                new SoundModel("SoundModel6",asset+"select1"),new SoundModel("AltSoundModel1",asset+"select2"),new SoundModel("AltSoundModel2",asset+"select3")));
            if(towerModel.GetBehavior<HeroModel>()!=null){
                towerModel.GetBehavior<CreateSoundOnSelectedModel>().sound5.assetId=asset+"select4";
                towerModel.GetBehavior<CreateSoundOnSelectedModel>().sound6.assetId=asset+"select5";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound1.assetId=asset+"upgrade1";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound2.assetId=asset+"upgrade2";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound3.assetId=asset+"upgrade3";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound4.assetId=asset+"upgrade";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound5.assetId=asset+"upgrade1";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound6.assetId=asset+"upgrade2";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound7.assetId=asset+"upgrade3";
                towerModel.GetBehavior<CreateSoundOnUpgradeModel>().sound8.assetId=asset+"upgrade";
            }
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId=asset+"birth";
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound2.assetId=towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId;
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().heroSound1.assetId=towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId;
            towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().heroSound2.assetId=towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId;
        }
        [HarmonyPatch(typeof(Factory),"FindAndSetupPrototypeAsync")]
        public class FactoryFindAndSetupPrototypeAsync_Patch{
            [HarmonyPrefix]
            public static bool Prefix(Factory __instance,string objectId,Il2CppSystem.Action<UnityDisplayNode>onComplete){
                foreach(string key in TowerBundles.Keys){
                    if(objectId.StartsWith(key)){
                        if(!DisplayDict.ContainsKey(objectId)){
                            Log(objectId);
                            var udn=uObject.Instantiate(TowerBundles[key].LoadAsset(objectId).Cast<GameObject>(),__instance.PrototypeRoot).AddComponent<UnityDisplayNode>();
                            udn.transform.position=new(-30000,-30000);
                            udn.name=objectId;
                            udn.isSprite=false;
                            if(File.Exists(MelonHandler.ModsDirectory+"\\warhirestextures.bundle")){
                                if(HiResTextures==null){
                                    HiResTextures=AssetBundle.LoadFromFile(MelonHandler.ModsDirectory+"\\warhirestextures.bundle").LoadAllAssets();
                                }
                                foreach(var renderer in udn.genericRenderers){
                                    if(!BlacklistedTextures.Contains(renderer.material.mainTexture.name)&&!renderer.material.mainTexture.name.Contains("HiRes")&&!renderer.material.shader.name.Contains("Moving")){
                                        if(renderer.material.GetTexture("_EmissionMap")!=null){
                                            renderer.material.SetTexture("_EmissionMap",HiResTextures.First(a=>a.name=="HiRes"+renderer.material.GetTexture("_EmissionMap").name).Cast<Texture2D>());
                                        }
                                        renderer.material.mainTexture=HiResTextures.First(a=>a.name=="HiRes"+renderer.material.mainTexture.name).Cast<Texture2D>();
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
                    }
                }
            return true;
            }
        }
        [HarmonyPatch(typeof(ResourceLoader),"LoadSpriteFromSpriteReferenceAsync")]
        public class ResourceLoaderLoadSpriteFromSpriteReferenceAsync_Patch{
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference,ref Image image){
                foreach(string key in TowerBundles.Keys){
                    if(reference!=null&&reference.guidRef.StartsWith(key)){
                        var text=TowerBundles[key].LoadAsset(reference.guidRef).Cast<Texture2D>();
                        image.canvasRenderer.SetTexture(text);
                        image.sprite=Sprite.Create(text,new(0,0,text.width,text.height),new());

                    }
                }
            }
        }
        public override void OnUpdate(){
            if(DisplayDict.Count!=0){
                foreach(var proto in DisplayDict.Values)uObject.Destroy(proto.gameObject);
                DisplayDict.Clear();
            }
            if(InGame.Bridge!=null){
                if((bool)RemoveBaseTowers.GetValue()){
                    switch(CurrentHero){
                        case"WarExpansion-KelThuzad":
                            foreach(var temp in uObject.FindObjectsOfType<TowerPurchaseButton>()){
                                var towermodel=temp.baseTowerModel;
                                if(!towermodel.towerSet.Contains("Undead")&&towermodel.GetBehavior<HeroModel>()==null){
                                    temp.transform.parent.gameObject.Destroy();
                                }
                            }
                            break;
                    }
                }
            }
        }
        public static string[]BlacklistedTextures=new string[]{"KelThuzadColourTexture"};
        public static Dictionary<string,AssetBundle>TowerBundles=new Dictionary<string,AssetBundle>();
        public static string CurrentHero;
        public static byte[] HiResBundle;
        public static Il2CppReferenceArray<uObject>HiResTextures;
        private static MelonLogger.Instance mllog=new MelonLogger.Instance("WarExpansion");
        public static readonly ModSettingBool RemoveBaseTowers=false;
        public static Dictionary<string,UnityDisplayNode>DisplayDict=new();
        public static AudioFactory AudioFactoryInstance;
        /*public class HumanSet:ModTowerSet{
            public override string DisplayName=>"Protoss";
            public override string Container=>"ProtossContainer";
            public override string Button=>"ProtossButton";
            public override string ContainerLarge=>Container;
            public override string Portrait=>"ProtossHex";
        }
        public class NightElfSet:ModTowerSet{
            public override string DisplayName=>"Terran";
            public override string Container=>"TerranContainer";
            public override string Button=>"TerranButton";
            public override string ContainerLarge=>Container;
            public override string Portrait=>"TerranPortrait";
        }
        public class OrcSet:ModTowerSet{
            public override string DisplayName=>"Zerg";
            public override string Container=>"ZergContainer";
            public override string Button=>"ZergButton";
            public override string ContainerLarge=>Container;
            public override string Portrait=>"ZergCreep";
        }
        public class UndeadSet:ModTowerSet{
            public override string DisplayName=>"Zerg";
            public override string Container=>"ZergContainer";
            public override string Button=>"ZergButton";
            public override string ContainerLarge=>Container;
            public override string Portrait=>"ZergCreep";
        }*/
        [HarmonyPatch(typeof(AudioFactory),"Start")]
        public class AudioFactoryStart_Patch{
            [HarmonyPostfix]
            public static void Postfix(ref AudioFactory __instance){
                AssetBundle bundle=AssetBundle.LoadFromMemory(Assets.Assets.audioclips);
                foreach(string asset in bundle.GetAllAssetNames()){
                    string audioClip=asset.Split('/').Last().Remove(asset.Split('/').Last().Length-4);
                    __instance.RegisterAudioClip(audioClip,bundle.LoadAsset(audioClip).Cast<AudioClip>());
                }
            }
        }
        [HarmonyPatch(typeof(CommonForegroundScreenHeroButton),"LoadIcon")]
        public class CommonForegroundScreenHeroButtonLoadIcon_Patch{
            [HarmonyPostfix]
            public static void Postfix(string heroSkin){
                CurrentHero=heroSkin;
            }
        }
    }
}