using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {

	private Rigidbody botRigidbody;

    public float speed = 1;
    public float rotationSpeed = 5;

    public Vector3 direction = Vector3.zero;

	void Start () {
		botRigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
	    if(direction != Vector3.zero)
        {
            //transform.LookAt(transform.position + direction * 10);


            Quaternion lookRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);

            botRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
	}

	public void Move(Vector3 _direction, float _speed = 0.1f)
	{
        direction = _direction;

        //speed = _speed;
        
        //botRigidbody.AddForce(transform.forward * speed * 2, ForceMode.Impulse);
	}
}
