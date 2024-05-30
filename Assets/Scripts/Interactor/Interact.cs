using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent OnInteract;
    public int Id;
    public Sprite IconInteract;
    void Start()
    {
        Id = Random.Range(0, 999999);
    }  
    void Update()
    {
        
    }
}
