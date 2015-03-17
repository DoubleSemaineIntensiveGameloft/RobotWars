using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotsManager
{
    private static RobotsManager instance;
    public static RobotsManager Instance
    {
        get
        {
            if (RobotsManager.instance == null)
            {
                RobotsManager.instance = new RobotsManager();
            }
            return RobotsManager.instance;
        }
    }
    public int maxRobotsCount = 2;
    private List<Robot> privateRobots;
    private List<Robot> localBattleRobots;

    private int currentIndex = -1;

    private RobotsManager()
    {
        this.privateRobots = new List<Robot>();
        //TODO: load private robots
        this.localBattleRobots = new List<Robot>();
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

    #region "Private robots"
    public void addRobotSlot()
    {
        this.maxRobotsCount++;
    }

    public bool savePrivateRobot(Robot robot)
    {
        if (this.privateRobots.Count < this.maxRobotsCount)
        {
            this.privateRobots.Add(robot);
            return true;
        }
        else
        {
            GUIManager.Instance.displayMessage("Max robot count reached");
            return false;
        }
    }

    public void deleteRobot(Robot robot)
    {
        this.privateRobots.Remove(robot);
    }

    public void removeAllRobots()
    {
        this.privateRobots.Clear();
    }


    public int getRobotsCount()
    {
        return this.privateRobots.Count;
    }

    public Robot getRobot(int index)
    {
        return this.privateRobots[index];
    }
    #endregion

    #region "Local battles"
    private bool saveMultiRobot(Robot robot)
    {
        if (this.localBattleRobots.Count <= 1)
        {
            this.localBattleRobots.Add(robot);
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

    public Robot[] getMultiRobots()
    {
        Robot[] bots = new Robot[2];
        for (int i = 0; i < this.localBattleRobots.Count; i++)
        {
            bots[i] = this.localBattleRobots[i];
        }
        return bots;
    }
    #endregion
}
