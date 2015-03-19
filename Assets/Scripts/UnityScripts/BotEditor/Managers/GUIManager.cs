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
    //private Text endEditor;
    private float hideDescriptionTimer = 0.0f;
    private GameObject torsoSelector;
    private GameObject skinSelector;
    private GameObject endEditorButton;

    //public GameObject parentNewBotTo;
    private static bool firstPass = true;

    void Awake()
    {
        // if (GUIManager.instance == null)
        // {
        GUIManager.instance = this;
        //}
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
        this.endEditorButton = canvas.FindChild("Play_Button").gameObject;
        //if (endEditorButton)
        //{
        //    this.endEditor = endEditorButton.GetComponent<Text>();
        //}
        endEditorButton.SetActive(false);

        this.torsoSelector = canvas.FindChild("TorsoSelector").gameObject;
        this.skinSelector = this.torsoSelector.transform.FindChild("SkinPanel").gameObject;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Block"), LayerMask.NameToLayer("Block"), true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init()
    {
        RobotsManager.Instance.removeAllRobots();
        firstPass = true;
    }

    public void startGame()
    {

        switch (GameModeManager.Instance.mode)
        {
            case GameModeManager.Mode.SOLO:
                {
                    this.goToArena();
                    break;
                }
            case GameModeManager.Mode.MULTI:
                {
                    if (firstPass)
                    {
                        firstPass = false;
                        GameObject bot = RobotsManager.Instance.getCurrentRobot().gameObject;
                        bot.transform.parent = null;
                        bot.SetActive(false);
                        Application.LoadLevelAsync(Application.loadedLevelName);
                    }
                    else
                    {
                        this.goToArena();
                    }
                    break;
                }
        }
    }

    public void goToArena()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Block"), LayerMask.NameToLayer("Block"), false);
        this.resetPositions();
        Application.LoadLevelAsync("Arena");
    }

    private void resetPositions()
    {
        Robot[] bots = RobotsManager.Instance.getRobots();
        Debug.Log("Bots count : " + bots.Length);
        for (int i = 0; i < bots.Length; i++)
        {
            GameObject bot = bots[i].gameObject;
            bot.transform.parent = null;
            bot.transform.localPosition = Vector3.zero;
            bot.transform.localRotation = Quaternion.identity;
            bot.transform.localScale = Vector3.one;
            bot.SetActive(true);
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

    private void startLightsAnim()
    {
        GameObject background = GameObject.Find("Background");
        Animator anim = background.GetComponent<Animator>();
        anim.SetTrigger("lightsOn");
        //this.torsoSelector.SetActive(true);
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
        b0.onClick.AddListener(() => { RobotsManager.Instance.applySkin("base"); this.torsoSelector.SetActive(false); this.startLightsAnim(); });

        GameObject skin1 = this.skinSelector.transform.FindChild("Skin_Alternative").gameObject;
        skin1.transform.FindChild("Icon").GetComponent<Image>().sprite = robot.getIcon("alternative");
        Button b1 = skin1.transform.Find("Choose").GetComponent<Button>();
        b1.onClick.AddListener(() => { RobotsManager.Instance.applySkin("alternative"); this.torsoSelector.SetActive(false); this.startLightsAnim(); });
    }

    public void setupRobot(Robot robot)
    {
        if (robot)
        {
            GameObject bot_go = Instantiate<GameObject>(robot.gameObject);
            Robot bot = bot_go.GetComponent<Robot>();
            if (RobotsManager.Instance.saveRobot(bot))
            {
                GameObject go = GameObject.Find("Robot");
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in go.transform)
                {
                    children.Add(child.gameObject);
                }
                children.ForEach(child => Destroy(child));

                bot_go.transform.parent = go.transform;
                bot_go.transform.localPosition = Vector3.zero;
                bot_go.transform.localRotation = Quaternion.identity;
                bot_go.transform.localScale = Vector3.one;
            }
            else
            {
                Destroy(bot_go);
                Debug.Log("Save error");
            }
            this.setupSkinSelection(robot);
            this.setSkinVisibility(true);
            this.endEditorButton.SetActive(true);
        }
    }

    public void setSkinVisibility(bool visibility)
    {
        this.skinSelector.SetActive(visibility);
    }
}
