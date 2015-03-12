using UnityEngine;
using System.Collections;

public class ManagerGame : MonoBehaviour {


    public static ManagerGame instance;

    public GameObject[] bots;
	// Use this for initialization
	void Start () {

        instance = this;

        bots = GameObject.FindGameObjectsWithTag("Bot");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
