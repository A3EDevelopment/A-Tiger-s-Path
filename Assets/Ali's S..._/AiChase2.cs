using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase2 : MonoBehaviour  
{
    public int AttackTrigger2;
    public Transform Player;
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;
    private object navmeshagent;

    bool shouldIFollow = false;

    public BoxCollider box;
    Vector3 patrolPoint;

    void Update()
    {
        if (shouldIFollow == true)
        {
            transform.LookAt(Player);

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)//not MinDist
            {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, Player.position) <= MinDist)//not MaxDist
                {
                    //Here Call any function you want, like Shoot or something
                }

            }
        } 
        else
        {
            if (Vector3.Distance(transform.position, patrolPoint) > 0.3f)//not MinDist
            {
                transform.LookAt(patrolPoint);
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            } else
            {
                patrolPoint = PickRandomPoint(box.bounds);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BoundingBox")
        {
            shouldIFollow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BoundingBox")
        {
            shouldIFollow = false;
        }
    }

    private Vector3 PickRandomPoint(Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0f, Random.Range(bounds.min.z, bounds.max.z));
    }
}
