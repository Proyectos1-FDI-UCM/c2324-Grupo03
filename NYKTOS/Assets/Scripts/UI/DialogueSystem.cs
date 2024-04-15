using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    private bool onDialogue = false;
    private DialogueScriptableObject[] dialogues;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] GameObject textBox;
    private void Awake()
    {
        textBox.SetActive(false);
        dialogues = Resources.LoadAll<DialogueScriptableObject>("DialogueResources");

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogues[i].dialogueStarted.AddListener((string[] dialogueBoxes)=>
            {
                if (!onDialogue)
                {
                    StartCoroutine(StartDialogue(dialogueBoxes));
                }
                else Debug.LogError("Se ha intentado reproducir un dialogo mientras el jugador ya se encuentra en uno.");
                
            }) ;
        }
    }

    private IEnumerator StartDialogue(string[] boxes)
    {
        onDialogue = true;
        textBox.SetActive(true);
        for (int i = 0;i < boxes.Length;i++)
        {
            text.text = "";
            for (int j =0; j < boxes[i].Length;j++)
            {
                text.text = text.text + boxes[i][j];
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1f);
        }
        textBox.SetActive(false);
        onDialogue = false;
    }


}
