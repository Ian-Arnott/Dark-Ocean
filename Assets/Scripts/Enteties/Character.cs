using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{


    [SerializeField] private float _interactDistance = 2f;
    // BINDING  KEYS
    [SerializeField] private KeyCode _interact = KeyCode.E;

    #region COMMANDS
    private CommandDoor _commandOpenDoor;
    #endregion

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
    }

}
