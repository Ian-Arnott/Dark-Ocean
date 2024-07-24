using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text.text = GlobalVictory.instance.IsVictory ? "YOU ESCAPED" : "YOU DIED";
    }

}
