using System.Runtime.CompilerServices;
using Utils;

namespace Py.LibLocalization.Internal.Utils
{
    public static class ModLog
    {
        private const string Prefix = "Py.LibLocalization";
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(string message) => DemeoLog.Log(Prefix, message);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogWarn(string message) => DemeoLog.LogWarn(Prefix, message);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogError(string message) => DemeoLog.LogError(Prefix, message);
    }
}