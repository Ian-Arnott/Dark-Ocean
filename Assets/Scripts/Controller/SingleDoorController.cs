using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorController : MonoBehaviour, IDoor
{

    [SerializeField] private bool _isOpen;
    

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
            Door.transform.position += new Vector3(0f, 2.5f, 0f);
            _isOpen = true;
        }
    }
}
