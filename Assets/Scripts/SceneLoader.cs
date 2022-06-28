using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Cutscene");
        }

        if (Input.GetKeyDown("joystick button 0"))
        {
            SceneManager.LoadScene("Cutscene");
            
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Cutscene");
    }


}
