using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour, IDoor
{
    [SerializeField] private GameObject _leftDoor;    
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private bool _isOpen;
    

    #region IDOOR_PROPERTIES
    public GameObject Door => transform.Find("Door_Double").gameObject;
    #endregion

    void Start()
    {
        _isOpen = false;
        // Get a reference to the child GameObject by name
        _leftDoor = Door.transform.Find("Door_Double.L").gameObject;
        _rightDoor = Door.transform.Find("Door_Double.R").gameObject;
    }

    public void OpenDoor()
    {
        if(!_isOpen)
        {
            // Move the left door to the left
            _leftDoor.transform.position += _leftDoor.transform.right;
            // Move the right door to the right
            _rightDoor.transform.position -= _rightDoor.transform.right;
            _isOpen = true;
        }
        else
        {
            _leftDoor.transform.position -= _leftDoor.transform.right;
            _rightDoor.transform.position += _rightDoor.transform.right;
            _isOpen = false;
        }
    }
}
