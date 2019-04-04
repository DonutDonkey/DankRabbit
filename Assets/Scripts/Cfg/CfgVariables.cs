using System.IO;
using UnityEngine;
using SharpConfig;

public class CfgVariables
{
    //Default Variables

    private static KeyCode movementLeftKey  = KeyCode.A;

    private static KeyCode movementRightKey = KeyCode.D;

    private static KeyCode movementJumpKey  = KeyCode.Space;

    private static KeyCode fireKey          = KeyCode.F;

    private string cfgPath;

    private Configuration cfg = new Configuration();

    void Start()
    {
        cfgPath = "cfg.cfg"; //todo: make path work with pc

        if(!File.Exists(cfgPath)) {
            Debug.Log("Setting up default cfg file");
            CreateDefaultCfg();
            SaveConfig();
        }

        cfg = Configuration.LoadFromFile(cfgPath);

        LoadSettings();
    }

    private void CreateDefaultCfg()
    {
        cfg["Keybinds"]["Left"].IntValue  = (int)movementLeftKey;
        cfg["Keybinds"]["Right"].IntValue = (int)movementRightKey;
        cfg["Keybinds"]["Jump"].IntValue  = (int)movementJumpKey;
        cfg["Keybinds"]["Fire"].IntValue = (int)fireKey;
    }

    private void SaveConfig()
    {
        Debug.Log("Saving Client config...");

        cfg.SaveToFile(cfgPath);
    }


    private void LoadSettings()
    {
        var section = cfg["Keybinds"];

        movementLeftKey  = (KeyCode)section["Left"].IntValue;
        movementRightKey = (KeyCode)section["Right"].IntValue;
        movementJumpKey  = (KeyCode)section["Jump"].IntValue;
        fireKey          = (KeyCode)section["Fire"].IntValue;
    }

    #region Getters

    public static KeyCode GetMovementLeftKey()
    {
        return movementLeftKey;
    }

    public static KeyCode GetMovementRightKey()
    {
        return movementRightKey;
    }

    public static KeyCode GetMovementJumpKey()
    {
        return movementJumpKey;
    }

    public static KeyCode GetFireKey()
    {
        return fireKey;
    }

    #endregion
}
