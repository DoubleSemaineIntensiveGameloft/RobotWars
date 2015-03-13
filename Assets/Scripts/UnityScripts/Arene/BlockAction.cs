using UnityEngine;
using System.Collections;

public class BlockAction : MonoBehaviour {


    public float actTimeCooldown = 0;
    public float timeCooldown = 1;

	// Use this for initialization
	void Start () {
	
	}
	
    public virtual void Update()
    {
	
        if(actTimeCooldown < timeCooldown)
        {
            actTimeCooldown += Time.deltaTime;
        }
	}

    public virtual void ActivateBlock()
    {

    }
}
