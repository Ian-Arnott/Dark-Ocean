using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /* Image References */

    [SerializeField] private List<GameObject> _back;
    [SerializeField] private List<GameObject> _charge;

    /* Text References */


    /* Variables */
    private int _currentIdx;

    private void Start()
    {
        _currentIdx = 0;
        /* Suscripcion de eventos */
        EventManager.instance.OnLightChange += UpdateLight;
        EventManager.instance.OnBatteryChange += UpdateBattery;

    }

    private void UpdateLight(int idx)
    {
        _back[_currentIdx].SetActive(false);
        _charge[_currentIdx].SetActive(false);
        _currentIdx = idx;
        _back[_currentIdx].SetActive(true);
        _charge[_currentIdx].SetActive(true);
        
    }

    private void UpdateBattery(float current, float max)
    {
        _charge[_currentIdx].GetComponent<Image>().fillAmount = current/100;
    } 

}
