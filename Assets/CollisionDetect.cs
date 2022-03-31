using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Box")
        {
            Debug.Log("Chris got 'Rocked' hehe");
            Destroy(col.gameObject);
        }
    }
}
