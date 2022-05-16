using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueColliderTrigger : MonoBehaviour
{
    
    public GameObject Player;

    public GameObject NPCCAM;

    private bool playerInRange;

    public Transform PlayerTransform;

    public Transform TeleportGoal;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    void Start()
    {
        PlayerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "Dialogue" && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                
                NPCCAM.SetActive(true);
                
                Basic moveScript; 
                
                moveScript = Player.GetComponent<Basic>();

                moveScript.enabled = false;

                Animator Anim;

                Anim = Player.GetComponent<Animator>();

                Anim.enabled = false;

                PlayerTransform.position = TeleportGoal.position;

                
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
