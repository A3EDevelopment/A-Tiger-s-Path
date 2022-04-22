using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    public NPC npc;

    bool isTalking = false;
    bool isConversationFinished = false;

    float distance;
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;

    public GameObject Prompt;


    void Start()
    {
        dialogueUI.SetActive(false);
        Prompt.SetActive(false);
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            curResponseTracker++;
            if (curResponseTracker >= npc.playerDialogue.Length -1)
            {
                curResponseTracker = npc.playerDialogue.Length - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            curResponseTracker--;
            if (curResponseTracker < 0 )
            {
                curResponseTracker = 0;
            }
            
        }

        if(curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
        {
            playerResponse.text = npc.playerDialogue[0];

            if (Input.GetKeyDown(KeyCode.E) && isConversationFinished == false)
            {
                npcDialogueBox.text = npc.dialogue[1];
            }

        }
        else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
        {
            playerResponse.text = npc.playerDialogue[1];

            if(Input.GetKeyDown(KeyCode.E) && isConversationFinished == false)
            {
                npcDialogueBox.text = npc.dialogue[2];
            }
        }
        else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
        {
            playerResponse.text = npc.playerDialogue[2];

            if(Input.GetKeyDown(KeyCode.E) && isConversationFinished == false)
            {
                npcDialogueBox.text = npc.dialogue[3];
                isConversationFinished = true;
            }
        }


        if (distance <= 4)
        {
            Prompt.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E) && isTalking == false)
            {
                StartConversation();
                
            }
            else if (Input.GetKeyDown(KeyCode.E) && isTalking == true && isConversationFinished == true)
            {
                EndDialogue();
            }
        }

        if (isTalking == true)
        {
            Prompt.SetActive(false);
            isConversationFinished = false;
        }
        else{
            Prompt.SetActive(true);
            isConversationFinished = true;
        }
    }

    void StartConversation()
    {
        Time.timeScale = 0;
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        Prompt.SetActive(false);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
        Time.timeScale = 1;
    }

}
