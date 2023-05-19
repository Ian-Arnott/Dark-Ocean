using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Monster : MonoBehaviour
{
    public float MaxSpeed => GetComponent<Actor>().Stats.MaxSpeed;
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerCapsule")
        {
            EventManager.instance.EventGameOver(false);
        } 
    }

}