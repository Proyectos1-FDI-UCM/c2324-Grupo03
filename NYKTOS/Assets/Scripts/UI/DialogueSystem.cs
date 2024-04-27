using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueSystem : MonoBehaviour
{
    #region controles de dialogo
    [SerializeField] float ReadingTime = 0.075f;

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

    [SerializeField] private BoolEmitter enableDialogueActions;

    [SerializeField] GameObject HUD;

    [SerializeField] private VoidEmitter _resumeDialogueEmitter;
    #endregion
    private void Start()
    {
        _resumeDialogueEmitter.Perform.AddListener(ResumeDialogue);

        textBox.SetActive(false);
        talkingDialogues = Resources.LoadAll<DialogueScriptableObject>("DialogueResources");
        actionDialogues = Resources.LoadAll<ActionDialogueScriptableObject>("DialogueResources");

        //Suscripcion de metodo de TALKING DIALOGUES
        for (int i = 0; i < talkingDialogues.Length; i++)
        {
            talkingDialogues[i].dialogueStarted.AddListener(TalkingDialogueStarted);
        }

        //Suscripcion de metodo de ACTION DIALOGUES
        for (int i = 0;i < actionDialogues.Length; i++)
        {
            actionDialogues[i].dialogueStarted.AddListener(ActionDialogueStarted);
        }
    }

    private void TalkingDialogueStarted(string[] dialogueBoxes, DialogueScriptableObject dialogue)
    {
        if (!onDialogue)
        {
            StartCoroutine(StartTalkingDialogue(dialogueBoxes, dialogue));

        }
        else Debug.LogError("Se ha intentado reproducir un dialogo normal mientras el jugador ya se encuentra en uno.");
    }

    private void ActionDialogueStarted(string dialogueBox, ActionDialogueScriptableObject dialogue, VoidEmitter emitter)
    {
        if (!onDialogue)
        {
            StartCoroutine(StartActionDialogue(dialogueBox, dialogue, emitter));
        }
        else Debug.LogError("Se ha intentado reproducir un dialogo de accion mientras el jugador ya se encuentra en uno.");
    }
    private IEnumerator StartTalkingDialogue(string[] boxes, DialogueScriptableObject dialogue)
    {
        
        StartCoroutine(EnableHUD(false));
        onDialogue = true;
        textBox.SetActive(true);
        dialogue.PlayEnterEvent();

        for (int i = 0; i < boxes.Length; i++)
        {
            text.text = "";
            for (int j = 0; j < boxes[i].Length && text.text != boxes[i]; j++)
            {
                enableDialogueActions?.InvokePerform(true);
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

                    if (boxes[i][j] == '.' || boxes[i][j] == ',')
                    {
                        yield return new WaitForSeconds(3 * ReadingTime);
                    }
                    else yield return new WaitForSeconds(ReadingTime);
                }
            }
            yield return new WaitUntil(() => resumeDialogue);
            resumeDialogue = false;
        }
        textBox.SetActive(false);
        onDialogue = false;
        enableDialogueActions?.InvokePerform(false);
        StartCoroutine(EnableHUD(true)) ;
        dialogue.PlayFinishEvent();
    }

    private IEnumerator StartActionDialogue(string box, ActionDialogueScriptableObject dialogue, VoidEmitter emitter)
    {
        string[] iconsArray = box.Split('<', '>');
        List<string> icons = new List<string>();
        for (int i = 0; i < iconsArray.Length; i++)
        {
            if (i % 2 != 0)
            {
                icons.Add('<'+iconsArray[i] +'>');
            }
        }

        int iconsPosition = 0;

        text.text = "";
        onDialogue = true;
        textBox.SetActive(true);

        dialogue.PlayEnterEvent();

        emitter.Perform.AddListener(PerformedEvent);
        for (int i = 0; i < box.Length && !performedEvent; i++)
        {
            if (box[i] == '<' && icons.Count > 0)
            {
                text.text = text.text + icons[iconsPosition];
                i = i + icons[iconsPosition].Length - 1;
                iconsPosition++;
                
            }
            else text.text = text.text + box[i];

            PlayVoice(dialogue.voice, box[i]);

            if (box[i] == '.' || box[i] == ',')
            {
                yield return new WaitForSeconds(3*ReadingTime);
            }
            else yield return new WaitForSeconds(ReadingTime);

        }

        
        yield return new WaitUntil(()=> performedEvent);

        emitter.Perform.RemoveListener(PerformedEvent);
        performedEvent = false;
        textBox.SetActive(false);
        onDialogue = false;
        dialogue.PlayFinishEvent();
    }

    private void PerformedEvent()
    {
        performedEvent = true;
    }

    private void OnDestroy()
    {
        _resumeDialogueEmitter.Perform.RemoveListener(ResumeDialogue);
        if (talkingDialogues != null)
        {
            for (int i = 0; i < talkingDialogues.Length; i++)
            {
                talkingDialogues[i].dialogueStarted.RemoveListener(TalkingDialogueStarted);
            }
        }
        
        if (actionDialogues != null)
        {
            for (int i = 0; i < actionDialogues.Length; i++)
            {
                actionDialogues[i].dialogueStarted.RemoveListener(ActionDialogueStarted);
            }
        }
        
    }

    public void ResumeDialogue()
    {
        if (onDialogue) resumeDialogue = true;
        else resumeDialogue = false;
    }

    private void PlayVoice(AudioPlayer player, char c)
    {
        if (player != null && c != '.' && c != ' ' && c!= ',')
        {
            player.Play();
        }
    }

    private IEnumerator EnableHUD(bool b)
    {
        if (HUD != null)
        {
            CanvasGroup canvas = HUD.GetComponent<CanvasGroup>();
            for (int i =0; i*0.01f < 1; i = i + 10)
            {
                if (b == true)
                {
                    canvas.alpha = i * 0.01f;
                }
                else
                {
                    canvas.alpha = 1 - (0.01f * i);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
