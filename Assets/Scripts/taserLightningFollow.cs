using UnityEngine;
using System.Collections;

public class taserLightningFollow : MonoBehaviour {

	public Transform follow;
	
	void FixedUpdate () {

		transform.position = follow.position;
	
	}
}
