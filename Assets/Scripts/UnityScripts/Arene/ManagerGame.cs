using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour {


    public static ManagerGame instance;

    public GameObject[] bots;

    public GameObject prefabBot;

    public GameObject[] positionsStart;

    public MoveSlide[] moveControllers;

    public GameObject player1;
    public GameObject player2;

    public int tutoStatus = 0;

    public GameObject limiter;
    public Image limiterImage;

    public bool hideLimiter = false;

    public float actTimeLimiter = 0;
    public float timeLimiter = 5;

    public Gradient limiterColor;
	// Use this for initialization
	void Start () {

        instance = this;

        tutoStatus = 0;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int index = 0;

        player1 = players[0];
        player2 = players[1];
        
        //player2 = Instantiate(players[0], players[0].transform.position, Quaternion.identity) as GameObject;

        //player1.transform.position = positionsStart[0].transform.position;

        GameObject newBot = Instantiate(prefabBot, positionsStart[index].transform.position, positionsStart[index].transform.rotation) as GameObject;
        player1.transform.parent = newBot.transform;
        player1.transform.localPosition = Vector3.zero;
        moveControllers[0].botControlled = newBot.GetComponent<Bot>();

        player1.transform.rotation = positionsStart[index].transform.rotation;

        moveControllers[0].botControlled.startPosition = positionsStart[0].transform.position;

        newBot = Instantiate(prefabBot, positionsStart[index + 1].transform.position, positionsStart[index + 1].transform.rotation) as GameObject;
        player2.transform.parent = newBot.transform;
        player2.transform.localPosition = Vector3.zero;
        moveControllers[1].botControlled = newBot.GetComponent<Bot>();

        player2.transform.rotation = positionsStart[index + 1].transform.rotation;

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

        limiterImage = limiter.GetComponent<Image>();

	
	}
	
	// Update is called once per frame
	void Update () {

        if(hideLimiter && actTimeLimiter < timeLimiter)
        {
            actTimeLimiter += Time.deltaTime;
            limiterImage.color = limiterColor.Evaluate(actTimeLimiter / timeLimiter);
        }
	
	}

    public void CheckTuto()
    {
        tutoStatus++;
        if(tutoStatus >= 2)
        {
            // Destroy Limitter
            hideLimiter = true;

        }
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        player1.GetComponentInParent<Bot>().Restart();
        player2.GetComponentInParent<Bot>().Restart();
    }

    public void Garage()
    {
        Application.LoadLevel("MainScene");
    }
}
