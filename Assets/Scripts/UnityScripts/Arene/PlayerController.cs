using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Bot playerBot;

	void Start () {
		playerBot = GetComponent<Bot> ();
	}

	void Update () {
	

		// Move
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			playerBot.Move (new Vector3 (Input.GetAxis ("Horizontal"), 0,  Input.GetAxis ("Vertical")));
		}
	

	}
}
