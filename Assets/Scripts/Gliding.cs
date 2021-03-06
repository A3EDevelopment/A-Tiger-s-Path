using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    public LayerMask whatIsGround;
    public GameObject groundChecker;
    public GameObject Player;

    public Transform myEndPoint;
    public Transform playerTransform;

    public bool isOnGround;
    public float glidemass;
    public float speed;

    Rigidbody myRigidbody;
    //public Animator Anim;

    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.2f, whatIsGround);

        //Anim.applyRootMotion = true;

        if (/*isOnGround == false && */Input.GetKey(KeyCode.F))
        {
            //Player.transform.Translate(Vector3.up * glidemass, Space.World);
            //Player.transform.position +=  new Vector3(0.0f, glidemass, 0.0f);
            //playerTransform.transform.position = Vector3.MoveTowards(playerTransform.position, myEndPoint.position, speed);
            transform.Translate(Vector3.up * 0.1f);
            Debug.Log("yo");
            //Anim.applyRootMotion = false;
            //playerTransform.position = myEndPoint.position;
            //Debug.Log ("yo");
            //myRigidbody.AddForce(new Vector3(0.0f, glidemass, 0.0f));
        }
    }
}
