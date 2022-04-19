using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] views;
    public GameObject[] buttons;
    public float transitionSpeed;
    Transform currentView;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentView = views[0];
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
            buttons[2].SetActive(false);
            buttons[3].SetActive(false);
            buttons[4].SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentView = views[1];
            buttons[0].SetActive(false);
            buttons[1].SetActive(true);
            buttons[2].SetActive(false);
            buttons[3].SetActive(false);
            buttons[4].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentView = views[2];
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            buttons[2].SetActive(true);
            buttons[3].SetActive(false);
            buttons[4].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentView = views[3];
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            buttons[2].SetActive(false);
            buttons[3].SetActive(true);
            buttons[4].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentView = views[4];
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            buttons[2].SetActive(false);
            buttons[3].SetActive(false);
            buttons[4].SetActive(true);
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                                           Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                                           Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

        transform.eulerAngles = currentAngle;
    }

}
