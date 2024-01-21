using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;


    [SerializeField]
    [Min(0)]
    private int bounces = 1;

    private List<Ray2D> rays;
    private List<RaycastHit2D> hits;
    private List<Vector2> normals;
    private List<Vector2> dirs;

    private Ray2D ray;
    private Vector2 dir;
    private Vector2 normal;
    private RaycastHit2D hit;
    float alpha = 1.0f;
    [SerializeField]
    LayerMask layermask;

    Audiomanager audioManager;

    [SerializeField]
    GameObject Cross;

    bool isGameover, hittingReceiver;

    LaserReceiver lastLaserReceiver;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        rays = new List<Ray2D>();
        hits = new List<RaycastHit2D>();
        normals = new List<Vector2>();
        dirs = new List<Vector2>();
        audioManager = GameObject.Find("audio").GetComponent<Audiomanager>();

        for (int a = 0; a < bounces + 1; a++)
        {
            rays.Add(ray);
            hits.Add(hit);
            normals.Add(normal);
            dirs.Add(dir);
        }
    }

    void FixedUpdate()
    {
        if(!hittingReceiver)
        {
            if(lastLaserReceiver != null) 
            {
                lastLaserReceiver.LaserRecived = false;
                lastLaserReceiver = null;
            }
        }

        hittingReceiver = false;




        //aiming(); // aiming
        dirs[0] = transform.right;

        int bounceCount = bounces;

        for (int i = 1; i < bounces + 2; i++)
        {
            
                rays.Add(ray);
                hits.Add(hit);
                normals.Add(normal);
                dirs.Add(dir);

                switch (i)
                {
                    case 1:
                        rays[i - 1] = new Ray2D(transform.position, dirs[i - 1]);
                        hits[i - 1] = Physics2D.Raycast(rays[i - 1].origin, rays[i - 1].direction, Mathf.Infinity, layermask);
                        lineRenderer.positionCount = bounces + 2;
                        lineRenderer.SetPosition(i - 1, rays[i - 1].origin);
                        lineRenderer.SetPosition(i, hits[i - 1].point);

                    if(hits[i - 1].collider.CompareTag("Block"))
                    {
                        lineRenderer.positionCount = i + 2;
                        lineRenderer.SetPosition(i, hits[i - 1].point);
                        lineRenderer.SetPosition(i + 1, hits[i - 1].point);
                        i = bounceCount + 1;



                    }
                    else if (hits[i - 1].collider.CompareTag("LaserReceiver"))
                    {

                        lastLaserReceiver = hits[i - 1].collider.GetComponent<LaserReceiver>();
                        lastLaserReceiver.LaserRecived = true;
                        hittingReceiver = true;

                        lineRenderer.positionCount = i + 2;
                        lineRenderer.SetPosition(i, hits[i - 1].point);
                        lineRenderer.SetPosition(i + 1, hits[i - 1].point);
                        i = bounceCount + 1;
                    }

                    else if (hits[i - 1].collider.CompareTag("Player"))
                    {
                        if (!isGameover)
                        {

                            DisableRotateObjectComponentsInScene();
                            hits[i - 1].collider.GetComponent<Movement>().enabled = false;
                            hits[i - 1].collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            GameOver();
                            Instantiate(Cross, hits[i - 1].point, hits[i - 1].collider.transform.rotation);
                            isGameover = true;

                        }
                    }


                    break;

                    default:
                    
                        normals[i - 2] = hits[i - 2].normal;
                        dirs[i - 1] = Vector2.Reflect(rays[i - 2].direction, normals[i - 2].normalized);
                        rays[i - 1] = new Ray2D(hits[i - 2].point + dirs[i - 1] * 0.01f, dirs[i - 1]);
                        hits[i - 1] = Physics2D.Raycast(rays[i - 1].origin, rays[i - 1].direction, Mathf.Infinity, layermask);

                    lineRenderer.positionCount = bounces + 2;
                    lineRenderer.SetPosition(i, hits[i - 1].point);

                    if (hits[i -1].collider.CompareTag("Block"))
                    {
                        lineRenderer.positionCount = i + 2;
                        lineRenderer.SetPosition(i, hits[i - 1].point);
                        lineRenderer.SetPosition(i + 1, hits[i - 1].point);
                        i = bounceCount +1;

                        

                    }
                    else if (hits[i - 1].collider.CompareTag("LaserReceiver"))
                    {
                       
                        lastLaserReceiver = hits[i - 1].collider.GetComponent<LaserReceiver>();
                        lastLaserReceiver.LaserRecived = true;
                        hittingReceiver = true;

                        lineRenderer.positionCount = i + 2;
                        lineRenderer.SetPosition(i, hits[i - 1].point);
                        lineRenderer.SetPosition(i + 1, hits[i - 1].point);
                        i = bounceCount + 1;
                    }

                    else if (hits[i - 1].collider.CompareTag("Player"))
                    {
                        if (!isGameover)
                        {

                            DisableRotateObjectComponentsInScene();
                            hits[i - 1].collider.GetComponent<Movement>().enabled = false;
                            hits[i - 1].collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            GameOver();
                            Instantiate(Cross, hits[i - 1].point, hits[i - 1].collider.transform.rotation);
                            isGameover = true;

                        }
                    }

                    


                    break;
                }

            

        }

        
    }

    void aiming()
    {
        dirs[0] = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dirs[0].y, dirs[0].x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    void DisableRotateObjectComponentsInScene()
    {
        // Find all GameObjects with RotateObject components in the scene
        GameObject[] objectsWithRotateObject = GameObject.FindGameObjectsWithTag("Rotate"); // Use an appropriate tag to identify these objects

        foreach (GameObject obj in objectsWithRotateObject)
        {
            // Disable RotateObject components on each GameObject
            RotateObject rotateObjectComponent = obj.GetComponent<RotateObject>();

            if (rotateObjectComponent != null)
            {
                rotateObjectComponent.enabled = false;
            }
        }
    }


    void GameOver()
    {
        audioManager.StopSound("Move");
        audioManager.PlaySound("Dead");
        GameObject.Find("LaserSFX").GetComponent<AudioSource>().volume = 0.2f;
        Invoke("RestartLevel", 2);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}