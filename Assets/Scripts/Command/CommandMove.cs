using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : ICommand
{
    private IMoveable _moveable;
    private Vector3 _direction;

    public CommandMove(IMoveable moveable, Vector3 direction)
    {
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute() => _moveable.Move(_direction);
}
