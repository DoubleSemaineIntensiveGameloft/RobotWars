using UnityEngine;
using System.Collections;

public class Pelle : MonoBehaviour {

    public BActionPelle actionPelle;

	// Use this for initialization
	void Start () {

        actionPelle = GetComponentInParent<BActionPelle>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if(ManagerGame.instance && actionPelle == null)
        {
            actionPelle = GetComponentInParent<BActionPelle>();
        }
	
	}


    void OnTriggerEnter(Collider collision)
    {
        if (actionPelle)
        { 
            if(actionPelle.active)
            {
                if(collision.gameObject.GetComponent<Rigidbody>())
                {
                    Rigidbody rigidCol = collision.gameObject.GetComponent<Rigidbody>();
                    Debug.Log("actionPelle.bot.transform:" + actionPelle.transform.name);
                    if (rigidCol.transform != actionPelle.bot.transform)
                    { 
                        rigidCol.AddForce(new Vector3(0, actionPelle.forcePelle, 0), ForceMode.Impulse);
                        Debug.Log("pelled");
                    }
                }
            }
        }
    }

}
