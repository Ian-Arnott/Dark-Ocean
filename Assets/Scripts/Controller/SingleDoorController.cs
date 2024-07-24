using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorController : MonoBehaviour, IDoor
{

    [SerializeField] private bool _isOpen;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;


    #region IDOOR_PROPERTIES
    public GameObject Door => transform.Find("Door_Single").gameObject;
    #endregion

    void Start()
    {
        _isOpen = false;
    }

    public void OpenDoor()
    {
        if(!_isOpen)
        {
            _animator.Play("SingleDoorOpen",0,0.0f);
            _isOpen = true;
        }
        else
        {
            _animator.Play("SingleDoorClose",0,0.0f);
            _isOpen = false;            
        }
        _audioSource.Play();
    }
}
