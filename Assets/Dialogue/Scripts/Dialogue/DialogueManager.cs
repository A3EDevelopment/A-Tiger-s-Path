using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;
    

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Animator layoutAnimator;



    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    public GameObject Fader;

    public GameObject NPCCAM1;
    public GameObject NPCCAM2;
    public GameObject NPCCAM3;
    public GameObject NPCCAM4;
    public GameObject NPCCAM5;
    public GameObject NPCCAM6;
    public GameObject NPCCAM7;
    public GameObject NPCCAM8;

    public Transform PlayerTransform;

    public Transform TeleportGoal;

    public GameObject NPC3;
    //public GameObject TRIGGER;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    public GameObject Player;

    public AudioSource Walking;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        PlayerTransform = Player.transform;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get the layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;

        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine
            && currentStory.currentChoices.Count == 0
            && (Input.GetKeyDown(KeyCode.E)))
        {
            ContinueStory();
        }


    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Walking.mute = true;

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // reset portrait, layout, and speaker
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        ContinueStory();

       


    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";


        Basic moveScript;

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = true;

        Animator Anim;


        Anim = Player.GetComponent<Animator>();


        Anim.enabled = true;

        Walking.mute = false;

        //TRIGGER script;

        //Cam = script.GetComponent<DialogueTrigger>();
        //ialogueTrigger NPCCAM;

        //NPCCAM = TRIGGER.GetComponent<NPCCAM>();
        //NPCCAM.enabled = false;

        NPCCAM1.SetActive(false);
        NPCCAM2.SetActive(false);

        if (NPCCAM3.activeSelf)
        {
            //PlayerTransform.position = TeleportGoal.position;
            NPCCAM3.SetActive(false);

            


            //PlayerTransform.position = TeleportGoal.position;


            StartCoroutine(justwait());


            
        }

        if (NPCCAM8.activeSelf)
        {
            //PlayerTransform.position = TeleportGoal.position;
            NPCCAM8.SetActive(false);

            StartCoroutine(wait());

            /*FadeInandOut Script;
            Script = Fader.GetComponent<FadeInandOut>();
            Script.FadeOut();


            moveScript = Player.GetComponent<Basic>();

            moveScript.enabled = false;

            PlayerTransform.position = TeleportGoal.position;





            //NPC3.SetActive(true);

            moveScript = Player.GetComponent<Basic>();

            moveScript.enabled = true;


            Script.FadeIn();*/
        }

        NPCCAM4.SetActive(false);
        NPCCAM5.SetActive(false);
        NPCCAM6.SetActive(false);
        NPCCAM7.SetActive(false);

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);

        FadeInandOut Script;

        Script = Fader.GetComponent<FadeInandOut>();

        

        Script.FadeOut();

        yield return new WaitForSeconds(1f);

        PlayerTransform.position = TeleportGoal.position;



        Basic moveScript;

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = false;

        PlayerTransform.position = TeleportGoal.position;

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = true;

       
    }
    IEnumerator justwait()
    {
        

        FadeInandOut Script;
        Script = Fader.GetComponent<FadeInandOut>();
        Script.FadeOut();

        Basic moveScript;

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = false;

        Animator anims;

        anims = Player.GetComponent<Animator>();

        anims.enabled = false;

        yield return new WaitForSeconds(2.5f);

        NPC3.SetActive(true);

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = true;



        anims = Player.GetComponent<Animator>();

        anims.enabled = true;


        Script.FadeIn();

    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty the dialogue text
        dialogueText.text = "";
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.text = line;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            // this is specific to my InputManager script
            ContinueStory();
        }
    }

}