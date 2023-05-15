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
    public GameObject LightPrefab => _stats.LightPrefab;
    #endregion

    #region UNITY_EVENTS
    private void Start()
    {
        _batteryLife = BatteryLife;
        _isOn = false;
        // UI_Updater();
    }

    private void Update()
    {
        if (_isOn) _batteryLife -= BatteryRate * Time.deltaTime;
        if (_isOn && _batteryLife < 0) Toggle();
    }
    #endregion

    #region I_LIGHT_PROPERTIES
    public virtual void Toggle()
    {
        if (!_isOn && _batteryLife > 0)
        {
            _lightObject = Instantiate(LightPrefab, transform.position, transform.rotation);
            _lightObject.GetComponent<Light>().intensity = Intensity;
            _lightObject.GetComponent<Light>().range = Range;
            _lightObject.transform.parent = transform;

            // Define the rotation quaternion
            Quaternion xRotation = Quaternion.AngleAxis(-90, Vector3.right);
            // Apply the rotation to the lightObject
            _lightObject.transform.rotation = _lightObject.transform.rotation * xRotation;

            // Move the _lightObject on the local Y axis by 1
            _lightObject.transform.localPosition += new Vector3(0, 0.175f, 0);

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

    // public void UI_Updater() => EventManager.instance.BatteryChange(_batteryLife);
}
