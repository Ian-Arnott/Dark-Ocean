using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILight
{
    float BatteryLife { get; }

    float Intensity { get; }
    float Range { get; }

    float BatteryRate { get; }

    void Toggle();
}
