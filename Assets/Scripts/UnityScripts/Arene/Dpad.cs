using UnityEngine;
using System.Collections;

public class Dpad : MonoBehaviour {

    public bool active = false;
    public float distanceMax = 5;


    public Transform pad;
    public Bot bot;
    
    private int actTouchId;
    private Collider2D colliderPad;

    

	void Start () {

        pad = transform.FindChild("Pad");
        colliderPad = GetComponent<Collider2D>();
	
	}

	void Update () {



        if(!active)
        { 
            foreach (Touch touch in Input.touches)
            {            
                if (Physics2D.OverlapPoint(touch.position) == colliderPad)
                {
                    actTouchId = touch.fingerId;
                    active = true;
                }
            }
        }
        else
        {
            bool touchActive = false;

            foreach (Touch touch in Input.touches)
            {
                if(touch.fingerId == actTouchId)
                {
                    touchActive = true;
                }
            }

            if(!touchActive)
            {
                active = false;
            }
        }


        if(!active)
        { 
            pad.transform.localPosition = Vector3.zero;
            
        } 
        else
        {
            UpdatePad();
            Vector3 padDirection = (pad.transform.position - transform.position).normalized;
            bot.Move(new Vector3(padDirection.x, 0, padDirection.y));
        }


	
	}

    public void UpdatePad()
    {

        Vector3 touchPosition = Vector3.zero;

        foreach (Touch touch in Input.touches)
        {
            if(touch.fingerId == actTouchId)
            {
                touchPosition = touch.position;
            }
        }
       
        pad.transform.position = touchPosition;

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
