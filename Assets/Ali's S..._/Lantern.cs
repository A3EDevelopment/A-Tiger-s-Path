using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    bool playerInRange = false;
    Basic player;

    public GameObject fire;
    public GameObject fire2;

    private void Start()
    {
        player = FindObjectOfType<Basic>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (player.holdingTorch == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.GrabTorch(false);
                    player.holdingTorch = false;

                    fire.SetActive(true);
                    fire2.SetActive(false);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}