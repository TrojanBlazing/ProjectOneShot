using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum FlashLightState
{
    off, on, dead
}
public class FlashLight : MonoBehaviour
{

    [Range(0.0f, 2f)][SerializeField] float BatteryLoss = 0.5f;

    [SerializeField]
    int startBattery = 100;
    public int currentBattery;
    public FlashLightState state;
    private bool FlashLightIsON;
    KeyCode TK = KeyCode.F;

    public Slider slider;

    public GameObject FlashLightPrefab;


    private void Start()
    {
        
        currentBattery = startBattery;
        slider.maxValue = startBattery;
        slider.value = currentBattery;
        InvokeRepeating(nameof(LoseBattery), 0, BatteryLoss);

    }

    private void Update()
    {


        if (Input.GetKeyDown(TK))
        {
            ToggleFlash();
        }
        if (state == FlashLightState.off)
        {
            FlashLightPrefab.SetActive(false);
        }
        else if (state == FlashLightState.on)
        {
            FlashLightPrefab.SetActive(true);
        }
        else if (state == FlashLightState.dead)
        {
            FlashLightPrefab.SetActive(false);
        }


        if (currentBattery <= 0)
        {
            currentBattery = 0;
            state = FlashLightState.dead;
            FlashLightIsON = false;
        }
      
    }

    private void LoseBattery()
    {
        if (state == FlashLightState.on)
        {
            currentBattery--;
            slider.value = currentBattery;

        }
    }
    public void GainBattery(int amount)
    {

        if (currentBattery == 0)
        {
            state = FlashLightState.on;
            FlashLightIsON = true;
        }
        if (currentBattery + amount > startBattery)
        {
            currentBattery = startBattery;
        }
        else
        {
            currentBattery += amount;
        }
    }
    private void ToggleFlash()
    {
        FlashLightIsON = !FlashLightIsON;
        if (state == FlashLightState.dead)
        {
            FlashLightIsON = false;

        }
        if (FlashLightIsON)
        {
            state = FlashLightState.on;
        }
        else
        {
            state = FlashLightState.off;
        }
    }
}
