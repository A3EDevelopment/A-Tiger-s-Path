using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    public LayerMask whatIsGround;
    public GameObject groundChecker;
    public bool isOnGround;
    public GameObject Player;
    public float glidemass;
    Rigidbody myRigidbody;

    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, whatIsGround);

        if (isOnGround == false && Input.GetKey(KeyCode.F))
        {
            //Player.transform.Translate(Vector3.up * glidemass, Space.World);
            Player.transform.position +=  new Vector3(0.0f, glidemass, 0.0f);
            //myRigidbody.AddForce(new Vector3(0.0f, glidemass, 0.0f));
        }
    }
}
