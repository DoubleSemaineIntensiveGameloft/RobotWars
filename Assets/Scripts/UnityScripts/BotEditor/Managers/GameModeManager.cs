using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModeManager : MonoBehaviour
{
    private static GameModeManager instance;
    public static GameModeManager Instance
    {
        get { return GameModeManager.instance; }
    }
    public static string editorSceneName = "MainScene";
    public enum Mode { SOLO, MULTI };
    public Mode mode = Mode.MULTI;

    void Awake()
    {
        if (GameModeManager.instance == null)
        {
            GameModeManager.instance = this;
        }
    }

    public void startSingleMode()
    {
        this.mode = Mode.SOLO;
        this.goToEditor();
    }

    public void startMultiMode()
    {
        this.mode = Mode.MULTI;
        this.goToEditor();
    }

    public void goToEditor()
    {
        Application.LoadLevelAsync("MainScene");
    }
}
