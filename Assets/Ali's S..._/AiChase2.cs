using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChase2 : MonoBehaviour
{
    public Animator anim;
    public Transform Player;
    public Transform positionSpawn;
    public int MoveSpeed = 4;
    public int MaxDist = 8;
    private object navmeshagent;
    public GameObject GuardTrigger;


    public bool inBox = false;
    public int state = 0;

    public BoxCollider box;
    Vector3 patrolPoint;

    private void Start()
    {
        patrolPoint = PickRandomPoint(box);
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
        if (inBox == true)
        {
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

            if (state == 1)
            {
                MoveSpeed = 5;


                transform.LookAt(Player);
                float distance = Vector3.Distance(transform.position, Player.position);

                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }

            if (state == 2)
            {
                MoveSpeed = 5;

                transform.LookAt(positionSpawn);
                float distance = Vector3.Distance(transform.position, Player.position);

                transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                StartCoroutine(goBack());

                if (distance == 0f)
                {
                    anim.StopPlayback();
                }
            }
        }
        else
        {
            state = 2;
        }


        if (Vector3.Distance(transform.position, Player.position) <= MaxDist && GuardTrigger.activeSelf == true)//not MinDist
        {
            state = 1;
        }
        else
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

    IEnumerator goBack()
    {
        yield return new WaitForSeconds(3f);

        state = 1;
    }
}