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
    private GameObject blockDescription;
    private Text blockDescription_Text;
    public bool autoHideDescription;
    public float hideDescriptionDelay = 3.0f;
    private Text endEditor;
    private float hideDescriptionTimer = 0.0f;


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
        this.blockDescription = canvas.FindChild("BlockDescription").gameObject;
        if (this.blockDescription)
        {
            Transform text_go = this.blockDescription.transform.FindChild("Text");
            if (text_go)
            {
                this.blockDescription_Text = text_go.GetComponent<Text>();
                this.blockDescription.SetActive(false);
            }
        }
        Transform endEditor_go = canvas.FindChild("Play_Button");
        if (endEditor_go)
        {
            this.endEditor = endEditor_go.GetComponent<Text>();
        }

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Block"), LayerMask.NameToLayer("Block"), true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        switch (GameModeManager.Instance.mode)
        {
            case GameModeManager.Mode.SOLO:
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Block"), LayerMask.NameToLayer("Block"), false);
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player)
                {
                    player.transform.parent = null;
                    player.transform.localPosition = Vector3.zero;
                    player.transform.localRotation = Quaternion.identity;
                    player.transform.localScale = Vector3.one;
                    DontDestroyOnLoad comp = player.AddComponent<DontDestroyOnLoad>();
                    Application.LoadLevelAsync("Arena");
                    Destroy(comp, 1.0f);
                    //GameModeManager.Instance.SaveRobot(player);
                }
                break;
            case GameModeManager.Mode.MULTI:
                //TODO: save robot
                //GameModeManager.Instance.SaveRobot(null);
                Application.LoadLevelAsync(GameModeManager.editorSceneName);
                break;
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
            this.blockDescription.SetActive(false);
        }
        else
        {
            this.blockDescription_Text.text = description;
            this.blockDescription.SetActive(true);
            if (this.autoHideDescription)
            {
                if (this.hideDescriptionTimer == 0.0f)
                {
                    StartCoroutine(this.hideDescriptionIn(this.hideDescriptionDelay));
                }
                else
                {
                    this.hideDescriptionTimer = 0.0f;
                }
            }
        }
    }

    private IEnumerator hideDescriptionIn(float duration)
    {
        for (this.hideDescriptionTimer = 0; this.hideDescriptionTimer < duration; this.hideDescriptionTimer += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        this.blockDescription.SetActive(false);
    }
}
