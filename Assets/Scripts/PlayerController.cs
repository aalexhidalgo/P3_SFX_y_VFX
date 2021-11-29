using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce = 400f;
    public float GravityModifier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRigidbody.AddForce(Vector3.up * JumpForce);
        }
    }
}
