using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;
    private Animator PlayerAnimator;
    private Rigidbody PlayerRigidbody;
    [SerializeField] private float JumpForce = 400f;
    public float GravityModifier = 1f;
    private bool IsOnTheGround = true;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        PlayerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;

        PlayerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOnTheGround)
        {
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnTheGround = false;
            PlayerAnimator.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Ground"))
        {
            IsOnTheGround = true;
        }

        if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            //Muerte random
            int RandomDeath = Random.Range(1, 3);
            PlayerAnimator.SetBool("Death_b", true);
            PlayerAnimator.SetInteger("DeathType_int", RandomDeath);

            gameOver = true;
        }

    }
}
