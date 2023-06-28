using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCapsule")
        {
            EventManager.instance.EventGameOver(false);
        } 
    }

}