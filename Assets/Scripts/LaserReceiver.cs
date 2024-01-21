using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LaserReceiver : MonoBehaviour
{
    public bool LaserRecived, LaserRecived2;
    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if(LaserRecived)
        {
            lineRenderer.SetColors(Color.yellow, Color.yellow);
        }
        else
        {
            lineRenderer.SetColors(Color.white, Color.white);
        }
    }



}
