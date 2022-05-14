#pragma warning disable
namespace KelThuzad.Towers{
    public class KelThuzad:ModHero{
        public override string BaseTower=>"GlueGunner";
        public override int Cost=>675;
        public override string DisplayName=>"Kel'Thuzad";
        public override string Title=>"Arch Lich of the Scourge";
        public override string Level1Description=>"Casts slowing frost bolts at nearby bloons";
        public override string Description=>"Extremely experienced necromancer and founder of the Cult of the Damned. Reborn into a Lich with the destruction of the Sunwell";
        public override string NameStyle=>TowerType.Gwendolin;
        public override int MaxLevel=>15;
        public override float XpRatio=>1;
        public override int Abilities=>2;
        public override SpriteReference PortraitReference=>new("KelThuzad-Portrait");
        public override SpriteReference ButtonReference=>new("KelThuzad-Button");
        public override SpriteReference SquareReference=>new("KelThuzad-HeroIcon");
        public override SpriteReference IconReference=>new("KelThuzad-Icon");
        public static AssetBundle TowerAssets=AssetBundle.LoadFromMemory(Bundles.Bundles.kelthuzad);
        public override void ModifyBaseTowerModel(TowerModel KelThuzad){
            blankAbilityModel=Game.instance.model.GetTowerFromId("BoomerangMonkey-040").GetAbility().Duplicate();
            blankAbilityModel.RemoveBehavior<TurboModel>();
            blankAbilityModel.RemoveBehavior<CreateSoundOnAbilityModel>();
            blankAbilityModel.RemoveBehavior<CreateEffectOnAbilityModel>();
            AttackModel FrostBolt=KelThuzad.GetAttackModel();
            FrostBolt.GetBehavior<AttackFilterModel>().filters=FrostBolt.GetBehavior<AttackFilterModel>().filters.RemoveItem(FrostBolt.GetBehavior<AttackFilterModel>().filters.
                First(a=>a.name.Contains("Tag")));
            FrostBolt.GetBehavior<AttackFilterModel>().filters=FrostBolt.GetBehavior<AttackFilterModel>().filters.RemoveItem(FrostBolt.GetBehavior<AttackFilterModel>().filters.
                First(a=>a.name.Contains("Glue")));
            FrostBolt.weapons[0].rate=1.1f;
            FrostBolt.weapons[0].projectile.AddBehavior(new DamageModel("DamageModel",2,-1,true,true,true,(BloonProperties)5,(BloonProperties)5));
            FrostBolt.weapons[0].projectile.filters=FrostBolt.weapons[0].projectile.filters.RemoveItem(FrostBolt.weapons[0].projectile.filters.First(a=>a.name.Contains("Glue")));
            FrostBolt.weapons[0].projectile.filters=FrostBolt.weapons[0].projectile.filters.RemoveItem(FrostBolt.weapons[0].projectile.filters.First(a=>a.name.Contains("Tag")));
            FrostBolt.weapons[0].projectile.display=Game.instance.model.GetTowerFromId("IceMonkey-005").GetAttackModel().weapons[0].projectile.display;
            KelThuzad.display="KelThuzad-Prefab";
            KelThuzad.GetBehavior<DisplayModel>().display=KelThuzad.display;
            SetSounds(KelThuzad,"kelthuzad");
            KelThuzad.RemoveBehavior<CreateSoundOnBloonEnterTrackModel>();
            KelThuzad.RemoveBehavior<CreateSoundOnBloonLeakModel>();
        }
        public class L2:ModHeroLevel<KelThuzad>{
            public override string Description=>"Faster attack speed";
            public override int Level=>2;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetAttackModel().weapons[0].rate-=0.1f;
            }
        }
        public class L3:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack damage";
            public override int Level=>3;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetAttackModel().weapons[0].projectile.GetDamageModel().damage+=2;
            }
        }
        public class L4:ModHeroLevel<KelThuzad>{
            public override string AbilityName=>"Frost Nova";
            public override string AbilityDescription=>"Releases a blast of ice towards a bloon, dealing damage to nearby bloons to it and slowing them down";
            public override string Description=>AbilityName+": "+AbilityDescription;
            public override int Level=>4;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                AbilityModel FrostNova=Game.instance.model.GetTowerFromId("SuperMonkey-050").GetBehavior<AbilityModel>().Duplicate();
                AttackModel FrostNovaAttack=KelThuzad.GetAttackModel().Duplicate();
                FrostNova.name="FrostNova";
                FrostNova.icon=new SpriteReference("KelThuzad-FrostNovaIcon");
                FrostNova.displayName=AbilityName;
                FrostNova.description=AbilityDescription;
                FrostNova.cooldown=55;
                FrostNova.RemoveBehavior<CreateEffectOnAbilityModel>();
                FrostNovaAttack.weapons[0].projectile.AddBehavior(new CreateProjectileOnContactModel("CreateProjectileOnContactModel",KelThuzad.GetAttackModel().weapons[0].projectile.Duplicate(),
                    new ArcEmissionModel("ArcEmissionModel",8,0,45,null,false),false,false,false));
                FrostNovaAttack.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage/=2;
                FrostNova.GetBehavior<ActivateAttackModel>().attacks[0]=FrostNovaAttack;
                FrostNova.GetBehavior<ActivateAttackModel>().isOneShot=true;
                FrostNova.GetBehavior<ActivateAttackModel>().cancelIfNoTargets=true;
                FrostNova.RemoveBehavior<CreateSoundOnAbilityModel>();
                SoundModel sound=new SoundModel("SoundModel","kelthuzad-frostnova");
                FrostNova.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel",sound,sound,sound));
                KelThuzad.AddBehavior(FrostNova);
            }
        }
        public class L5:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack range";
            public override int Level=>5;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=5;
                KelThuzad.GetAttackModel().range+=15;
            }
        }
        public class L6:ModHeroLevel<KelThuzad>{
            public override string Description=>"Frost bolt slows down bloons more";
            public override int Level=>6;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetAttackModel().weapons[0].projectile.GetBehavior<SlowModel>().multiplier=0.35f;
            }
        }
        public class L7:ModHeroLevel<KelThuzad>{
            public override string AbilityName=>"Dark Ritual";
            public override string AbilityDescription=>"Steals the soul of another tower, granting XP and attack speed bonuses for a short while";
            public override string Description=>AbilityName+": "+AbilityDescription;
            public override int Level=>7;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                var DarkRitual=Game.instance.model.GetTowerFromId("Adora 10").GetBehaviors<AbilityModel>().First(a=>a.name.Contains("Sacrifice")).Duplicate();
                DarkRitual.name="DarkRitual";
                DarkRitual.icon=new SpriteReference("KelThuzad-DarkRitualIcon");
                DarkRitual.displayName=AbilityName;
                DarkRitual.description=AbilityDescription;
                DarkRitual.cooldown=45;
                DarkRitual.GetBehavior<BloodSacrificeModel>().rangeBonusMultiplier=1;
                DarkRitual.GetBehavior<BloodSacrificeModel>().buffIconName=DarkRitual.icon.GUID;
                DarkRitual.GetBehavior<BloodSacrificeModel>().selectionObjectPath="KelThuzad-DarkRitualCirclePrefab";
                DarkRitual.GetBehavior<BloodSacrificeModel>().maxBonusCount=5;
                DarkRitual.RemoveBehavior<CreateSoundOnAbilityModel>();
                SoundModel sound=new SoundModel("SoundModel","kelthuzad-darkritual");
                DarkRitual.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel",sound,sound,sound));
                KelThuzad.AddBehavior(DarkRitual);
            }
        }
        public class L8:ModHeroLevel<KelThuzad>{
            public override string Description=>"Doubles Frost Nova's damage and decreases its cooldown";
            public override int Level=>8;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="FrostNova").GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().
                    projectile.GetDamageModel().damage*=2;
                KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="FrostNova").GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.GetDamageModel().damage*=2;
                KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="FrostNova").cooldown-=13;
            }
        }
        public class L9:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases Dark Ritual's attack speed bonus";
            public override int Level=>9;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetAbilities().First(a=>a.name.Equals("DarkRitual")).GetBehavior<BloodSacrificeModel>().rateBonusMultiplier=0.075f;
            }
        }
        public class L10:ModHeroLevel<KelThuzad>{
            public override string Description=>"2 more icy shards are released by Frost Nova";
            public override int Level=>10;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                CreateProjectileOnContactModel FrostNovaProj=KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="FrostNova").GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.
                    GetBehavior<CreateProjectileOnContactModel>();
                FileIOUtil.SaveObject("temp.json",FrostNovaProj);
                FrostNovaProj.emission.Cast<ArcEmissionModel>().count=+2;
                FrostNovaProj.emission.Cast<ArcEmissionModel>().angle=36;
            }
        }
        public class L11:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack damage";
            public override int Level=>11;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetAttackModel().weapons[0].projectile.GetDamageModel().damage=+4;
            }
        }
        public class L12:ModHeroLevel<KelThuzad>{
            public override string Description=>"Decreases the cooldown on Frost Nova and Dark Ritual";
            public override int Level=>12;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="FrostNova").cooldown-=10;
                KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="DarkRitual").cooldown-=15;
            }
        }
        public class L13:ModHeroLevel<KelThuzad>{
            public override string Description=>"Faster attack speed";
            public override int Level=>13;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetAttackModel().weapons[0].rate-=0.15f;
            }
        }
        public class L14:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases attack range";
            public override int Level=>14;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=20;
                KelThuzad.GetAttackModel().range+=20;
            }
        }
        public class L15:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases Dark Ritual's XP bonus";
            public override int Level=>15;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.GetBehaviors<AbilityModel>().First(a=>a.name=="DarkRitual").GetBehavior<BloodSacrificeModel>().xpMultiplier=6;
            }
        }
        /*public class L16:ModHeroLevel<KelThuzad>{
            public override string AbilityName=>"Death & Decay";
            public override string AbilityDescription=>"Deals high damage in a area to all bloons";
            public override string Description=>AbilityName+": "+AbilityDescription;
            public override int Level=>16;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                AbilityModel DeathAndDecay=blankAbilityModel.Duplicate();
                DeathAndDecay.AddBehavior(Game.instance.model.GetTowerFromId("Alchemist-040").GetAbility().GetBehavior<ActivateAttackModel>().Duplicate());
                ActivateAttackModel activateAttackModel=DeathAndDecay.GetBehavior<ActivateAttackModel>();
                activateAttackModel.attacks[0]=Game.instance.model.GetTowerFromId("BombShooter").GetAttackModel().Duplicate();
                AttackModel attackModel=activateAttackModel.attacks[0];
                ProjectileModel Proj=attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
                DeathAndDecay.name="DeathAndDecay";
                DeathAndDecay.icon=new SpriteReference("KelThuzad-DeathAndDecayIcon");
                DeathAndDecay.displayName=AbilityName;
                DeathAndDecay.description=AbilityDescription;
                DeathAndDecay.cooldown=40;
                activateAttackModel.endOnDefeatScreen=false;
                activateAttackModel.lifespan=10;
                activateAttackModel.cancelIfNoTargets=true;
                attackModel.range=9999;
                attackModel.fireWithoutTarget=false;
                attackModel.attackThroughWalls=true;
                attackModel.addsToSharedGrid=false;
                attackModel.weapons[0].rate=99999;
                attackModel.weapons[0].projectile.display="";
                attackModel.weapons[0].projectile.RemoveBehavior<DamageModel>();
                attackModel.weapons[0].projectile.AddBehavior(new InstantModel("InstantModel",false));
                attackModel.weapons[0].projectile.RemoveBehavior<TravelStraitModel>();
                Proj.display="KelThuzad-DeathAndDecayPrefab";
                Proj.GetBehavior<AgeModel>().lifespan=10;
                Proj.GetDamageModel().immuneBloonProperties=(BloonProperties)9;
                Proj.ignorePierceExhaustion=true;
                SoundModel sound=new SoundModel("SoundModel","kelthuzad-deathanddecay");
                DeathAndDecay.AddBehavior(new CreateSoundOnAbilityModel("CreateSoundOnAbilityModel",sound,sound,sound));
                KelThuzad.AddBehavior(DeathAndDecay);
            }
        }
        public class L17:ModHeroLevel<KelThuzad>{
            public override string Description=>"Normal attacks slow down bloons even more";
            public override int Level=>17;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L18:ModHeroLevel<KelThuzad>{
            public override string Description=>"16 shards are now released by Frost Nova";
            public override int Level=>18;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L19:ModHeroLevel<KelThuzad>{
            public override string Description=>"Increases the area of Death And Decay";
            public override int Level=>19;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
            }
        }
        public class L20:ModHeroLevel<KelThuzad>{
            public override string Description=>"Death & Decay can now target BAD's";
            public override int Level=>20;
            public override void ApplyUpgrade(TowerModel KelThuzad){
                KelThuzad.range+=1;
                //CustomComponents.Add("KelThuzad-DeathAndDecaySkullPrefab",Il2CppType.Of<RotatingParticleSystem>());
            }
        }*/
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
                            __instance.tower.Node.graphic.GetComponent<Animator>().Play("KelThuzadDeathAndDecayStart");
                            break;
                    }
                }
            }
        }
    }
}
