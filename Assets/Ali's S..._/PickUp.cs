using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour 
{
    bool playerInRange = false;
    Basic player;
    public GameObject torchSFX;
    public GameObject DuptorchSFX;

    private void Start()
    {
        player = FindObjectOfType<Basic>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GrabTorch(true);
                player.holdingTorch = true;

                Destroy(gameObject);
                DuptorchSFX.SetActive(true);
                torchSFX.SetActive(false);
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
