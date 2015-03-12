using UnityEngine;
using System.Collections;

public class MoveSlide : MonoBehaviour {

    public Bot botControlled;

    public int side = 0;

    public Vector2 initialPosition = Vector2.zero;
    private int touchId = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(initialPosition != Vector2.zero)
        {
            foreach (Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Ended)
                {
                    if(touch.fingerId == touchId)
                    {
                        if(Vector2.Distance(initialPosition, touch.position) > 2)
                        {
                            // Move
                            Vector2 moveDirection = (touch.position - initialPosition).normalized;

                            botControlled.Move(new Vector3(moveDirection.x, 0, moveDirection.y));
                            initialPosition = Vector2.zero;
                        }
                    }
                }
            }
        }
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (side == 0 && touch.position.x < Screen.width / 2 || side == 1 && touch.position.x > Screen.width / 2)
                    {
                        initialPosition = touch.position;
                        touchId = touch.fingerId;
                    }
                }       

            }
        }
	
	}
}
