using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour, IDoor
{
    [SerializeField] private GameObject _leftDoor;    
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isOpen;
    [SerializeField] private AudioSource _audioSource;
    

    #region IDOOR_PROPERTIES
    public GameObject Door => transform.Find("Door_Double").gameObject;
    #endregion

    void Start()
    {
        _isOpen = false;
        _leftDoor = Door.transform.Find("Door_Double.L").gameObject;
        _rightDoor = Door.transform.Find("Door_Double.R").gameObject;
    }

    public void OpenDoor()
    {
        if(!_isOpen)
        {
            _animator.Play("DoubleDoorOpen",0,0.0f);
            _isOpen = true;
        }
        else
        {
            _animator.Play("DoubleDoorClose",0,0.0f);
            _isOpen = false;
        }
        _audioSource.Play();
    }
}
