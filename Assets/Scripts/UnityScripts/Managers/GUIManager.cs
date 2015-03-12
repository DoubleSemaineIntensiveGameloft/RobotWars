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
    private Text blockDescription;
    public bool autoHideDescription;
    public float hideDescriptionDelay = 3.0f;

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
        Transform description_go = canvas.FindChild("BlockDescription");
        if (description_go)
        {
            this.blockDescription = description_go.GetComponent<Text>();
            description_go.gameObject.SetActive(false);
        }
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

    public void setBlockDescription(string description)
    {
        if (description == null || description.Equals(""))
        {
            this.blockDescription.gameObject.SetActive(false);
        }
        else
        {
            this.blockDescription.text = description;
            this.blockDescription.gameObject.SetActive(true);
            if (this.autoHideDescription)
            {
                StartCoroutine(this.hideDescriptionIn(this.hideDescriptionDelay));
            }
        }
    }

    private IEnumerator hideDescriptionIn(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.blockDescription.gameObject.SetActive(false);
    }
}
