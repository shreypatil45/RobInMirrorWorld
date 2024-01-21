using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    bool lastLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.CompareTag("Player"))
        {
            if(lastLevel)
            {
                SceneManager.LoadScene(0);
                GameObject.Find("audio").GetComponent<Audiomanager>().StopSound("Move");
                return;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameObject.Find("audio").GetComponent<Audiomanager>().StopSound("Move");
        }
    }
}
