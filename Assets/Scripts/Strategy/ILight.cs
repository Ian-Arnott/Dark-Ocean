using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILight
{
    GameObject LightPrefab { get; }

    float BatteryLife { get; }

    float Intensity { get; }
    float Range { get; }

    float BatteryRate { get; }

    void Toggle();
}
