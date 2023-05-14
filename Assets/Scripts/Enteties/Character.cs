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

    void Start()
    {
        _commandLight = new CommandLight(_flash);
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

    }

}
