using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHolorgram : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed;

    void Update()
    {
        transform.Rotate(_rotationSpeed*Time.deltaTime);
    }
}
