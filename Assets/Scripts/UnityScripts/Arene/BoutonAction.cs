using UnityEngine;
using System.Collections;

public class BoutonAction : MonoBehaviour {

    public BlockAction blocLinked;

    private Collider2D colliderBtn;

    public bool pressed = false;

	// Use this for initialization
	void Start () {
        colliderBtn = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        bool pressedThisFrame = false;

        foreach (Touch touch in Input.touches)
        {
            if (Physics2D.OverlapPoint(touch.position) == colliderBtn)
            {
                pressedThisFrame = true;
            }
        }

        if(pressedThisFrame && !pressed)
        {
            pressed = true;
            DoAction();
        }

        if(!pressedThisFrame)
        {
            pressed = false;
        }

	
	}

    public void DoAction()
    {
        blocLinked.ActivateBlock();
    }
}
