using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    #region controles de dialogo
    private bool onDialogue = false;
    private bool resumeDialogue = false;
    #endregion

    #region properties
    private DialogueScriptableObject[] dialogues;
    #endregion

    #region references
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] GameObject textBox;

    [SerializeField] private BoolEmitter enablePlayerActions;
    #endregion
    private void Awake()
    {
        textBox.SetActive(false);
        dialogues = Resources.LoadAll<DialogueScriptableObject>("DialogueResources");

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogues[i].dialogueStarted.AddListener((string[] dialogueBoxes, bool canMove)=>
            {
                if (!onDialogue)
                {
                    StartCoroutine(StartDialogue(dialogueBoxes, canMove));
                }
                else Debug.LogError("Se ha intentado reproducir un dialogo mientras el jugador ya se encuentra en uno.");
                
            }) ;
        }
    }

    
    private IEnumerator StartDialogue(string[] boxes, bool canMove)
    {
        if (!canMove)
        {
            enablePlayerActions?.InvokePerform(false);
        }
        
        onDialogue = true;
        textBox.SetActive(true);
        for (int i = 0;i < boxes.Length;i++)
        {
            text.text = "";
            for (int j =0; j < boxes[i].Length && text.text != boxes[i] ;j++)
            {
                if (resumeDialogue)
                {
                    text.text = boxes[i];
                    resumeDialogue = false;
                }
                else
                {
                    text.text = text.text + boxes[i][j];
                    yield return new WaitForSeconds(0.05f);
                }
            }
            yield return new WaitUntil(() => resumeDialogue);
            resumeDialogue = false;
        }
        textBox.SetActive(false);
        onDialogue = false;

        if (!canMove)
        {
            enablePlayerActions?.InvokePerform(true);
        }
        
    }

    private void OnDestroy()
    {
        for (int i=0;i < dialogues.Length; i++)
        {
            dialogues[i].dialogueStarted.RemoveAllListeners();
        }
    }

    public void ResumeDialogue()
    {
        if (onDialogue) resumeDialogue = true;
        else resumeDialogue = false;
    }

}
