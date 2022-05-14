namespace KelThuzad.Pets{
    /*[HarmonyPatch(typeof(Modding),"CheckForMods")]
    public class Bigglesworth{
        [HarmonyPostfix]
        public static void Postfix(ref bool __result){
            __result=false;
        }
    }
    [HarmonyPatch(typeof(Btd6Player),"Save")]
    public class thing{
        public static bool ran=false;
        [HarmonyPostfix]
        public static void Postfix(ref Btd6Player __instance){
            if(ran==false&&idfk.Count!=0){
                foreach(string item in idfk){
                    Log(item);
                    __instance.AddTrophyStoreItem(item);
                }
                ran=true;
            }
        }
    }*/
    [HarmonyPatch(typeof(Pet),"Initialise")]
    public class test{
        [HarmonyPostfix]
        public static void Postfix(Entity target,Model modelToUse){
            Log(modelToUse.Cast<PetModel>().name);
        }
    }
}
