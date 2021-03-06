using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Obstacle;
    private Vector3 SpawnPos = new Vector3(25, 0, 0);
    public float StartDelay = 2f;
    public float RepeatRate = 2f;

    private PlayerController PlayerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        /*Otra forma
        PlayerControllerScript = FindObjectOfType<PlayerController>();
        */

        InvokeRepeating("SpawnObject", StartDelay, RepeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObject()
    {
        if (!PlayerControllerScript.gameOver)
        {
            Instantiate(Obstacle, SpawnPos, Obstacle.transform.rotation);
        }
    }
}
