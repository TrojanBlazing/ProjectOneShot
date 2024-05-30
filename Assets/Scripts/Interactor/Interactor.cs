using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Sprite defaultIcon;
    public Sprite defaultInteractIcon;

    private Interact interactable;
    public LayerMask interact;
    public Image imgInter;

    //public Vector2 defaultIconSize;
    //public Vector2 defaultInteractIconSize

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit,2,interact))
        {
            if(hit.collider.GetComponent<Interact>()!=false)
            {
               if(interactable==null||interactable.Id!=hit.collider.GetComponent<Interact>().Id)
                {
                    interactable = hit.collider.GetComponent<Interact>();
                   
                }
               if(interactable.IconInteract!=null)
                {
                    imgInter.sprite=interactable.IconInteract;
                }
               else
                {
                    imgInter.sprite = defaultInteractIcon;
                }
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.OnInteract.Invoke();
                }
            }
        }
        else
        {
            if(imgInter.sprite!=defaultIcon)
            {
                imgInter.sprite=defaultIcon;
               
            }
        }
    }
}
