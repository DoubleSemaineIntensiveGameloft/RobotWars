using UnityEngine;
using System.Collections;

public class BActionCanon : BlockAction {

    public GameObject boule;
    public float forceBoule = 10;

    Collider colliderGraph;


	void Start () {
        colliderGraph = transform.FindChild("Graph").GetComponent<Collider>();
	}
	
	public override void Update () {
        base.Update();
	}

    public override void ActivateBlock()
    {
        if(actTimeCooldown >= timeCooldown)
        {
            actTimeCooldown = 0;
            GameObject newBoule = Instantiate(boule, transform.position, Quaternion.identity) as GameObject;
            Physics.IgnoreCollision(newBoule.GetComponent<Collider>(), colliderGraph);

            newBoule.GetComponent<Rigidbody>().AddForce(transform.forward * forceBoule, ForceMode.Impulse);

        }
    }
}
