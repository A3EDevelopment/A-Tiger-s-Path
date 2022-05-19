using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase2 : MonoBehaviour
{
    public int AttackTrigger2;
    public Transform Player;
    /*  public Transform enemyTransform;
       public float speed = 3f;
    */
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;
    private object navmeshagent;

    bool inBox = false;
    int state = 0;

    public BoxCollider box;
    Vector3 patrolPoint;

    private void Start()
    {
        patrolPoint = PickRandomPoint(box);
    }

    void Update()
    {
        //Debug.Log(state);
        if (state == 0)
        {
            if (Vector3.Distance(transform.position, patrolPoint) > 0.3f)//not MinDist
            {
                transform.LookAt(patrolPoint);
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }
            else
            {
                patrolPoint = PickRandomPoint(box);
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
        }
        else if (state == 2)
        {
            //Look at target
            transform.LookAt(Player);

            //Get distance between player and enemy
            float distance = Vector3.Distance(transform.position, Player.position);

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;


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
