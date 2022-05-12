using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour
{
    void Start()
    {
        PlayFade();
    }

    // Update is called once per frame
    void PlayFade()
    {
        GameObject animator = GameObject.Find("BlackFade");
        Animator animatorr = animator.GetComponent<Animator>();
        animatorr.SetTrigger("FadeOut");
    }
}
