using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Clase que registra todos los Scriptables de tipo Dialogue. Cuando recibe que un dialogo ha comenzado, reproduce la informacion que esta contenida en el Scriptable
/// Reproduce con un sonido cada letra que va apareciendo progresivamente, y detecta cuando se ha pulsado la tecla correspondiente para continuar con el dialogo.
/// 
/// </summary>
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

    /// <summary>
    /// Metodo que comienza un dialogo de habla cuando se le llama
    /// </summary>
    /// <param name="dialogueBoxes"></param>
    /// <param name="dialogue"></param>
    private void TalkingDialogueStarted(string[] dialogueBoxes, DialogueScriptableObject dialogue)
    {
        if (!onDialogue)
        {
            StartCoroutine(StartTalkingDialogue(dialogueBoxes, dialogue));

        }
        else Debug.LogError("Se ha intentado reproducir un dialogo normal mientras el jugador ya se encuentra en uno.");
    }
    /// <summary>
    /// Metodo que comienza un dialogo de accion cuando se le llama
    /// </summary>
    /// <param name="dialogueBox"></param>
    /// <param name="dialogue"></param>
    /// <param name="emitter"></param>
    private void ActionDialogueStarted(string dialogueBox, ActionDialogueScriptableObject dialogue, VoidEmitter emitter)
    {
        if (!onDialogue)
        {
            StartCoroutine(StartActionDialogue(dialogueBox, dialogue, emitter));
        }
        else Debug.LogError("Se ha intentado reproducir un dialogo de accion mientras el jugador ya se encuentra en uno.");
    }

    /// <summary>
    /// Reproduce letra por letra la informacion del dialogo, y espera a que se reciba una bandera para poder borrar el texto entero y comenzar a escribir el siguiente texto
    /// </summary>
    /// <param name="boxes"></param>
    /// <param name="dialogue"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Reproduce letra por letra la informacion del dialogo, y espera a que se reciba una bandera, en forma de evento de unity recibido, para poder borrar el texto y terminar el dialogo.
    /// </summary>
    /// <param name="box"></param>
    /// <param name="dialogue"></param>
    /// <param name="emitter"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Activa la bandera para el dialogo de accion
    /// </summary>
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

    /// <summary>
    /// Setea a true el booleano resumeDialogue
    /// </summary>
    public void ResumeDialogue()
    {
        if (onDialogue) resumeDialogue = true;
        else resumeDialogue = false;
    }

    /// <summary>
    /// Reproduce el sonido de hablar
    /// </summary>
    /// <param name="player"></param>
    /// <param name="c"></param>
    private void PlayVoice(AudioPlayer player, char c)
    {
        if (player != null && c != '.' && c != ' ' && c!= ',')
        {
            player.Play();
        }
    }

    /// <summary>
    /// Activa o desactiva el HUD al entrar o salir de un dialogo respectivamente
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
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
