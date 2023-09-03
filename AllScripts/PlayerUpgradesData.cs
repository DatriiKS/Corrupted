using Newtonsoft.Json;
using System;

[Serializable]
public class PlayerUpgradesData : PersistentDataBase
{
    [JsonProperty]
    public float ResultMultiplier { get; private set; }
    [JsonProperty]
    public int ResultMultiplierCost { get; set; }
    [JsonProperty]
    public float SpeedMultiplier { get; private set; }
    [JsonProperty]
    public int SpeedMultiplierCost { get; set; }
    [JsonProperty]
    public float LaunchForceMultiplier { get; private set; }
    [JsonProperty]
    public int LaunchForceMultiplierCost { get; set; }

    public void UpgradeSpeedMultiplier(float increment)
    {
        SpeedMultiplier += increment;
    }
    public void UpgradeLaunchForceMultiplier(float increment)
    {
        LaunchForceMultiplier += increment;
    }
    public void UpgradeResultMultiplier(float increment)
    {
        ResultMultiplier += increment;
    }

    protected override void OnDataObjectCreated()
    {
        ResultMultiplier = 10;
        SpeedMultiplier = 10;
        LaunchForceMultiplier = 10;
        ResultMultiplierCost = 100;
        SpeedMultiplierCost = 100;
        LaunchForceMultiplierCost = 100;
    }
}
