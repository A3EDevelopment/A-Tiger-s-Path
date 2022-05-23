using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    bool playerInRange = false;
    Basic player;

    public GameObject lanternFire;
    public GameObject lanternFireSfx;
    public GameObject torchFire;
    public GameObject torchSFX;

    public GameObject Guard;

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

                    lanternFire.SetActive(true);
                    lanternFireSfx.SetActive(true);
                    torchFire.SetActive(false);
                    torchSFX.SetActive(false);

                    Guard.SetActive(false);
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
