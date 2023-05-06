using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDoor : ICommand
{
    private IDoor _door;

    public CommandDoor(IDoor door)
    {
        _door = door;
    }

    public void Execute() => _door.OpenDoor();
}
