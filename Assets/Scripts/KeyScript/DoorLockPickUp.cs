using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockPickUp : MonoBehaviour
{
    public Animator boxOB;
    public GameObject keyOBNeeded;
    public GameObject openText;
    public GameObject keyMissingText;
    //public AudioSource openSound;
    private float maxInteractionDistance = 3f;

    bool isOpen;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        openText.SetActive(false);
        keyMissingText.SetActive(false);    
    }

 

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxInteractionDistance)) 
        {
            if (hit.collider.gameObject == gameObject && keyOBNeeded.activeInHierarchy)
            {
                openText.SetActive(true);
                keyMissingText.SetActive(false);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    
                    if (Vector3.Distance(transform.position, hit.point) <= maxInteractionDistance)
                    {
                        keyOBNeeded.SetActive(false);
                        //openSound.Play();
                        boxOB.SetBool("Open", true);
                        isOpen = true;
                        keyMissingText.SetActive(false);
                        Debug.Log(keyMissingText);
                    }
                }
            }
            else
            {
                openText.SetActive(false);

                if (hit.collider.gameObject == gameObject && !keyOBNeeded.activeInHierarchy)
                {
                    keyMissingText.SetActive(true);
                }
                else
                {
                    keyMissingText.SetActive(false);
                }
            }
        }
        else
        {
            openText.SetActive(false);
            keyMissingText.SetActive(false);
        }

        if (isOpen)
        {
            keyMissingText.SetActive(false);
        }
    }

}



