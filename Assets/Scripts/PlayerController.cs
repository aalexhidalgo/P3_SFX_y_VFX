using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;

    public ParticleSystem ExplosionParticleSystem;
    public ParticleSystem DirtSplattParticleSystem;
    
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
        if(Input.GetKeyDown(KeyCode.Space) && IsOnTheGround && !gameOver)
        {
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnTheGround = false;
            PlayerAnimator.SetTrigger("Jump_trig");
            DirtSplattParticleSystem.Stop();
        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver)
        {
            if (otherCollider.gameObject.CompareTag("Ground"))
            {
                IsOnTheGround = true;

                //Activamos el sistema de partículas de la tierra
                DirtSplattParticleSystem.Play();
            }

            if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                //Muerte random
                int RandomDeath = Random.Range(1, 3);
                PlayerAnimator.SetBool("Death_b", true);
                PlayerAnimator.SetInteger("DeathType_int", RandomDeath);

                gameOver = true;

                //Si murere se activa la explosión de humo y se desactiva la de tierra al avanzar
                Vector3 ExplosionOffset = new Vector3(0, 1, 0);
                Instantiate(ExplosionParticleSystem, transform.position + ExplosionOffset, ExplosionParticleSystem.transform.rotation);

                // ExplosionParticleSystem.Play();

                /*if (IsOnTheGround == true)
                {
                    DirtSplattParticleSystem.Stop();
                }
                */

                // Destroy(DirtSplattParticleSystem);

                DirtSplattParticleSystem.Stop();
            }
        }

    }
}
