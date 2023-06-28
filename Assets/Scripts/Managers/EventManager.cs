using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    static public EventManager instance;

    #region UNITY_EVENTS
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    #endregion

    #region GAME_MANAGER
    public event Action<bool> OnGameOver;
    public event Action OnKeyPickup;

    public void EventGameOver(bool isVictory) 
    {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public void EventKeyPickup() 
    {
        if (OnGameOver != null) OnKeyPickup();
    }
    #endregion

    #region IN_GAME_UI
    public event Action<float, float> OnBatteryChange;
    public event Action<float> OnLookTimeChange;

    public event Action<int> OnLightChange;

    public void LookTimeChange(float currentTime)
    {
        if (OnBatteryChange != null) OnLookTimeChange(currentTime);
    }
    public void BatteryChange(float currentBattery, float maxBattery)
    {
        if (OnBatteryChange != null) OnBatteryChange(currentBattery, maxBattery);
    }

    public void LightChange(int lightIndex)
    {
        if (OnLightChange != null) OnLightChange(lightIndex);
    }
    #endregion

    #region IN_GAME_AUDIO
    public event Action OnDoorChange;
    public event Action OnLigtOnOffChange;
    public void LigtOnOffChange()
    {
        if (OnLigtOnOffChange != null) OnLigtOnOffChange();
    }

    public void DoorChange()
    {
        if (OnDoorChange != null) OnDoorChange();
    }
    #endregion
}