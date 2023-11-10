using System;
using Boardgame.Modding;
using Fidelity.Singleton;
using Py.LibLocalization.Internal.Utils;

namespace Py.LibLocalization.Internal
{
    // ReSharper disable once UnusedType.Global
    public class LibLocalization : DemeoMod
    {
        public override void OnEarlyInit()
        {
            if (HarmonyLoader.LoadHarmony())
            {
                ModPatches.Patch();
            }
            else
            {
                Console.WriteLine("[Py.LibLocalization] ERROR | Failed to load Harmony, Py.LibLocalization WILL NOT WORK.");
            }
        }

        public override void Load()
        {
            Singleton<LocaleRegistry>.Instance.Refresh();
        }

        public override ModdingAPI.ModInformation ModInformation { get; } = new()
        {
            name = "Py.LibLocalization",
            author = "JustPyrrha",
            version = "1.0.0",
            description = "Mod localization library.",
            isNetworkCompatible = true
        };
    }
}
