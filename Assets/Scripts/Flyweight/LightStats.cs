using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightStats", menuName = "Stats/Flashlights", order = 0)]
public class LightStats : ScriptableObject
{
    [SerializeField] private StatsValues _stats;

    public float BatteryLife => _stats.BatteryLife;
    public float BatteryRate => _stats.BatteryRate;
    public float Intensity => _stats.Intensity;
    public float Range => _stats.Range;
}

[System.Serializable]
public struct StatsValues
{
    public float Intensity;
    public float Range;
    public float BatteryLife;
    public float BatteryRate;

}
