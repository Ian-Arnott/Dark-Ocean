using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Flashlight
{

    #region I_LIGHT_PROPERTIES
    public override void Toggle()
    {
        if (!_isOn && _batteryLife > 0)
        {
            _lightObject = Instantiate(LightPrefab, transform.position, transform.rotation);
            _lightObject.GetComponent<Light>().intensity = Intensity;
            _lightObject.GetComponent<Light>().range = Range;
            _lightObject.transform.parent = transform;
            _isOn = true;
            // UI_Updater();
        }
        else
        {
            Destroy(_lightObject);
            _isOn = false;
            // UI_Updater();
        }
    }
    #endregion
}