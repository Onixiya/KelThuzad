namespace WarExpansion.Towers{
    public class KelThuzad:ModHero{
        public override string BaseTower=>"GlueGunner";
        public override int Cost=>675;
        public override string DisplayName=>"Kel'Thuzad";
        public override string Title=>"Arch Lich of the Scourge";
        public override string Level1Description=>"Casts frost bolts at nearby bloons";
        public override string Description=>"Extremely experienced necromancer and founder of the Cult of the Damned. Reborn into a Lich with the destruction of the Sunwell";
        public override string NameStyle=>TowerType.Gwendolin;
        public override int MaxLevel=>20;
        public override float XpRatio=>1;
        public override int Abilities=>3;
        public override SpriteReference PortraitReference=>new("KelThuzadPortrait");
        public override SpriteReference ButtonReference=>new("KelThuzadButton");
        public override SpriteReference IconReference=>new("KelThuzadIcon");
        public override void ModifyBaseTowerModel(TowerModel KelThuzad){
            try{TowerBundles.Add("KelThuzad",AssetBundle.LoadFromMemory(Assets.Assets.kelthuzad));}catch{}
            var FrostBolt=KelThuzad.GetAttackModel();
            //FrostBolt.weapons[0].projectile.AddBehavior(new DamageModel("DamageModel",1,0,false,true,true,BloonProperties.));
            KelThuzad.display="KelThuzadPrefab";
            KelThuzad.GetBehavior<DisplayModel>().display=KelThuzad.display;
            SetSounds(KelThuzad,"kelthuzad");
            KelThuzad.RemoveBehavior<CreateSoundOnBloonEnterTrackModel>();
            KelThuzad.RemoveBehavior<CreateSoundOnBloonLeakModel>();
        }
        public class L2:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack speed";
            public override int Level=>2;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L3:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases damage";
            public override int Level=>3;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L4:ModHeroLevel<KelThuzad>{
            public override string AbilityName=>"Frost Nova";
            public override string AbilityDescription=>"Releases a radial blast of ice, dealing damage and slowing down bloons";
            public override string Description=>AbilityName+": "+AbilityDescription;
            public override int Level=>4;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                var FrostNova=Game.instance.model.GetTowerFromId("SuperMonkey-040").GetBehavior<AbilityModel>().Duplicate();
                FrostNova.name="FrostNova";
                FrostNova.icon=new SpriteReference("KelThuzadFrostNovaIcon");
                FrostNova.displayName=AbilityName;
                FrostNova.description=AbilityDescription;
                FrostNova.RemoveBehavior<CreateSoundOnAbilityModel>();
                SoundModel sound=new SoundModel("SoundModel","KelThuzadFrostNova");
                FrostNova.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel",sound,sound,sound));
                KelThuzad.AddBehavior(FrostNova);
            }
        }
        public class L5:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases range";
            public override int Level=>5;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L6:ModHeroLevel<KelThuzad>{
            public override string Description=>"Slows down bloons more";
            public override int Level=>6;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L7:ModHeroLevel<KelThuzad>{
            public override string AbilityName=>"Dark Ritual";
            public override string AbilityDescription=>"Steals the soul of another tower, granting XP and attack speed bonuses for a short while";
            public override string Description=>AbilityName+": "+AbilityDescription;
            public override int Level=>7;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                var DarkRitual=Game.instance.model.GetTowerFromId("SuperMonkey-040").GetBehavior<AbilityModel>().Duplicate();
                DarkRitual.name="DarkRitual";
                DarkRitual.icon=new SpriteReference("KelThuzadDarkRitualIcon");
                DarkRitual.displayName=AbilityName;
                DarkRitual.description=AbilityDescription;
                DarkRitual.RemoveBehavior<CreateSoundOnAbilityModel>();
                SoundModel sound=new SoundModel("SoundModel","KelThuzadDarkRitual");
                DarkRitual.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel",sound,sound,sound));
                KelThuzad.AddBehavior(DarkRitual);
            }
        }
        public class L8:ModHeroLevel<KelThuzad>{
            public override string Description=>"Lightning Dash cooldown decreased";
            public override int Level=>8;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L9:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack speed";
            public override int Level=>9;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L10:ModHeroLevel<KelThuzad>{
            public override string Description=>"Lightning Dash damage increased";
            public override int Level=>10;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L11:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases damage";
            public override int Level=>11;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L12:ModHeroLevel<KelThuzad>{
            public override string Description=>"Astral Wind and Lightning Dash cooldown decreased";
            public override int Level=>12;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L13:ModHeroLevel<KelThuzad>{
            public override string Description=>"Astral Wind duration increased";
            public override int Level=>13;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L14:ModHeroLevel<KelThuzad>{
            public override string Description=>"Psi Blades now damage all bloons in front";
            public override int Level=>14;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L15:ModHeroLevel<KelThuzad>{
            public override string Description=>"Astral Wind range increased";
            public override int Level=>15;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L16:ModHeroLevel<KelThuzad>{
            public override string AbilityName=>"Death and Decay";
            public override string AbilityDescription=>"Deals high damage in a area to all bloons";
            public override string Description=>AbilityName+": "+AbilityDescription;
            public override int Level=>16;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                var DeathAndDecay=Game.instance.model.GetTowerFromId("SuperMonkey-040").GetBehavior<AbilityModel>().Duplicate();
                DeathAndDecay.name="DeathAndDecay";
                DeathAndDecay.icon=new SpriteReference("KelThuzadDeathAndDecayIcon");
                DeathAndDecay.displayName=AbilityName;
                DeathAndDecay.description=AbilityDescription;
                DeathAndDecay.RemoveBehavior<CreateSoundOnAbilityModel>();
                SoundModel sound=new SoundModel("SoundModel","KelThuzadDeathAndDecay");
                DeathAndDecay.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel",sound,sound,sound));
                KelThuzad.AddBehavior(DeathAndDecay);
            }
        }
        public class L17:ModHeroLevel<KelThuzad>{
            public override string Description=>"Lightning Dash damage increased";
            public override int Level=>17;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L18:ModHeroLevel<KelThuzad>{
            public override string Description=>"Astral Wind and Lightning Dash cooldown decreased";
            public override int Level=>18;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L19:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases damage";
            public override int Level=>19;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L20:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack speed";
            public override int Level=>20;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        [HarmonyPatch(typeof(Weapon),"SpawnDart")]
        public class WeaponSpawnDart_Patch{
            [HarmonyPostfix]
            public static void Postfix(ref Weapon __instance){
                if(__instance.attack.tower.towerModel.name.Contains("KelThuzad")){
                    __instance.attack.tower.Node.graphic.GetComponent<Animator>().Play("KelThuzadAttack");
                }
            }
        }
        [HarmonyPatch(typeof(Ability),"Activate")]
        public class Activate_Patch{
            [HarmonyPostfix]
            public static void Postfix(ref Ability __instance){
                if(__instance.tower.towerModel.name.Contains("KelThuzad")){
                    switch(__instance.abilityModel.name){
                        case"AbilityModel_FrostNova":
                            __instance.tower.Node.graphic.GetComponent<Animator>().Play("KelThuzadFrostNova");
                            break;
                        case"AbilityModel_DarkRitual":
                            __instance.tower.Node.graphic.GetComponent<Animator>().Play("KelThuzadAttack");
                            break;
                        case"AbilityModel_DeathAndDecay":
                            __instance.tower.Node.graphic.GetComponent<Animator>().Play("KelThuzadDeathAndDecay");
                            break;
                    }
                }
            }
        }
    }
}
