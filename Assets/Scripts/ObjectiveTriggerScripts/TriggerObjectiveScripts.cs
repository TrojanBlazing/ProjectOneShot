using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerObjectiveScripts : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI notiTextUI;
    [SerializeField]
    private Image CharIconUi;

    [SerializeField]
    private Sprite urIcon;

    [SerializeField]
    [TextArea] private string NotificationMessage;


    public bool removeAfterExit = false;
    public bool disableAfterTimer = false;
    [SerializeField]
    private float DisableTimer = 2.0f;


    [SerializeField]
    private Animator notianim;

    private BoxCollider objectcollider;

    private void Awake()
    {
        objectcollider = gameObject.GetComponent<BoxCollider>(); 

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EnableNotification();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")&&removeAfterExit)
        {
            RemoveNotification();
        }
    }

    void EnableNotification()
    {
        objectcollider.enabled = false;
        notianim.Play("FadeIn");
        notiTextUI.text = NotificationMessage;
       // CharIconUi.sprite=urIcon;

        Invoke(nameof(RemoveNotification), DisableTimer);
    }
    private void RemoveNotification()
    {
        Debug.Log("testeafdffasdfljasdf;jdasfk");
        notianim.Play("FadeOut");
    }
}
