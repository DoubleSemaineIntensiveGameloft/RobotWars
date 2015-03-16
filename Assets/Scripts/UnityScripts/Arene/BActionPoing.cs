using UnityEngine;
using System.Collections;

public class BActionPoing : BlockAction {

    Collider colliderGraph;

    Animator animatorPoing;
    
    public override void Start()
    {
        base.Start();
    
        colliderGraph = transform.FindChild("Graph").GetComponent<Collider>();

        animatorPoing = GetComponentInChildren<Animator>();
	}
	
	public override void Update () {
        base.Update();
	}

    public override void ActivateBlock()
    {
        if(actTimeCooldown >= timeCooldown)
        {
            actTimeCooldown = 0;

            animatorPoing.SetTrigger("Action");


        }
    }
}
