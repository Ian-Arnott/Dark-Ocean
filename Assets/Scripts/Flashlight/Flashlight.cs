using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, ILight
{
    [SerializeField] protected LightStats _stats;
    private bool _isOn;

    #region LIGHT_PROPERTIES
    [SerializeField] protected float _batteryRate;
    [SerializeField] protected float _batteryLife;

    #endregion

    #region I_LIGHT_PROPERTIES
    public float BatteryLife => _stats.BatteryLife;
    public float BatteryRate => _stats.BatteryRate;
    public float Intensity => _stats.Intensity;

    public GameObject LightPrefab => _stats.LightPrefab;
    #endregion

    #region UNITY_EVENTS
    private void Start()
    {
        _batteryRate = BatteryRate;
        _batteryLife = BatteryLife;
        _isOn = false;
        // UI_Updater();
    }

    private void Update()
    {
        if (_isOn) _batteryLife -= _batteryRate * Time.deltaTime;
    }
    #endregion

    #region I_LIGHT_PROPERTIES
    private GameObject lightObject;
    public virtual void Toggle()
    {
        if (!_isOn && _batteryLife > 0)
        {
            lightObject = Instantiate(LightPrefab, transform.position, transform.rotation);
            lightObject.GetComponent<Light>().intensity = Intensity;
            lightObject.transform.parent = transform;
            
            // Define the rotation quaternion
            Quaternion xRotation = Quaternion.AngleAxis(-90, Vector3.right);
            // Apply the rotation to the lightObject
            lightObject.transform.rotation = lightObject.transform.rotation * xRotation;
            
            _isOn = true;
            // UI_Updater();
        }
        else
        {
            Destroy(lightObject);
            _isOn = false;
            // UI_Updater();
        }
    }
    #endregion

    // public void UI_Updater() => EventManager.instance.BatteryChange(_batteryLife);
}
