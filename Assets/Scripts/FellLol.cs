using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellLol : MonoBehaviour
{
    public GameObject PlayerCam1;

    public GameObject Fader;

    public GameObject Player;

    public Transform PlayerTransform;

    public Transform TeleportGoal;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCam1.SetActive(false);

        PlayerTransform = Player.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCam1.SetActive(true);


            StartCoroutine(wait());

            

            


            

            //PlayerCam1.SetActive(false);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);

        FadeInandOut Script;

        Script = Fader.GetComponent<FadeInandOut>();

        Script.FadeOut();

        Basic moveScript;

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = false;

        PlayerTransform.position = TeleportGoal.position;

        moveScript = Player.GetComponent<Basic>();

        moveScript.enabled = true;

        Script.FadeIn();

        yield return new WaitForSeconds(0.5f);

        PlayerCam1.SetActive(false);
    }
}
