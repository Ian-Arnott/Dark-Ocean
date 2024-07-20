using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, ILight
{
    [SerializeField] protected LightStats _stats;
    [SerializeField] protected GameObject _lightObject;
    protected bool _isOn;

    #region LIGHT_PROPERTIES
    [SerializeField] protected float _batteryLife;

    #endregion

    #region I_LIGHT_PROPERTIES
    public float BatteryLife => _stats.BatteryLife;
    public float BatteryRate => _stats.BatteryRate;
    public float Intensity => _stats.Intensity;
    public float Range => _stats.Range;
    #endregion

    #region UNITY_EVENTS
    private void Start()
    {
        _batteryLife = BatteryLife;
        _lightObject.SetActive(false);

        _isOn = false;
    }

    private void Update()
    {
        if (_isOn)
        {
            _batteryLife -= BatteryRate * Time.deltaTime;
            UI_Updater();
        }
        if (_isOn && _batteryLife < 0) Toggle();
    }
    #endregion

    #region I_LIGHT_PROPERTIES
    public virtual void Toggle()
    {
        if (!_isOn && _batteryLife > 0)
        {
            _lightObject.SetActive(true);
            _lightObject.GetComponent<Light>().intensity = Intensity;
            _lightObject.GetComponent<Light>().range = Range;
            _lightObject.transform.parent = transform;
            _isOn = true;
        }
        else
        {
            _lightObject.SetActive(false);
            _isOn = false;
        }
    }

    #endregion


    public void UI_Updater() => EventManager.instance.BatteryChange(_batteryLife, BatteryLife);
}
