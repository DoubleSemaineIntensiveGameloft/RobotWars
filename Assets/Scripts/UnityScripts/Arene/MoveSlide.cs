using UnityEngine;
using System.Collections;

public class MoveSlide : MonoBehaviour {

    public Bot botControlled;

    public int side = 0;

    public Vector2 initialPosition = Vector2.zero;
    private int touchId = 0;

    public GameObject trailCursor;

    public bool tutoChecked = false;
    public Animator slideTutoAnimator;

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
                        if(Vector2.Distance(initialPosition, touch.position) > 10)
                        {
                            // 10 > 0.5 -  180 > 1.5
                            // Move
                            Vector2 moveDirection = (touch.position - initialPosition).normalized;

                            //Debug.Log("distance:" + Vector2.Distance(initialPosition, touch.position));

                            //float speed = Vector2.Distance(initialPosition, touch.position) * 1.5f / 100;

                            botControlled.Move(new Vector3(moveDirection.x, 0, moveDirection.y)); // , speed
                            initialPosition = Vector2.zero;

                            if (!tutoChecked && CameraGame.instance.cameraState > 0)
                            {
                                tutoChecked = true;
                                slideTutoAnimator.SetTrigger("AnimOut");
                                ManagerGame.instance.CheckTuto();
                            }
                        }
                        else if (Vector2.Distance(initialPosition, touch.position) < 4)
                        {
                            botControlled.Move(Vector3.zero);
                            initialPosition = Vector2.zero;
                        }
                    }
                }
                    /*
                else if(touch.phase == TouchPhase.Moved)
                {
                    if(trailCursor)
                    {
                        trailCursor.transform.position = touch.position;
                    }
                }*/
            }
        }
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("Screen.width:" + Screen.width + "  touch.position:" + touch.position);

                    if ((side == 0 && touch.position.x < Screen.width / 2 && touch.position.x > 100) || side == 1 && touch.position.x > Screen.width / 2 && touch.position.x < Screen.width - 100)
                    {
                        initialPosition = touch.position;
                        touchId = touch.fingerId;
                    }
                }       

            }
        }
	
	}
}
