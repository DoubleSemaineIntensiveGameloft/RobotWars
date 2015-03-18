using UnityEngine;
using System.Collections;

public class BActionTaseur : BlockAction
{
    public float timeStun = 3;

    public ParticleSystem partElec;

    bool coolingDown = false;

    
    
    public override void Start()
    {
        base.Start();
        partElec = GetComponentInChildren<ParticleSystem>();

    }
	
	public override void Update () {
        base.Update();
        if(coolingDown && actTimeCooldown > timeCooldown)
        {
            coolingDown = false;
            partElec.Emit(10);
            partElec.gameObject.SetActive(true);
        }
	}

    public override void ActivateBlock()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("taz4");
        if(actTimeCooldown >= timeCooldown)
        {
            Debug.Log("taz3");
            if (collision.transform.tag == "Bot")
            {
                Debug.Log("taz2");
                if(bot.gameObject != collision.gameObject)
                {
                    Debug.Log("taz");
                    // TAZZ
                    coolingDown = true;
                    actTimeCooldown = 0;
                    //partElec.Stop();
                    partElec.gameObject.SetActive(false);

                    collision.gameObject.GetComponent<Bot>().Stun(timeStun);
                }
            }

        }
    }

}

