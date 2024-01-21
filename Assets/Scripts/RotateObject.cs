
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Camera myCam;
    private Vector3 screenPos;
    private float angleOffset;


    [SerializeField]
    float radius, smoothTime;

    bool ObjectSelecte;

    AudioSource audioManager;
    float volume = 0.2f;

    private void Start()
    {
        myCam = Camera.main;
        audioManager = GameObject.Find("LaserSFX").GetComponent<AudioSource>();

    }

    private void Update()
    {
        Vector3 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(mousePos, transform.position) < radius)
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Input.mousePosition - screenPos;
                angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                ObjectSelecte = true;
                
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (ObjectSelecte)
            {
                Vector3 vec3 = Input.mousePosition - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;

                Quaternion targeRotation = Quaternion.Euler(0, 0, angle + angleOffset);
                transform.rotation =  Quaternion.RotateTowards(transform.rotation, targeRotation, smoothTime * 300 * Time.deltaTime);

                if(volume <0.8f)
                {
                    volume += Time.deltaTime;
                    audioManager.volume = volume;
                }

                

            }  
        }
        else
        {
            ObjectSelecte=false;
            if (volume > 0.2f)
            {
                volume -= Time.deltaTime;
                audioManager.volume = volume;
            }
        }
        
    }
}

