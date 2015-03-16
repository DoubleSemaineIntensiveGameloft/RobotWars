using UnityEngine;
using System.Collections;

public class BActionSouffleuse : BlockAction {

    public GameObject boule;
    public Souffle souffle;
    public ParticleSystem partSouffle;
    Collider colliderGraph;

    public bool active = false;

    public float actTimeActive = 0;
    public float timeActive = 5;

    public override void Start()
    {
        base.Start();
        colliderGraph = transform.FindChild("Graph").GetComponent<Collider>();
        souffle = gameObject.GetComponentInChildren<Souffle>();
        partSouffle = GetComponentInChildren<ParticleSystem>();
	}
	
	public override void Update () {
        base.Update();

        if(active)
        {
            actTimeActive += Time.deltaTime;
            if(actTimeActive > timeActive)
            {
                actTimeActive = 0;
                // Deactivate
                partSouffle.Stop();
                souffle.active = false;
                active = false;
            }
        }

	}

    public override void ActivateBlock()
    {
        if(actTimeCooldown >= timeCooldown)
        {
            actTimeCooldown = 0;

            souffle.active = true;
            partSouffle.Play();
            active = true;



        }
    }
}
