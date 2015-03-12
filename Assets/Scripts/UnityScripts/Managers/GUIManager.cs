using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{

    private static GUIManager instance;
    public static GUIManager Instance
    {
        get
        {
            return GUIManager.instance;
        }
    }

    private GameObject messageDisplayer;

    void Awake()
    {
        if (GUIManager.instance == null)
        {
            GUIManager.instance = this;
        }
    }

    void Start()
    {
        Transform canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        this.messageDisplayer = canvas.FindChild("MessageDisplayer").gameObject;
        this.messageDisplayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            DontDestroyOnLoad comp = player.AddComponent<DontDestroyOnLoad>();
            Application.LoadLevelAsync("Arena");
            Destroy(comp);
        }

    }

    public void displayMessage(string message)
    {
        this.displayMessage(message, 1.0f);
    }

    public void displayMessage(string message, float duration)
    {
        StartCoroutine(this.displayMsg(message, duration));
    }

    private IEnumerator displayMsg(string message, float duration)
    {
        Text txt = this.messageDisplayer.transform.FindChild("Text").GetComponent<Text>();
        txt.text = message;
        this.messageDisplayer.SetActive(true);
        yield return new WaitForSeconds(duration);
        this.messageDisplayer.SetActive(false);
    }
}
