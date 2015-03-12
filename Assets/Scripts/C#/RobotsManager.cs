using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotsManager
{
    public int maxRobotsCount = 2;
    private List<Robot> robots;

    public RobotsManager()
    {
        this.robots = new List<Robot>();
    }

    public void addRobotSlot()
    {
        this.maxRobotsCount++;
    }

    public void addRobot(Robot robot)
    {
        if (this.robots.Count < this.maxRobotsCount)
        {
            this.robots.Add(robot);
        }
        else
        {
            GUIManager.Instance.displayMessage("Max robot count reached");
        }
    }

    public void removeRobot(Robot robot)
    {
        this.robots.Remove(robot);
    }


    public int getRobotsCount()
    {
        return this.robots.Count;
    }
}
