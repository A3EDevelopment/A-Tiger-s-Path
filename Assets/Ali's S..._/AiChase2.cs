using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase2 : MonoBehaviour  
{
    public Animator anim; 
    public int AttackTrigger2;
    public Transform Player;
 /*  public Transform enemyTransform;
    public float speed = 3f;
 */   public int MoveSpeed = 4;
    public int MaxDist = 5;
    public int MinDist = 2;

    public float distance;
    private object navmeshagent;
   // public bool Seen; 
    

    bool inBox = false;
    int state = 0;

    public BoxCollider box;
    Vector3 patrolPoint;

    private void Start()
    {
        patrolPoint = PickRandomPoint(box);

        if (inBox == false)
        {
            MinDist = 0;
            MaxDist = 0;
        }
        else if (inBox == true)
        {
            MinDist = 5;
            MaxDist = 9;
        }

    }

    void Update()
    {   

        anim.SetFloat("speed", MoveSpeed);
        {
            if (MoveSpeed > 6)
            {
                anim.SetBool("Running", true);
            }
        }

        //Debug.Log(state);
        if (state == 0)
        {
            anim.Play("Jog Forward");
            if (Vector3.Distance(transform.position, patrolPoint) > 0.3f)//not MinDist
            {
                transform.LookAt(patrolPoint);
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                Debug.Log("S0");
           
            //   Seen = false;
            }
            else
            {
                patrolPoint = PickRandomPoint(box);
           //     Seen = false;
            }
        }
        else if (state == 1)
        {
            /*   Player = GameObject.FindWithTag("Player").transform;
               Vector3 targetHeading = Player.position - transform.position;
               Vector3 targetDirection = targetHeading.normalized;
               transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
               transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

               //move towards the player
               enemyTransform.position += enemyTransform.forward * speed * Time.deltaTime;

               transform.LookAt(Player);
            */
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            Debug.Log("S1");
            //   Seen = true;
        }
        else if (state == 2)
        {
            MoveSpeed = 7;

            //Look at target
            transform.LookAt(Player);

            //Get distance between player and enemy
            distance = Vector3.Distance(transform.position, Player.position);

           // transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            Debug.Log("S0");
             
           
        }

        if (Vector3.Distance(transform.position, Player.position) <= MaxDist)//not MinDist
        {
            state = 1;
        }

        if (Vector3.Distance(transform.position, Player.position) <= MinDist)//not MaxDist
        {
            state = 2;

        }

        if (inBox == false)
        {
            state = 0;
            //MinDist = 0;
            //MaxDist = 0;
        }
        else
        {
            //MinDist = 9;
            //MaxDist = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BoundingBox")
        {
            inBox = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BoundingBox")
        {
            inBox = false;
        }
    }

    private Vector3 PickRandomPoint(BoxCollider box)
    {
        return new Vector3(Random.Range(box.bounds.min.x, box.bounds.max.x), box.transform.position.y, Random.Range(box.bounds.min.z, box.bounds.max.z));
    }
}
