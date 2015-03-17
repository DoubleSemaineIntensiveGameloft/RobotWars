using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{

    private Rigidbody botRigidbody;
    private ParticleSystem partStun;
    private ParticleSystem partHit;
    private Animator animator;

    public float speed = 1;
    public float rotationSpeed = 5;

    public Vector3 direction = Vector3.zero;

    public bool braked = false;

    public bool stun = false;
    public float actTimeStun = 0;
    public float timeStun = 2;

    public Vector3 startPosition = Vector3.zero;

    public BoxCollider boxCollider;


    void Start()
    {
        botRigidbody = GetComponent<Rigidbody>();
        partStun = transform.FindChild("ParticlesElectricityStun").GetComponent<ParticleSystem>();
        partHit = transform.FindChild("ParticlesHit").GetComponent<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();

    }

    void Update()
    {

        if (!stun)
        {
            if (direction != Vector3.zero)
            {
                //transform.LookAt(transform.position + direction * 10);

                Quaternion lookRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
                animator.SetFloat("Speed", 1);

                Debug.DrawRay(transform.position + new Vector3(0, 2, 0), transform.forward * 10f + new Vector3(0, -2f, 0));

                Ray rayVide = new Ray(transform.position + new Vector3(0, 2, 0), (transform.forward + new Vector3(0, -2f, 0)).normalized);

                if (!Physics.Raycast(rayVide, 10, 1 << 10))
                {
                    if (!braked)
                    {
                        /*
                        direction = Vector3.zero;
                        direction *= -1;*/
                        botRigidbody.AddForce(transform.forward * -speed, ForceMode.Impulse);
                        braked = true;
                    }
                }
                else
                {
                    braked = false;
                }
            }
            else
            {

                animator.SetFloat("Speed", 0);
            }




        }
        else
        {
            actTimeStun += Time.deltaTime;
            if (actTimeStun > timeStun)
            {
                actTimeStun = 0;
                stun = false;
                partStun.Stop();

                animator.SetBool("Stun", false);

            }
        }

    }

    void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {

            botRigidbody.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    public void Move(Vector3 _direction, float _speed = 0.1f)
    {
        direction = _direction;

        //speed = _speed;

        botRigidbody.AddForce(transform.forward * speed / 40, ForceMode.Impulse);
    }

    public void Stun(float _timeStun)
    {

        timeStun = _timeStun;
        partStun.Play();
        actTimeStun = 0;
        stun = true;
        animator.SetBool("Stun", true);
    }

    public void Restart()
    {
        transform.position = startPosition;
        direction = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bot")
        {
            partHit.transform.position = collision.contacts[0].point;
            partHit.Emit(15);
            partHit.transform.rotation = Quaternion.LookRotation((transform.position - collision.transform.position).normalized);
        }
    }
}
