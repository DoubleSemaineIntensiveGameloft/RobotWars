using UnityEngine;
using System.Collections;

public class LoaderScene : MonoBehaviour {

    public string levelName;

	// Use this for initialization
	void Start () {
        Application.LoadLevel(levelName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
