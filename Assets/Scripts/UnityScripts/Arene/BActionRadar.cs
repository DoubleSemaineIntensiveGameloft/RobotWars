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
                Quaternion lookRot = Quaternion.LookRotation((robotAdverse.transform.position - bot.transform.position).normalized);

                bot.botRigidbody.MoveRotation(Quaternion.Lerp(bot.transform.rotation, lookRot, 10 * Time.deltaTime));
            }

        }
	}

    public override void ActivateBlock()
    {
    }


}

