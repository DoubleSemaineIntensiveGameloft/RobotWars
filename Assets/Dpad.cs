using UnityEngine;
using System.Collections;

public class Dpad : MonoBehaviour {

    public bool active = false;

    public Transform pad;

    public Bot bot;

    public float distanceMax = 5;

    

	void Start () {

        pad = transform.FindChild("Pad");
	
	}

	void Update () {

        if(!active)
        { 
            pad.transform.localPosition = Vector3.zero;
        } else
        {
            Vector3 padDirection = (pad.transform.position - transform.position).normalized;
            bot.Move(new Vector3(padDirection.x, 0, padDirection.y));
        }
	
	}

    public void UpdatePad()
    {
        active = true;
        pad.transform.position = Input.mousePosition;

        Vector3 padDirection = (pad.transform.position - transform.position).normalized;

        if(Vector3.Distance(pad.position, transform.position) > distanceMax)
        {
            pad.transform.localPosition = padDirection * distanceMax;
        }



    }

    public void DeActivate()
    {
        active = false;
    }
}
