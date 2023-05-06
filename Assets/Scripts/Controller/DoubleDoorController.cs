using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour, IDoor
{
    [SerializeField] private GameObject _leftDoor;    
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private bool _isOpen;
    

    #region IDOOR_PROPERTIES
    public GameObject Door => GameObject.Find("Door_Double");
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
            _leftDoor.transform.position += new Vector3(0f, 0f, -1f);
            // Move the right door to the right
            _rightDoor.transform.position += new Vector3(0f, 0f, 1f);
            _isOpen = true;
        }
    }
}
