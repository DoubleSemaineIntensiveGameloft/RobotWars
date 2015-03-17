using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Souffle : MonoBehaviour {

    public bool active = false;

    public float souffleForce = 10;

    public Transform parentSouffle;
    public List<Rigidbody> objetsInSouffle = new List<Rigidbody>();

	// Use this for initialization
	void Start () {
        parentSouffle = transform.parent;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(active)
        {
            foreach (Rigidbody actRigid in objetsInSouffle)
            {
                if (actRigid)
                {
                    actRigid.AddForce((actRigid.transform.position - parentSouffle.transform.position).normalized * souffleForce * Time.fixedDeltaTime); // (1- (Vector3.Distance(actRigid.transform.position, parentSouffle.transform.position) / 4.5f)) * 
                    Debug.Log("lolSouffle");
                }
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
