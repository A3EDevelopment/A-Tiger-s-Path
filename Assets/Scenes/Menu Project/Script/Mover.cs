using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Mover : MonoBehaviour
{
    public List<Vector3> Positions;

    public List<GameObject> VoiceOver;
    public List<GameObject> buttons;

    /*public RawImage img;*/
    public List<RawImage> img;
    public List<GameObject> texts;

    public GameObject cube;
    public GameObject camera;
    public GameObject random;

    public Rigidbody cuberb;

    public int current = 0;
    public int ButtonCurrent = 0;

    public AudioMixerSnapshot[] fadeOut;
    public AudioMixerSnapshot[] fadeIn;

    public bool transitionRight = false;
    public bool transitionLeft = false;

    void Start()
    {
        current++;
        cuberb.MovePosition(Positions[current]);

        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
        buttons[2].SetActive(false);

        texts[0].SetActive(false);
    }

    void Update()
    {

        if (camera.GetComponent<MenuThing>().started == true)
        {
            //StartCoroutine(FadeImage(false));
        }

        if (Input.GetKeyDown(KeyCode.A) && current != 0 || Input.GetKeyDown(KeyCode.LeftArrow) && current != 0 )
        {
            //Vector3 pos = Vector3.MoveTowards(cube.transform.position, Positions[Positions.Count - 1], 200 * Time.fixedDeltaTime);
            current--;
            cuberb.MovePosition(Positions[current]);
            Debug.Log(Positions.Count);

            transitionLeft = true;

            Debug.Log(Positions.Count);
        }
        else
        {
            transitionLeft = false;
        }

    

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            /*Vector3 pos = Vector3.MoveTowards(cube.transform.position, Positions[Positions.Count + 1], 200 * Time.fixedDeltaTime);
            cuberb.MovePosition(pos);*/
            current++;
            cuberb.MovePosition(Positions[current]);

            transitionRight = true;

            Debug.Log(Positions.Count);

            //StartCoroutine(FadeImage(false));
        }
        else
        {
            transitionRight = false;
        }

        if (current == 6)
        {
            SceneManager.LoadScene("Village");
        }

        if (current == 2 && random.activeSelf == true)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (current == 0)
        {
            VoiceOver[0].SetActive(true);
            VoiceOver[1].SetActive(false);
            VoiceOver[2].SetActive(false);
            VoiceOver[3].SetActive(false);
            VoiceOver[4].SetActive(false);
            VoiceOver[5].SetActive(false);
        }

        if (current == 1)
        {
            VoiceOver[0].SetActive(false);
            VoiceOver[1].SetActive(true);
            VoiceOver[2].SetActive(false);
            VoiceOver[3].SetActive(false);
            VoiceOver[4].SetActive(false);
            VoiceOver[5].SetActive(false);
        }

        if (current == 2)
        {
            VoiceOver[0].SetActive(false);
            VoiceOver[1].SetActive(false);
            VoiceOver[2].SetActive(true);
            VoiceOver[3].SetActive(false);
            VoiceOver[4].SetActive(false);
            VoiceOver[5].SetActive(false);
        }

        if (current == 3)
        {
            VoiceOver[0].SetActive(false);
            VoiceOver[1].SetActive(false);
            VoiceOver[2].SetActive(false);
            VoiceOver[3].SetActive(true);
            VoiceOver[4].SetActive(false);
            VoiceOver[5].SetActive(false);
        }

        if (current == 4)
        {
            VoiceOver[0].SetActive(false);
            VoiceOver[1].SetActive(false);
            VoiceOver[2].SetActive(false);
            VoiceOver[3].SetActive(false);
            VoiceOver[4].SetActive(true);
            VoiceOver[5].SetActive(false);
        }

        if (current == 5)
        {
            VoiceOver[0].SetActive(false);
            VoiceOver[1].SetActive(false);
            VoiceOver[2].SetActive(false);
            VoiceOver[3].SetActive(false);
            VoiceOver[4].SetActive(false);
            VoiceOver[5].SetActive(true);
        }






        /*if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ButtonCurrent++;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ButtonCurrent--;
        }

        if (ButtonCurrent == -1)
        {
            ButtonCurrent =  0;
        }
        
        if (ButtonCurrent == 3)
        {
            ButtonCurrent = 2;
        }

        if (ButtonCurrent == 0)
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
            buttons[2].SetActive(false);
        }
        else
        {
            buttons[0].SetActive(false);
        }

        if (ButtonCurrent == 1)
        {
            buttons[1].SetActive(true);
        }
        else
        {
            buttons[1].SetActive(false);
        }

        if (ButtonCurrent == 2)
        {
            buttons[2].SetActive(true);
        }
        else
        {
            buttons[2].SetActive(false);
        }

        /*if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 pos = Vector3.MoveTowards(cube.transform.position, Positions[Positions.Count + 1], 200 * Time.fixedDeltaTime);
            cuberb.MovePosition(pos);
            current++;
            cuberb.MovePosition(Positions[current]);
            Debug.Log(Positions.Count);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Vector3 pos = Vector3.MoveTowards(cube.transform.position, Positions[Positions.Count - 1], 200 * Time.fixedDeltaTime);
            current--;
            cuberb.MovePosition(Positions[current]);
            Debug.Log(Positions.Count);
        }*/

        if (current == -1)
        {
            current = 7;
            cuberb.MovePosition(Positions[7]);
        }

        if (current == 8)
        {
            current = 0;
            cuberb.MovePosition(Positions[0]);
        }

        /*if (transitionRight == true)
        {
            

            if (current == 1)
            {
                fadeOut[current + 1].TransitionTo(1);
                fadeOut[current].TransitionTo(1);
            }

            if (current == 2)
            {
                fadeOut[current + 2].TransitionTo(1);
                fadeOut[3].TransitionTo(1);
            }

            if (current == 3)
            {
                fadeOut[current + 2].TransitionTo(1);
                fadeOut[6].TransitionTo(1);
            }
        }

        if (transitionLeft == true)
        {
            if (current == 7)
            {
                //fadeOut[current + 3].TransitionTo(1);
                fadeOut[1].TransitionTo(1);
            }    

            if (current == 0)
            {
                fadeOut[current + 3].TransitionTo(1);
                fadeOut[current].TransitionTo(1);
            }

            if (current == 1)
            {
                fadeOut[current + 1].TransitionTo(1);
                fadeOut[5].TransitionTo(1);
            }
        }*/
    }


    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1f; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                /*img.color = new Color(1, 1, 1, i);
                yield return null;*/

                float e = 0f;

                for(int a = 0; a < img.Count; a++) 
                {

                    img[a].color = new Color(1, 1, 1, e);
                    yield return null;
                }
            }
        }
        // fade from transparent to opaque
        else
        {

            
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {   
                float e = 0f;

                for(int a = 0; a < img.Count; a++) 
                {

                    img[a].color = new Color(1, 1, 1, e);
                    yield return null;
                }

                for(int f = 0; f < texts.Count; f++) 
                {

                    img[f].color = new Color(1, 1, 1, e);
                    yield return null;
                }
            }
        }
    }

    /*public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }*/
}