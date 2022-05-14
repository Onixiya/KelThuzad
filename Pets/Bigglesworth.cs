namespace WarExpansion.Pets{
    [HarmonyPatch(typeof(Pet),"Initialise")]
    public class test{
        [HarmonyPostfix]
        public static void Postfix(Entity target,Model modelToUse){
            Log(modelToUse.Cast<PetModel>().name);
        }
    }
}
