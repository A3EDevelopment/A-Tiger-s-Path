using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuThing : MonoBehaviour
{
    public bool started = false;

    public float speed = 1.0f;

    public Transform target;

    public GameObject AH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        started = true;


        if  (started == true)
        {
            Vector3 targetDirection = target.position - transform.position;

            float singleStep = speed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
