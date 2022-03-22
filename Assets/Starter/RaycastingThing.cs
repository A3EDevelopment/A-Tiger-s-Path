using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastingThing : MonoBehaviour
{
    private GameObject raycastedObj;

    public GameObject NPC;

    public GameObject Image;

    public bool isHovering;


    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    private void Start()
    {
        Image.SetActive(false);
        isHovering = false;
        
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                if (Input.GetKeyDown("e"))
                {
                    
                }
                Image.SetActive(true);
                isHovering = true;
            }   
        }
        else
        {
            Image.SetActive(false);
            isHovering = false;
        }
    }
}
