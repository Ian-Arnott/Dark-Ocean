using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private DoubleDoorController _doubleDoorController;
    [SerializeField] private GameObject door;


    // BINDING  KEYS
    [SerializeField] private KeyCode _interact = KeyCode.E;

    #region COMMANDS
    private CommandDoor _commandOpenDoor;
    #endregion

    void Start()
    {
        _doubleDoorController = door.GetComponent<DoubleDoorController>();

        _commandOpenDoor = new CommandDoor(_doubleDoorController);

    }

    void Update()
    {
        if (Input.GetKeyDown(_interact)) EventQueueManager.instance.AddEvent(_commandOpenDoor);

    }

}
