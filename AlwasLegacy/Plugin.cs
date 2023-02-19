using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace AlwasLegacy;

[BepInPlugin("p1xel8ted.alwaslegacy.fixes", "Alwa's Legacy Fixes", "0.1.1")]
[HarmonyPatch]
public partial class Plugin : BaseUnityPlugin
{
    private static Harmony _hi;
    private static ManualLogSource _log;
    private static ConfigEntry<int> _horizontalScrollAdjustment = null!;
    private static ConfigEntry<float> _dialogScale = null!;
    
    public void Awake()
    {
        _horizontalScrollAdjustment = Config.Bind("General", "HorizontalScrollAdjustment", 300, "Change when the game initiates a scroll. The current setting will scroll right when player is within 300 pixels of the left/right edge of the screen. This setting allows you to adjust that value.");
        _dialogScale = Config.Bind("General", "DialogScale", 0.75f, "Scale of the NPC dialogues and item pickup dialogues. 0.75 is the default (makes it a littler smaller).");
        
        _hi = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        _log = new ManualLogSource("Log");
        BepInEx.Logging.Logger.Sources.Add(_log);
        _log.LogInfo($"Plugin Alwa's Legacy Fixes is loaded!");
    }

    public void OnDestroy()
    {
        _hi?.UnpatchSelf();
    }
    

}