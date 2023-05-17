using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _keysPickedUp = 0;
    [SerializeField] private GameObject _door;
    [SerializeField] private List<GameObject> _indicators;
    [SerializeField] private Material _materialGreen;

    private void Start()
    {
        EventManager.instance.OnGameOver += OnGameOver;
        EventManager.instance.OnKeyPickup += OnKeyPickup;
    }


    #region EVENT_ACTIONS
    private void OnGameOver(bool isVictory)
    {
        EventQueueManager.instance.AddEvent(new CommandDoor(_door.GetComponent<IDoor>()));
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
