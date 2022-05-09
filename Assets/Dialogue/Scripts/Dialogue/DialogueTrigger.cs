using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    

    public GameObject Player;

    public GameObject NPCCAM;

    private void Start()
    {
        
    }

    private void Awake() 
    {
        
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);

                Basic moveScript; 
                
                moveScript = Player.GetComponent<Basic>();

                moveScript.enabled = false;

                Animator Anim;

                Anim = Player.GetComponent<Animator>();

                Anim.enabled = false;

                NPCCAM.SetActive(true);
            }

            
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "Dialogue")
        {
            playerInRange = true;
            


            Debug.Log("yoo");
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        if (collider.gameObject.tag == "Dialogue")
        {
            playerInRange = false;
        }
    }
}
