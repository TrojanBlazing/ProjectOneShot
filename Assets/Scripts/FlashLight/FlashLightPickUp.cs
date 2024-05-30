using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickUp : MonoBehaviour
{
    public GameObject pickupText;
    public GameObject FlashOn;
    void Start()
    {
        FlashOn.SetActive(false);
        pickupText.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                FlashOn.SetActive(true);
                pickupText.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pickupText.SetActive(false);
    }
}


