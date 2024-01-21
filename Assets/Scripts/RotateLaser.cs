using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0,1), speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ok");
    }
}
