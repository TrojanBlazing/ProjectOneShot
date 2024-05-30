using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  [SerializeField] private Transform pcamera;
    [SerializeField] private float maxDistance = 3;
    public GameObject text;

    private Animator anim;
    private bool open = false;

    private void Start()
    {
        text.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PressCheck();
        }
    }

    private void PressCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(pcamera.transform.position, pcamera.transform.forward, out hit, maxDistance))
        {
            if (hit.transform.tag == "Door")
            {
                anim = hit.transform.GetComponentInParent<Animator>();
                open = !open;
                anim.SetBool("Open", !open);
            }
        }
    }

    private void UpdateTextVisibility()
    {
        RaycastHit hit;
        if (Physics.Raycast(pcamera.transform.position, pcamera.transform.forward, out hit, maxDistance))
        {
            if (hit.transform.tag == "Door")
            {
                text.SetActive(true);
                return;
            }
        }
        text.SetActive(false);
    }

    private void FixedUpdate()
    {
        UpdateTextVisibility();
    }
}




