using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using TMPro;
using UnityEngine;


public class FoodScript : MonoBehaviour
{
    [SerializeField] Transform pcamera;
    private float distance = 3f;
    public TextMeshProUGUI text;
    [SerializeField]
    private GameObject food;

    public AudioSource am;

    
    void Start()
    {
        text.enabled = false;
        food.SetActive(true);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pressing();
        } 
    }
    private void Pressing()
    {
        RaycastHit hit;
        if(Physics.Raycast(pcamera.transform.position,pcamera.transform.forward,out hit,distance))
        {
            if(hit.transform.tag=="Food")
            {

                am.Play();
                food.gameObject.SetActive(false);
              
                
            }
        }

    }
    private void TextVisible()
    {
        RaycastHit hit;
        if (Physics.Raycast(pcamera.transform.position, pcamera.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Food")
            {
                text.enabled=true;
                return;
            }
        }
        text.enabled = false;
    }
    private void FixedUpdate()
    {
        TextVisible();
    }
}
