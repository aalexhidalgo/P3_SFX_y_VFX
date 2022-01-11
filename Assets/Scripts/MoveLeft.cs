using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float Speed = 10f;
    private PlayerController PlayerControllerScript;

    //private float XLimit = 5f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        
        //Otra forma

        /*if (transform.position.x < -XLimit && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        */
    }
}
