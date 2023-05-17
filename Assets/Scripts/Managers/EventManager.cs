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
    public event Action<int, int> OnBatteryChange;
    public event Action<int> OnLigtChange;
    public void BatteryChange(int currentBattery, int maxBattery)
    {
        if (OnBatteryChange != null) OnBatteryChange(currentBattery, maxBattery);
    }

    public void LightChange(int lightIndex)
    {
        if (OnLigtChange != null) OnLigtChange(lightIndex);
    }
    #endregion
}