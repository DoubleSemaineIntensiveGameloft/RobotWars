using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {

	private Rigidbody botRigidbody;

    public float speed = 10;

	void Start () {
		botRigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
	
	}

	public void Move(Vector3 direction)
	{
        botRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);

		transform.LookAt (transform.position + direction);
	}
}
