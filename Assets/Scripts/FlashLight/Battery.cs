using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Battery : MonoBehaviour
{
    [SerializeField]
    int BatteryWeight;
    [SerializeField]
    GameObject[] HoverObject;

    [SerializeField]
    KeyCode CK = KeyCode.E;

    public void OnMouseOver()
    {
        foreach (GameObject obj in HoverObject)
        {
            obj.SetActive(true);
        }
        if (Input.GetKeyDown(CK))
        {
            FindObjectOfType<FlashLight>().GainBattery(BatteryWeight);
            Destroy(this.gameObject);
        }
    }
    public void OnMouseExit()
    {
        foreach (GameObject obj in HoverObject)
        {
            obj.SetActive(false);
        }
    }
}
