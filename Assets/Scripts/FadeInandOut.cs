using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInandOut : MonoBehaviour
{
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    public void FadeOut()
    {
        GameObject animator = GameObject.Find("Fade");
        Animator animatorr = animator.GetComponent<Animator>();
        animatorr.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        GameObject animator = GameObject.Find("Fade");
        Animator animatorr = animator.GetComponent<Animator>();
        animatorr.SetTrigger("FadeIn");
    }
}
