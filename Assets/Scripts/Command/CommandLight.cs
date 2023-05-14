using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLight : ICommand
{
    private ILight _light;

    public CommandLight(ILight light)
    {
        _light = light;
    }

    public void Execute() 
    {
        _light.Toggle();
        // EventManager.instance.AvatarChange();
    }
}
