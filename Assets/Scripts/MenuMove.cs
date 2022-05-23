using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMove : MonoBehaviour
{
    public Transform PointOne;
    public Transform PointTwo;
    public Transform Camera;
    public float speed;
    public float speed1;

    public GameObject Fader;

    public int Count;

    public GameObject CAM1;
    public GameObject CAM2;
    public GameObject CAM3;
    public GameObject CAM4;
    public GameObject CAM5;
    public GameObject CAM6;

    public Transform[] Points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Count == 6)
        {
            StartCoroutine(plswork());
        }
        else
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, Points[Count].position, Time.deltaTime * speed);
        }
        

        float singleStep = speed1 * Time.deltaTime;

        //Vector3 newDirection = Vector3.RotateTowards(Camera.transform.forward, PointTwo.position, singleStep, 0.0f);
        
        if (Count == 1)
        {
            CAM1.SetActive(true);
        }

        if (Count == 2)
        {
            CAM2.SetActive(true);
        }

        if (Count == 3)
        {
            CAM3.SetActive(true);
        }

        if (Count == 4)
        {
            CAM4.SetActive(true);
        }

        if (Count == 5)
        {
            CAM5.SetActive(true);
        }

        if (Count == 6)
        {
            CAM6.SetActive(true);
        }

        


    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MMCAM")
        {

            Count++;

        }
    }

    IEnumerator plswork()
    {
        FadeInandOut Script;

        Script = Fader.GetComponent<FadeInandOut>();

        Script.FadeOut();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
