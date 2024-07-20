using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /* Image References */

    [SerializeField] private List<GameObject> _back;
    [SerializeField] private List<GameObject> _charge;
    [SerializeField] private GameObject _sprintBar;
    [SerializeField] private Volume _volume;
    [SerializeField] private Vignette _vignette;


    /* Text References */


    /* Variables */
    private int _currentIdx;

    private void Start()
    {
        _currentIdx = 0;
        /* Suscripcion de eventos */
        EventManager.instance.OnLightChange += UpdateLight;
        EventManager.instance.OnBatteryChange += UpdateBattery;
        EventManager.instance.OnSprint += UpdateBar;
        EventManager.instance.OnLookTimeChange += UpdateVignette;

         // Get Vignette from Volume
        if (_volume.profile.TryGet(out _vignette))
        {
            Debug.Log("Vignette component found.");
        }
        else
        {
            Debug.LogError("Vignette component not found.");
        }

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

    private void UpdateBar(float current, float max)
    {
        _sprintBar.GetComponent<Image>().fillAmount = current/max;
    } 

    private void UpdateVignette(float current)
    {
        if (_vignette!=null) _vignette.intensity.value = current/5;
    }

}
