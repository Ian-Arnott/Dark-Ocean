using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _keysPickedUp = 0;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private List<GameObject> _indicators;
    [SerializeField] private Material _materialGreen;

    private void Start()
    {
        _canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        EventManager.instance.OnGameOver += OnGameOver;
        EventManager.instance.OnKeyPickup += OnKeyPickup;
    }


    #region EVENT_ACTIONS
    private void OnGameOver(bool isVictory)
    {
        GlobalVictory.instance.IsVictory = isVictory;
        if(isVictory) EventQueueManager.instance.AddEvent(new CommandDoor(_door.GetComponent<IDoor>()));
        else            _canvas.SetActive(true);
        Invoke("LoadEndgameScene", 3f);
    }

    private void OnKeyPickup()
    {
        _indicators[_keysPickedUp++].GetComponent<MeshRenderer>().material = _materialGreen;
        if (_keysPickedUp == 4)
        {
            EventQueueManager.instance.AddEvent(new CommandDoor(_door.GetComponent<IDoor>()));
        }
    }
    #endregion

    private void LoadEndgameScene() => SceneManager.LoadScene("EndScene");
}
