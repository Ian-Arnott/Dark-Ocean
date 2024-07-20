using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] TMP_Text _tmpText;
    [SerializeField] String _text;

    public void OnTriggerEnter()
    {
        _tmpText.SetText(_text);
    }
}
