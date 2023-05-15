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


    #region COMMANDS
    private CommandDoor _commandOpenDoor;
    private CommandLight _commandLight;
    #endregion

    // SWITCH LIGHT
    [SerializeField] private KeyCode _flashlight = KeyCode.Alpha1;
    [SerializeField] private KeyCode _lantern = KeyCode.Alpha2;
    [SerializeField] private List<Flashlight> _flashlights;

    void Start()
    {
        ChangeWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(_interact)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _interactDistance)) {
                if (hit.transform.CompareTag("Console")) {
                    EventQueueManager.instance.AddEvent(new CommandDoor(hit.transform.parent.GetComponent<IDoor>()));
                }
            }
        }
        if (Input.GetKeyDown(_toggle)) EventQueueManager.instance.AddEvent(_commandLight);
        if (Input.GetKeyDown(_flashlight)) ChangeWeapon(0);
        if (Input.GetKeyDown(_lantern)) ChangeWeapon(1);

    }


    private void ChangeWeapon(int index)
    {
        foreach (var light in _flashlights) light.gameObject.SetActive(false);
        _flashlights[index].gameObject.SetActive(true);
        EventManager.instance.LightChange(index);
        
        _flash = _flashlights[index];
        // _flash.UI_Updater();
        _commandLight = new CommandLight(_flash);
    }

}
