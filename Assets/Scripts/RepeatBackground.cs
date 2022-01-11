using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveLeft))]
public class RepeatBackground : MonoBehaviour
{
    public Vector3 InitialPos;
    public float RepeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        InitialPos = transform.position;
        RepeatWidth = GetComponent<BoxCollider>().size.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < InitialPos.x - RepeatWidth)
        {
            transform.position = InitialPos;
        }
    }
}
