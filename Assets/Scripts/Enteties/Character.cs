using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 2f;
    [SerializeField] private Flashlight _flash;

    // BINDING  KEYS
    [SerializeField] private KeyCode _interact = KeyCode.E;
    [SerializeField] private KeyCode _toggle = KeyCode.Mouse0;
    [SerializeField] private KeyCode _throwFlashbang = KeyCode.Mouse1;

    #region COMMANDS
    private CommandLight _commandLight;
    #endregion

    // SWITCH LIGHT
    [SerializeField] private KeyCode _flashlight = KeyCode.Alpha1;
    [SerializeField] private KeyCode _lantern = KeyCode.Alpha2;
    [SerializeField] private List<Flashlight> _flashlights;
    private bool _isOn;

    // FLASHBANG PREFAB
    [SerializeField] private GameObject _flashbangPrefab;
    [SerializeField] private Transform _throwPoint;

    void Start()
    {
        _isOn = false;
        ChangeInventory(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(_interact))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _interactDistance))
            {
                if (hit.transform.CompareTag("Console"))
                {
                    EventQueueManager.instance.AddEvent(new CommandDoor(hit.transform.parent.GetComponent<IDoor>()));
                }
            }
            Debug.DrawRay(transform.position, transform.forward * _interactDistance, Color.green, 1f); // Draw the raycast
        }
        if (Input.GetKeyDown(_toggle))
        {
            EventQueueManager.instance.AddEventToQueue(_commandLight);
            _isOn = !_isOn;
        }
        if (Input.GetKeyDown(_flashlight)) ChangeInventory(0);
        if (Input.GetKeyDown(_lantern)) ChangeInventory(1);

        if (Input.GetKeyDown(_throwFlashbang))
        {
            ThrowFlashbang();
        }
    }

    private void ChangeInventory(int index)
    {
        if (_isOn)
        {
            EventQueueManager.instance.AddEventToQueue(_commandLight);
            _isOn = false;
        }
        foreach (var light in _flashlights) light.gameObject.SetActive(false);
        _flashlights[index].gameObject.SetActive(true);
        EventManager.instance.LightChange(index);
        _flash = _flashlights[index];
        _commandLight = new CommandLight(_flash);
    }

    private void ThrowFlashbang()
    {
        GameObject flashbang = Instantiate(_flashbangPrefab, _throwPoint.position, _throwPoint.rotation);
        IThrowable throwable = flashbang.GetComponent<IThrowable>();
        throwable.Throw();
    }
}
