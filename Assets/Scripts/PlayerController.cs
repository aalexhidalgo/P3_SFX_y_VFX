using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;

    public ParticleSystem ExplosionParticleSystem;
    public ParticleSystem DirtSplattParticleSystem;

    public AudioClip JumpClip;
    public AudioClip CrashClip;

    private AudioSource PlayerAudioSource;
    private AudioSource CameraAudioSource;


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

        CameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOnTheGround && !gameOver)
        {
            //Salta nuestro jugador
            PlayerRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnTheGround = false;

            //Animación de salto (Para saber condiciones "Animator")
            PlayerAnimator.SetTrigger("Jump_trig");

            //Paramos la animación de la tierra (Indicamos cual y lo paramos)
            DirtSplattParticleSystem.Stop();

            //SFX de salida (Indicamos que se reproduzca una vez, cual de los clips queremos que se reproduzca y a cuanto de volumen)
            PlayerAudioSource.PlayOneShot(JumpClip, 1);
        }
    }

    private float lives = 3;
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
                Destroy(otherCollider.gameObject);
                lives--;

                if(lives <= 0)
                {
                    GameOver();
                }
                else
                {
                    PlayerAnimator.SetTrigger("Crash_trig");
                }
            }
        }

    }
    private void GameOver()
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

        // Otras formas

        /*if (IsOnTheGround == true)
        {
            DirtSplattParticleSystem.Stop();
        }
        */

        // Destroy(DirtSplattParticleSystem);

        DirtSplattParticleSystem.Stop();

        //SFX de salida (Indicamos que se reproduzca una vez, cual de los clips queremos que se reproduzca y a cuanto de volumen)
        PlayerAudioSource.PlayOneShot(CrashClip, 1);

        //Bajar el volumen cuando muere el jugador 
        CameraAudioSource.volume = 0.2f;
    }
}
