using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombinationLock : MonoBehaviour
{
    private int[] result, correctComb;
    private bool isOpened;

    private void Start()
    {
        result = new int[] { 0, 0, 0, 0 };
        correctComb = new int[] { 1, 3, 4, 5 };
        isOpened = false;
        PadRotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelname, int number)
    {
        Debug.Log($"Wheel: {wheelname}, Number: {number}");
        switch (wheelname)
        {
            case "WheelOne":
                result[0] = number;
                break;

            case "WheelTwo":
                result[1] = number;
                break;

            case "WheelThree":
                result[2] = number;
                break;

            case "WheelFour":
                result[3] = number;
                break;
        }

        Debug.Log("Current Combination: " + result[0] + result[1] + result[2] + result[3]); 

      
        if (result[0] == correctComb[0] &&
            result[1] == correctComb[1] &&
            result[2] == correctComb[2] &&
            result[3] == correctComb[3] &&
            !isOpened)
        {
            Debug.Log("Correct Combination Detected!"); 
            Debug.Log("Opened!"); 
           
            isOpened = true;
        }
    }

    private void OnDestroy()
    {
        PadRotate.Rotated -= CheckResults;
    }
}
