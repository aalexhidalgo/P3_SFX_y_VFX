using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce = 400f;
    public float GravityModifier = 1f;
    private bool IsOnTheGround = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOnTheGround)
        {
            PlayerRigidbody.AddForce(Vector3.up * JumpForce);
            IsOnTheGround = false;
        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        IsOnTheGround = true;
    }
}
