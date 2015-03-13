using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {

	private Rigidbody botRigidbody;

    public float speed = 1;
    public float rotationSpeed = 5;

    public Vector3 direction = Vector3.zero;

    public bool braked = false;

	void Start () {
		botRigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
        if (direction != Vector3.zero)
        {
            //transform.LookAt(transform.position + direction * 10);


            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
        }

        Debug.DrawRay(transform.position + new Vector3(0, 2, 0),  transform.forward * 10f + new Vector3(0,-2f,0));

        Ray rayVide = new Ray(transform.position + new Vector3(0, 2, 0),(transform.forward + new Vector3(0,-2f,0)).normalized);

        if (!Physics.Raycast(rayVide, 10, 1 << 10))
        {
            if (!braked)
            { 
                direction = Vector3.zero;
                botRigidbody.AddForce(transform.forward * -speed/5, ForceMode.Impulse);
                braked = true;
            }
        }
        else
        {
            braked = false;
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
        
        //botRigidbody.AddForce(transform.forward * speed * 2, ForceMode.Impulse);
	}
}
