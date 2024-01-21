using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    [SerializeField]
    float StartIntensity, EndIntensity, time, speed;
    HardLight2D hardLight2D;

    bool goingForward;

    private void Start()
    {
        hardLight2D = GetComponent<HardLight2D>();
        StartIntensity = hardLight2D.Intensity;
    }

    private void FixedUpdate()
    {
        if(goingForward)
        {
            time += speed * 0.1f;

            hardLight2D.Intensity = Mathf.Lerp(StartIntensity, EndIntensity, time);
            if(time >= 1f)
            {
                goingForward = false;
            }
        }
        else
        {
            time -= speed * 0.1f;
            hardLight2D.Intensity = Mathf.Lerp(StartIntensity, EndIntensity, time);
            if (time <= 0f)
            {
                goingForward = true;
            }
        }



    }
}
