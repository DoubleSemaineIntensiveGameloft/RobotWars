using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    private float torsoSelectorDelay = 5.0f;
    private GameObject torsoSelector;
    private GameObject skinSelector;

    public GameObject parentNewBotTo;
    private bool firstPass;

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

        this.torsoSelector = canvas.FindChild("TorsoSelector").gameObject;
        this.skinSelector = this.torsoSelector.transform.FindChild("SkinPanel").gameObject;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Block"), LayerMask.NameToLayer("Block"), true);

        StartCoroutine(this.showTorsoSelector());

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void init()
    {
        this.firstPass = true;
    }

    public void startGame()
    {
        switch (GameModeManager.Instance.mode)
        {
            case GameModeManager.Mode.SOLO:
                this.goToArena();
                //GameObject player = GameObject.FindGameObjectWithTag("Player");
                //if (player)
                //{
                //    player.transform.parent = null;
                //    player.transform.localPosition = Vector3.zero;
                //    player.transform.localRotation = Quaternion.identity;
                //    player.transform.localScale = Vector3.one;

                //}
                break;
            case GameModeManager.Mode.MULTI:
                if (this.firstPass)
                {
                    Application.LoadLevelAsync(GameModeManager.editorSceneName);
                    this.firstPass = false;
                }
                else
                {
                    this.goToArena();
                }
                break;
        }
    }

    public void goToArena()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Block"), LayerMask.NameToLayer("Block"), false);
        Application.LoadLevelAsync("Arena");
    }

    private void resetPositions()
    {
        Robot[] bots = RobotsManager.Instance.getRobots();
        for (int i = 0; i < bots.Length; i++)
        {

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

    private IEnumerator showTorsoSelector()
    {
        yield return new WaitForSeconds(this.torsoSelectorDelay);
        this.torsoSelector.SetActive(true);
    }


    private void setupSkinSelection(Robot robot)
    {
        if (robot == null)
        {
            Debug.LogError("No robot !!!");
            return;
        }
        GameObject skin0 = this.skinSelector.transform.FindChild("Skin_Base").gameObject;
        skin0.transform.FindChild("Icon").GetComponent<Image>().sprite = robot.getIcon("base");
        Button b0 = skin0.transform.Find("Choose").GetComponent<Button>();
        b0.onClick.RemoveAllListeners();
        b0.onClick.AddListener(() => { RobotsManager.Instance.applySkin("base"); this.torsoSelector.SetActive(false); });
        //foreach (Button b in skin0.GetComponentsInChildren<Button>())
        //{
        //    b.onClick.RemoveAllListeners();
        //    b.onClick.AddListener(() => { RobotsManager.Instance.applySkin("base"); this.setSkinVisibility(false); });
        //}


        GameObject skin1 = this.skinSelector.transform.FindChild("Skin_Alternative").gameObject;
        skin1.transform.FindChild("Icon").GetComponent<Image>().sprite = robot.getIcon("alternative");
        foreach (Button b in skin1.GetComponentsInChildren<Button>())
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() => { RobotsManager.Instance.applySkin("alternative"); this.torsoSelector.SetActive(false); });
        }
    }

    public void setupRobot(Robot robot)
    {
        if (robot)
        {
            if (RobotsManager.Instance.saveRobot(robot))
            {
                GameObject go = GameObject.Find("Robot");
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in go.transform)
                {
                    children.Add(child.gameObject);
                }
                children.ForEach(child => Destroy(child));

                GameObject newBot = Instantiate(robot.gameObject);
                newBot.transform.parent = parentNewBotTo.transform;
                newBot.transform.localPosition = Vector3.zero;
                newBot.transform.localRotation = Quaternion.identity;
                newBot.transform.localScale = Vector3.one;
            }
            else
            {
                Debug.Log("Save error");
            }
            this.setupSkinSelection(robot);
            this.setSkinVisibility(true);
        }
    }

    public void setSkinVisibility(bool visibility)
    {
        this.skinSelector.SetActive(visibility);
    }
}
