using UnityEngine;
using System.Collections;

public class BlockAction : MonoBehaviour {


    public float actTimeCooldown = 0;
    public float timeCooldown = 1;

    public Block block;

	// Use this for initialization
    public void Awake()
    {
        block = GetComponent<Block>();
	
	}

    public virtual void Start()
    {

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
