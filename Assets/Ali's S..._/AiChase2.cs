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

    void Update()
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
}