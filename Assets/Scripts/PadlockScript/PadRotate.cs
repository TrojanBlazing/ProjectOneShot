using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadRotate : MonoBehaviour
{
    public static event Action<string,int>Rotated=delegate { };
    private bool corroutineAllow;
    private int NumberShow;

   
    private void Start()
    {
        corroutineAllow = true;
        NumberShow = 5;
    }

  
    private void OnMouseDown()
    {
        if (corroutineAllow)
        {

            StartCoroutine("RotateWheel");
        }
    }
    private IEnumerator RotateWheel()
    {
        Debug.Log("Rotation started");
        corroutineAllow = false;
        for(int i=0;i<=11;i++)
        {
            transform.Rotate(0f, -3f, 0f);
            yield return new WaitForSeconds(0.02f);
        }
        corroutineAllow = true;
        NumberShow += 1;
        if(NumberShow>9)
        {
            NumberShow = 0;
        }
        Rotated(name, NumberShow);
    }
}
