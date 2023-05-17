using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLockTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCapsule")
        {
            EventManager.instance.EventGameOver(true);
        } 
            
    }
}
