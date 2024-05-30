using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : MonoBehaviour
{
    public GameObject playerStand;
    public GameObject playerSit;
    public GameObject text;
    public GameObject standText;

    public bool IsSitting;
    public bool IsInteract;
   
    void Update()
    {
        Sit();
    }
    private void Start()
    {
        text.SetActive(false);
        standText.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="MainCamera")
        {
            text.SetActive(true);
            IsInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            text.SetActive(false);
            IsInteract = false;
        }
    }

    private void Sit()
    {
        if(IsInteract==true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                text.SetActive(false);
                standText.SetActive(true);
                playerSit.SetActive(true);
                IsSitting = true;
                playerStand.SetActive(false);
                IsInteract = false;
            }
        }

        if(IsSitting==true)
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                standText.SetActive(true);
                playerSit.SetActive(false);
                playerStand.SetActive(true);
                IsSitting = false;      
            }
        }
    }
}
