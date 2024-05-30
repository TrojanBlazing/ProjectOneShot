using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realastic : MonoBehaviour
{
    private GameObject follow;
    private Vector3 offset;
    [SerializeField] float speed = 1.0f;
    void Start()
    {
        follow = Camera.main.gameObject;
        offset= transform.position-follow.transform.position;
    }

    
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position=follow.transform.position+offset;
        transform.rotation = Quaternion.Slerp(transform.rotation, follow.transform.rotation, speed * Time.deltaTime);
    }
}
