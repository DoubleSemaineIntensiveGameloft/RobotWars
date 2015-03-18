using UnityEngine;
using System.Collections;

public class BlockAction : MonoBehaviour {

    public Sprite spriteButton;

    public float actTimeCooldown = 0;
    public float timeCooldown = 1;

    public Block block;
    public Bot bot;

    public bool ignoreSet = false;

	// Use this for initialization
    public void Awake()
    {
        block = GetComponent<Block>();
        bot = GetComponentInParent<Bot>();
        actTimeCooldown = timeCooldown;
	
	}

    public virtual void Start()
    {
        
    }
	
    public virtual void Update()
    {
        if (ManagerGame.instance && bot == null)
        {
            bot = GetComponentInParent<Bot>();
            if(GetComponent<BoxCollider>())
            {
                Physics.IgnoreCollision(GetComponent<BoxCollider>(), bot.GetComponent<BoxCollider>());
            }
        }

        /*
        if (!ignoreSet)
        {
            if (GameObject.Find("ManagerGame") != null)
            {
                if (GetComponent<BoxCollider>())
                {
                    if (bot.GetComponent<BoxCollider>())
                    {
                        Debug.Log("IGNORE");
                        Physics.IgnoreCollision(GetComponent<BoxCollider>(), bot.GetComponent<BoxCollider>(), true);
                    }
                }
                ignoreSet = true;
            }
        }
         */ 

        if(actTimeCooldown < timeCooldown)
        {
            actTimeCooldown += Time.deltaTime;
        }
	}

    public virtual void ActivateBlock()
    {

    }
}
