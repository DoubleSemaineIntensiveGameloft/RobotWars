using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotsManager : MonoBehaviour
{
    private static RobotsManager instance;
    public static RobotsManager Instance
    {
        get
        {
            return RobotsManager.instance;
        }
    }
    public int maxRobotsCount = 2;
    private List<Robot> privateRobots = new List<Robot>();
    private List<Robot> localBattleRobots = new List<Robot>();

    private int currentIndex = -1;

    void Awake()
    {
        if (RobotsManager.instance == null)
        {
            RobotsManager.instance = this;
        }
    }

    public bool saveRobot(Robot robot)
    {
        switch (GameModeManager.Instance.mode)
        {
            case GameModeManager.Mode.SOLO:
                return this.savePrivateRobot(robot);
            case GameModeManager.Mode.MULTI:
                return this.saveMultiRobot(robot);
            default:
                return false;
        }
    }

    public Robot[] getRobots()
    {
        switch (GameModeManager.Instance.mode)
        {
            case GameModeManager.Mode.SOLO:
                return this.getSoloRobots();
            case GameModeManager.Mode.MULTI:
                return this.getMultiRobots();
            default:
                return null;
        }
    }

    public void applySkin(string skinName)
    {
        this.getCurrentRobot().applySkin(skinName);
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            MouseLook ml = player.GetComponent<MouseLook>();
            if (ml)
            {
                Debug.Log("mouse look activated");
                ml.enabled = true;
            }
        }
    }

    public Robot getRobot(int index)
    {
        switch (GameModeManager.Instance.mode)
        {
            case GameModeManager.Mode.SOLO:
                return this.privateRobots[index];
            case GameModeManager.Mode.MULTI:
                return this.localBattleRobots[index];
            default:
                return null;
        }
    }

    //public void selectRobot(int index)
    //{
    //    this.currentIndex = index;
    //    switch (GameModeManager.Instance.mode)
    //    {
    //        case GameModeManager.Mode.SOLO:
    //            this.clampPrivateIndex();
    //            break;
    //        case GameModeManager.Mode.MULTI:
    //            this.clearMultiRobots();
    //            break;
    //        default:
    //            Debug.Log("Unknown Mode");
    //            break;
    //    }
    //}

    public Robot getCurrentRobot()
    {
        return this.getRobot(this.currentIndex);
    }

    private void clampPrivateIndex()
    {
        if (this.currentIndex < 0)
        {
            this.currentIndex = 0;
        }
        else if (this.currentIndex >= this.privateRobots.Count)
        {
            this.currentIndex = this.privateRobots.Count - 1;
        }
    }

    private void clampMultiIndex()
    {
        if (this.currentIndex < 0)
        {
            this.currentIndex = 0;
        }
        else if (this.currentIndex >= this.localBattleRobots.Count)
        {
            this.currentIndex = this.localBattleRobots.Count - 1;
        }
    }

    #region "Private robots"
    public void addRobotSlot()
    {
        this.maxRobotsCount++;
    }

    private bool savePrivateRobot(Robot robot)
    {
        if (this.privateRobots.Count < this.maxRobotsCount)
        {
            this.privateRobots.Add(robot);
            this.currentIndex = this.privateRobots.Count - 1;
            return true;
        }
        else
        {
            GUIManager.Instance.displayMessage("Max robot count reached");
            return false;
        }

    }

    private void deleteRobot(Robot robot)
    {
        this.privateRobots.Remove(robot);
    }

    private void removeAllRobots()
    {
        this.privateRobots.Clear();
    }


    private int getRobotsCount()
    {
        return this.privateRobots.Count;
    }

    private Robot[] getSoloRobots()
    {
        Robot[] bots = new Robot[this.privateRobots.Count];
        for (int i = 0; i < this.privateRobots.Count; i++)
        {
            bots[i] = this.privateRobots[i];
        }
        return bots;
    }

    #endregion

    #region "Local battles"
    private bool saveMultiRobot(Robot robot)
    {
        if (this.localBattleRobots.Count <= 1)
        {
            this.localBattleRobots.Add(robot);
            robot.gameObject.AddComponent<DontDestroyOnLoad>();
            this.currentIndex = this.localBattleRobots.Count - 1;
            Debug.Log("Save multi, index : " + this.currentIndex);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void clearMultiRobots()
    {
        this.localBattleRobots.Clear();
    }

    private Robot[] getMultiRobots()
    {
        Robot[] bots = new Robot[this.localBattleRobots.Count];
        for (int i = 0; i < bots.Length; i++)
        {
            bots[i] = this.localBattleRobots[i];
        }
        return bots;
    }
    #endregion
}
