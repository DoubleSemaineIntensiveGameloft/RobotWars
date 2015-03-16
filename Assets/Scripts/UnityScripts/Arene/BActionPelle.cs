using UnityEngine;
using System.Collections;

public class BActionPelle : BlockAction {

    public float forcePelle = 10;


    public bool active = false;

    public float actTimeActive = 0;
    public float timeActive = 0.5f;

    private Animator animatorBot;

    public override void Start()
    {
        base.Start();
        animatorBot = GetComponentInChildren<Animator>();
	}
	
	public override void Update () {
        base.Update();

        if (active)
        {
            actTimeActive += Time.deltaTime;
            if (actTimeActive > timeActive)
            {
                actTimeActive = 0;
                // Deactivate
                active = false;
            }
        }
	}

    public override void ActivateBlock()
    {
        if(actTimeCooldown >= timeCooldown)
        {
            actTimeCooldown = 0;

            animatorBot.SetTrigger("Action");
            active = true;

        }
    }
}
