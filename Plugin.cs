using Assets.Scripts.Objects;
using BepInEx;
using HarmonyLib;
using Objects.Electrical;
using UnityEngine;

namespace MagnifyingGlass
{
    [BepInPlugin("MagnifyingGlass", "Magnifying Glass", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static BepInEx.Logging.ManualLogSource PluginLogger;

        private void Awake()
        {
            PluginLogger = Logger;
            new Harmony(nameof(MagnifyingGlass)).PatchAll(typeof(Patches));
        }
    }

    internal class Patches
    {
        [HarmonyPatch(typeof(AdvancedComposter), nameof(AdvancedComposter.GetPassiveTooltip))]
        [HarmonyPostfix]
        public static void ComposterExtraText(
            Collider hitCollider, ref PassiveTooltip __result, AdvancedComposter __instance)
        {
            if (hitCollider == __instance.InfoPanel && __instance.InfoPanel != null) {
                __result.State += string.Format(
                    "Biomass: {0}\nDecayed: {1}\nFood: {2}\n",
                    __instance.BiomassQuantity,
                    __instance.DecayFoodQuantity,
                    __instance.NormalFoodQuantity);
            }
        }
    }
}
