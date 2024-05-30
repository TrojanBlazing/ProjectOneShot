using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashBar : MonoBehaviour
{
    public Slider s;


    public void SetFlashFUll(int ful)
    {
        s.maxValue = ful;
        s.value = ful;
    }
    public void SetFlash(int ful)
    {
        s.value = ful;
    }
}
