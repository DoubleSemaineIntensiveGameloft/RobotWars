using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {

	private Rigidbody botRigidbody;

    public float speed = 10;
    public float rotationSpeed = 5;

    public Vector3 direction = Vector3.zero;

	void Start () {
		botRigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
	    if(direction != Vector3.zero)
        {
            //transform.LookAt(transform.position + direction * 10);


            Quaternion lookRot = Quaternion.LookRotation(transform.position + direction * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);

            botRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
	}

	public void Move(Vector3 _direction)
	{
        direction = _direction;
	}
}
