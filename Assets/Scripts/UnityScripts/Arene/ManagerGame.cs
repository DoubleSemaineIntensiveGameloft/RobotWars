using UnityEngine;
using System.Collections;

public class ManagerGame : MonoBehaviour {


    public static ManagerGame instance;

    public GameObject[] bots;

    public GameObject prefabBot;

    public GameObject[] positionsStart;

    public MoveSlide[] moveControllers;

    GameObject player1;
    GameObject player2;
	// Use this for initialization
	void Start () {

        instance = this;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int index = 0;

        player1 = players[0];
        player2 = Instantiate(players[0], players[0].transform.position, Quaternion.identity) as GameObject;

        //player1.transform.position = positionsStart[0].transform.position;

        GameObject newBot = Instantiate(prefabBot, positionsStart[index].transform.position, Quaternion.identity) as GameObject;
        player1.transform.parent = newBot.transform;
        player1.transform.localPosition = Vector3.zero;
        moveControllers[0].botControlled = newBot.GetComponent<Bot>();

        moveControllers[0].botControlled.startPosition = positionsStart[0].transform.position;

        newBot = Instantiate(prefabBot, positionsStart[index + 1].transform.position, Quaternion.identity) as GameObject;
        player2.transform.parent = newBot.transform;
        player2.transform.localPosition = Vector3.zero;
        moveControllers[1].botControlled = newBot.GetComponent<Bot>();

        moveControllers[1].botControlled.startPosition = positionsStart[index + 1].transform.position;


        //foreach (GameObject player in players)
        /*
        for (int i = 0; i < 2; i++)
        {
            GameObject newBot = Instantiate(prefabBot, positionsStart[index].transform.position, Quaternion.identity) as GameObject;
            players[0].transform.parent = newBot.transform;
            index++;
        }*/

        bots = GameObject.FindGameObjectsWithTag("Bot");

        GameObject[] anchors = GameObject.FindGameObjectsWithTag("Anchor");

        for (int i = 0; i < anchors.Length; i++)
        {
            if (anchors[i].GetComponentInChildren<Block>())
            { 
                anchors[i].GetComponentInChildren<Block>().transform.parent = anchors[i].transform.parent;
            }

            Destroy(anchors[i]);
        }

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        player1.GetComponentInParent<Bot>().Restart();
        player2.GetComponentInParent<Bot>().Restart();
    }
}
