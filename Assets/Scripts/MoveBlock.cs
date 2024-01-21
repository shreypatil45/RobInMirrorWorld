using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField]
    LaserReceiver lr;
    GameObject block;
    Transform StartPos, EndPos;

    public float moveSpeed = 0.5f;

    void Start()
    {
        block = gameObject.transform.Find("YellowBlock").gameObject;
        StartPos = gameObject.transform.Find("StartPos").transform;
        EndPos = gameObject.transform.Find("EndPos").transform;
    }

    void FixedUpdate()
    {
        if (lr.LaserRecived) 
        {
            block.transform.position = Vector3.MoveTowards(block.transform.position,EndPos.position,moveSpeed);
        }
        else if(!lr.LaserRecived) 
        {
            block.transform.position = Vector3.MoveTowards(block.transform.position, StartPos.position, moveSpeed);
        }
    }


    
}
