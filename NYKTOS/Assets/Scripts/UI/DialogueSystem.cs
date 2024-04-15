using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    #region controles de dialogo
    private bool onDialogue = false;
    private bool resumeDialogue = false;

    private bool performedEvent = false;
    #endregion

    #region properties
    private DialogueScriptableObject[] talkingDialogues;
    private ActionDialogueScriptableObject[] actionDialogues;
    #endregion

    #region references
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] GameObject textBox;

    [SerializeField] private BoolEmitter enablePlayerActions;
    #endregion
    private void Awake()
    {
        textBox.SetActive(false);
        talkingDialogues = Resources.LoadAll<DialogueScriptableObject>("DialogueResources");
        actionDialogues = Resources.LoadAll<ActionDialogueScriptableObject>("DialogueResources");

        //Suscripcion de metodo de TALKING DIALOGUES
        for (int i = 0; i < talkingDialogues.Length; i++)
        {
            talkingDialogues[i].dialogueStarted.AddListener((string[] dialogueBoxes, DialogueScriptableObject dialogue) =>
            {
                if (!onDialogue)
                {
                    StartCoroutine(StartTalkingDialogue(dialogueBoxes, dialogue));
                    
                }
                else Debug.LogError("Se ha intentado reproducir un dialogo normal mientras el jugador ya se encuentra en uno.");

            });
        }

        //Suscripcion de metodo de ACTION DIALOGUES
        for (int i = 0;i < actionDialogues.Length; i++)
        {
            actionDialogues[i].dialogueStarted.AddListener((string dialogueBox, ActionDialogueScriptableObject dialogue, VoidEmitter emitter) =>
            {
                if (!onDialogue)
                {
                    StartCoroutine(StartActionDialogue(dialogueBox, dialogue, emitter));
                }
                else Debug.LogError("Se ha intentado reproducir un dialogo de accion mientras el jugador ya se encuentra en uno.");
            });
        }
    }


    private IEnumerator StartTalkingDialogue(string[] boxes, DialogueScriptableObject dialogue)
    {
        enablePlayerActions?.InvokePerform(false);

        onDialogue = true;
        textBox.SetActive(true);
        for (int i = 0; i < boxes.Length; i++)
        {
            text.text = "";
            for (int j = 0; j < boxes[i].Length && text.text != boxes[i]; j++)
            {
                if (resumeDialogue)
                {
                    text.text = boxes[i];
                    PlayVoice(dialogue.voice, 'a');
                    resumeDialogue = false;
                }
                else
                {
                    text.text = text.text + boxes[i][j];
                    PlayVoice(dialogue.voice, boxes[i][j]);
                    yield return new WaitForSeconds(0.05f);
                }
            }
            yield return new WaitUntil(() => resumeDialogue);
            resumeDialogue = false;
        }
        textBox.SetActive(false);
        onDialogue = false;
        enablePlayerActions?.InvokePerform(true);
        dialogue.PlayFinishEvent();
    }

    private IEnumerator StartActionDialogue(string box, ActionDialogueScriptableObject dialogue, VoidEmitter emitter)
    {
        text.text = "";
        onDialogue = true;
        textBox.SetActive(true);
        for (int i = 0; i < box.Length; i++)
        {
            text.text = text.text + box[i];

            PlayVoice(dialogue.voice, box[i]);

            yield return new WaitForSeconds(0.05f);
        }

        emitter.Perform.AddListener(() => performedEvent = true);
        yield return new WaitUntil(()=> performedEvent);


        performedEvent = false;
        textBox.SetActive(false);
        onDialogue = false;
        dialogue.PlayFinishEvent();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < talkingDialogues.Length; i++)
        {
            talkingDialogues[i].dialogueStarted.RemoveAllListeners();
        }
    }

    public void ResumeDialogue()
    {
        if (onDialogue) resumeDialogue = true;
        else resumeDialogue = false;
    }

    private void PlayVoice(AudioPlayer player, char c)
    {
        if (player != null && c != '.' && c != ' ')
        {
            player.Play();
        }
    }
}
