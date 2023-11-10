using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Py.LibLocalization.Internal.Utils
{
    public static class HarmonyLoader
    {
        public static bool LoadHarmony() =>
            LoadHarmony(
                Path.Combine(
                    Path.GetDirectoryName(Application.dataPath)!,
                    "DemeoMods",
                    "Py.Localization"
                )
            );

        public static bool LoadHarmony(string modDir)
        {
            if (CheckAlreadyLoaded()) return true;
            if (!Directory.Exists(modDir)) return false;
        
            foreach (var harmonyFile in Directory.GetFiles(modDir, "0Harmony.dll"))
            {
                try
                {
                    Assembly.LoadFile(harmonyFile);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    
        private static bool CheckAlreadyLoaded()
        {
            var harmonyType = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.Name == "Harmony" && type.Namespace == "HarmonyLib"
                select type).FirstOrDefault();

            return harmonyType != null;
        }
    }
}