using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NotificationScr")]
public class NotificationScriptable : ScriptableObject
{

    [Header("MessageCustomization")]
    public Sprite urIcon;
    [TextArea] public string NotificationMessage;

    [Header("NotificationRemove")]
    public bool removeAfterExit = false;
    public bool disableAfterTimer = false;
    public float DisableTimer = 1.0f;

}
