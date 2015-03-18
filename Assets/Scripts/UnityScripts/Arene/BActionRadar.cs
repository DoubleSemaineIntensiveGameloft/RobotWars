using UnityEngine;
using System.Collections;

public class BActionRadar : BlockAction
{

    public GameObject robotAdverse;
    
    
    public override void Start()
    {
        base.Start();


    }
	
	public override void Update () {
        base.Update();
        if(bot == null || robotAdverse == null)
        {
            bot = GetComponentInParent<Bot>();

            if(bot != null)
            {

                GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

                foreach (GameObject actBot in bots)
                {
                    if (actBot != bot.gameObject)
                    {
                        robotAdverse = actBot;
                    }
                }
            }
        }
        else
        {
            if(bot.direction == Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation((new Vector3(robotAdverse.transform.position.x, bot.transform.position.y, robotAdverse.transform.position.z) - bot.transform.position).normalized);

                bot.botRigidbody.MoveRotation(Quaternion.Lerp(bot.transform.rotation, lookRot, 2.5f * Time.deltaTime));
            }

        }
	}

    public override void ActivateBlock()
    {
    }


}

