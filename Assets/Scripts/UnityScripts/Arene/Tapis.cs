using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tapis : MonoBehaviour {


    public float souffleForce = 10;

    public Vector3 direction;

    public List<Rigidbody> objetsInSouffle = new List<Rigidbody>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        foreach (Rigidbody actRigid in objetsInSouffle)
        {
            if (actRigid)
            {
                actRigid.AddForce(direction * souffleForce * Time.fixedDeltaTime); // (1- (Vector3.Distance(actRigid.transform.position, parentSouffle.transform.position) / 4.5f)) * 

            }
        }
        
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            objetsInSouffle.Add(other.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            if(objetsInSouffle.Contains(other.GetComponent<Rigidbody>()))
            {
                objetsInSouffle.Remove(other.GetComponent<Rigidbody>());
            }
        }
    }
}
