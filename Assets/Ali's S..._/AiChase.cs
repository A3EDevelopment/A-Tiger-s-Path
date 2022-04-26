using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase : MonoBehaviour
{

    Transform target;
    Transform enemyTransform;
    public float speed = 3f;
    public float rotationSpeed = 3f;
    public float detectionRange = 10;


    void Start()
    {
        //obtain the game object Transform
        enemyTransform = this.GetComponent<Transform>();
    }

    void Update()
    {

        target = GameObject.FindWithTag("Player").transform;
        Vector3 targetHeading = target.position - transform.position;
        Vector3 targetDirection = targetHeading.normalized;

        //rotate to look at the player

        transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //move towards the player
        enemyTransform.position += enemyTransform.forward * speed * Time.deltaTime;

        
    
    
    
    }

}
