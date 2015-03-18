using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoutonAction : MonoBehaviour {

    public BlockAction blocLinked;

    private Collider2D colliderBtn;

    public bool pressed = false;
    public Image imageBtn;

    public AnimationCurve animCurveBump;
    public bool feedbackBump = false;

    public float actTimeFeedbackBump = 0;
    public float timeFeedbackBump = 0.5f;

	// Use this for initialization
	void Start () {
        colliderBtn = GetComponent<Collider2D>();
        imageBtn = GetComponent<Image>();
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

        if(blocLinked.actTimeCooldown < blocLinked.timeCooldown)
        {
            imageBtn.color = new Color(1, 1, 1, 0.5f);
            feedbackBump = false;
        }
        else if (!feedbackBump)
        {
            feedbackBump = true;
            actTimeFeedbackBump = 0;

            imageBtn.color = new Color(1, 1, 1, 1f);
        }

        if(actTimeFeedbackBump < timeFeedbackBump)
        {
            actTimeFeedbackBump += Time.deltaTime;
            float bumpSize = animCurveBump.Evaluate(actTimeFeedbackBump / timeFeedbackBump) * 0.3f;
            imageBtn.transform.localScale = new Vector3(bumpSize, bumpSize, bumpSize);
        }

	
	}

    public void DoAction()
    {
        if(!blocLinked.bot.stun)
        { 
        blocLinked.ActivateBlock();
        }
    }
}
