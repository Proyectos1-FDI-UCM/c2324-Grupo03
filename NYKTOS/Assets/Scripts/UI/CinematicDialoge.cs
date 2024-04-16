using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinematicDialoge : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string[] _lines;
    [SerializeField]
    private float _textSpeed = 0.1f;

    int index;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void StartDialoge()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in _lines[index].ToCharArray())
        {
            _text.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}
