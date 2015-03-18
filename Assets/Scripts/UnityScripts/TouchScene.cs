using UnityEngine;
using System.Collections;

public class TouchScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        foreach (Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Ended)
            {
                Application.LoadLevel("MainScene");
            }
        }
	
	}
}
