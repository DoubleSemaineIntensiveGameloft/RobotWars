using UnityEngine;
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

    void Awake()
    {
        if (GUIManager.instance == null)
        {
            GUIManager.instance = this;
        }
    }

    void Start()
    {

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
}
