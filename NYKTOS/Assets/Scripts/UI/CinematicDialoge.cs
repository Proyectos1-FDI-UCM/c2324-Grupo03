using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CinematicDialoge : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _lines;
    [SerializeField] private float _textSpeed = 0.2f;
    [SerializeField] private float _lineSpeed = 3f;

    //[SerializeField] private float _waitForStart = 2.5f;

    [SerializeField] private UnityEvent voice = new UnityEvent();

    private int _index;

    private void Start()
    {
        _text.text = string.Empty;
        _index = 0;
        //StartCoroutine(StartDialogueAfterDelay(_waitForStart));
    }

    public void InitializeCoroutineDialogue()
    {
        StartCoroutine(StartDialogue());
    }

    //private IEnumerator StartDialogueAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay); 
    //    StartCoroutine(StartDialogue());
    //}

    private IEnumerator StartDialogue()
    {

        while (_index < _lines.Length)
        {
            _text.text = string.Empty;
            yield return StartCoroutine(WriteLine(_lines[_index]));
            yield return new WaitForSeconds(_lineSpeed);//Espera entre l�neas
            _index++;
        }

        //Fin
        gameObject.SetActive(false);
    }

    private IEnumerator WriteLine(string line)
    {
        foreach (char letter in line)
        {
            _text.text += letter;
            voice?.Invoke();
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}

