using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pelle : MonoBehaviour {

    public BActionPelle actionPelle;


    public List<Rigidbody> objetsInPelle = new List<Rigidbody>();

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

        if(actionPelle)
        {
            if(actionPelle.active)
            {
                foreach (Rigidbody actRigid in objetsInPelle)
                {
                   // Debug.Log("actionPelle.bot.transform:" + actionPelle.transform.name);
                    if (actRigid.transform != actionPelle.bot.transform)
                    {
                        AudioSystem.instance.PlayAudio(0);
                        actRigid.AddForce((actRigid.transform.position - transform.position).normalized * 25 + new Vector3(0, actionPelle.forcePelle, 0), ForceMode.Impulse);
                    }
                }
                actionPelle.active = false;
            }
        }
	
	}

    /*
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
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            objetsInPelle.Add(other.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            if (objetsInPelle.Contains(other.GetComponent<Rigidbody>()))
            {
                objetsInPelle.Remove(other.GetComponent<Rigidbody>());
            }
        }
    }

}
