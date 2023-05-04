using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable
{
    [SerializeField] private CharacterController _characterController;

    #region IMOVEABLE_PROPERTIES
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;
    #endregion

    #region IMOVEABLE_METHODS
    public void Move(Vector3 direction)
    {
        _characterController.Move(direction * Time.deltaTime * Speed);
    }
    #endregion

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
}
