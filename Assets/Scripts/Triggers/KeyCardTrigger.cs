using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCapsule")
        {
            EventManager.instance.EventKeyPickup();
            Destroy(this.transform.parent.gameObject);
        } 
            
    }
}
