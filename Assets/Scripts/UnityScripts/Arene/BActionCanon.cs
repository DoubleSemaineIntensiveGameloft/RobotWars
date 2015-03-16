using UnityEngine;
using System.Collections;

public class BActionCanon : BlockAction {

    public GameObject boule;
    public float forceBoule = 10;

    Collider colliderGraph;


    public override void Start()
    {
        base.Start();
        colliderGraph = GetComponent<Collider>();
	}
	
	public override void Update () {
        base.Update();
	}

    public override void ActivateBlock()
    {
        if(actTimeCooldown >= timeCooldown)
        {
            actTimeCooldown = 0;
            GameObject newBoule = Instantiate(boule, transform.position + transform.up * -1, Quaternion.identity) as GameObject;
            Physics.IgnoreCollision(newBoule.GetComponent<Collider>(), colliderGraph);

            Vector3 directionForce = (transform.up * -1 * forceBoule);
            directionForce = new Vector3(directionForce.x, transform.position.y, directionForce.z);

            newBoule.GetComponent<Rigidbody>().AddForce(directionForce, ForceMode.Impulse);

        }
    }
}
