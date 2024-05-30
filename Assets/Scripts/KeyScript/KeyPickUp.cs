using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
 
    public GameObject keyOB;
    public GameObject invOB;
    public GameObject pickUpText;
   // public AudioSource keySound;

    public float reachDistance = 3f;

    void Start()
    {
        pickUpText.SetActive(false);
        invOB.SetActive(false);
    }

    void Update()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                pickUpText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp();
                }
            }
            else
            {
                pickUpText.SetActive(false);
            }
        }
        else
        {
            pickUpText.SetActive(false);
        }
    }

    void PickUp()
    {
        keyOB.SetActive(false);
       // keySound.Play();
        invOB.SetActive(true);
        pickUpText.SetActive(false);
    }
}

