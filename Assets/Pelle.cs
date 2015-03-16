using UnityEngine;
using System.Collections;

public class Pelle : MonoBehaviour {

    BActionPelle actionPelle;

	// Use this for initialization
	void Start () {
        actionPelle = GetComponentInParent<BActionPelle>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter(Collision collision)
    {
        if(actionPelle.active)
        {
            if(collision.gameObject.GetComponent<Rigidbody>())
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, actionPelle.forcePelle, 0), ForceMode.Impulse);
                Debug.Log("pelette");
            }
        }
    }

}
