using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Initialization, Load the manager into")]
    public Serializer tempPlayerObj;
    public ScreenManagerScript tempScreenManager;
    public LogScript tempLogScript;

    public bool getObj = false;
    private bool getScreen = false;
    private bool getLog = false;


    [Header("List of jobs")]
    public List<Job> jobList;

    private Job tempJob;

    //--------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------

    private void Update()
    {
        if (!getObj)
        {
            tempPlayerObj = GameObject.Find("PlayerStats(Clone)").GetComponent<Serializer>();
            if (tempPlayerObj != null)
            {
                getObj = true;
            }
        }

        if (!getScreen)
        {
            tempScreenManager = GameObject.Find("ScreenManager").GetComponent<ScreenManagerScript>();
            if (tempScreenManager != null)
            {
                getScreen = true;
            }
        }

        if (!getLog)
        {
            tempLogScript = GameObject.Find("LogManager").GetComponent<LogScript>();
            if (tempLogScript != null)
            {
                getLog = true;
            }
        }

    }

    //-the Method that add the job to player 
    //--------------------------------------------------------------------------------------
    public void AddJobPlayer(string jobName) {
        foreach (var jobObj in jobList)
        {
            if (jobObj.jobName == jobName) {
                tempJob = jobObj;
            }
        }
        tempPlayerObj.data.job = tempJob.name;
        tempScreenManager.jobTxt.text = tempJob.name;
        tempLogScript.AddLog("Now you are a <color=lime>" + tempJob.name+"</color>");
    }

    //-the method that work
    //--------------------------------------------------------------------------------------
    public void WorkJob() {
        if (tempPlayerObj.data.job == "Jobless" || tempPlayerObj == null)
        {
            tempLogScript.AddLog("<color=red>You Dont Have a Job</color>");
        }
        else {
            if ((4 - tempPlayerObj.data.daytime) < tempJob.time)
            {
                tempLogScript.AddLog("<color=red>You dont have enought time</color>");
            }
            else if ((tempPlayerObj.data.energy+1) < tempJob.energySpent)
            {
                tempLogScript.AddLog("<color=red>You dont have enought energy</color>");
            }
            else {
                tempPlayerObj.data.gold += tempJob.wage;
                tempPlayerObj.data.daytime += tempJob.time;
                if (tempPlayerObj.data.daytime > 3)
                {
                    TurnDay();
                }
                tempPlayerObj.data.energy -= tempJob.energySpent;
                tempPlayerObj.data.strength += tempJob.strength;
                tempPlayerObj.data.respect += tempJob.respect;
                tempPlayerObj.data.charm += tempJob.charm;
                tempPlayerObj.data.karma += tempJob.wisdom;
                tempPlayerObj.data.wisdom += tempJob.wisdom;
                tempLogScript.AddLog("You work hard and gain <color=yellow>" + tempJob.wage.ToString() + "</color> Gold");
                tempScreenManager.UpdateValuesScreen();
            }
            
        }
    }
    //-this method turn day
    //--------------------------------------------------------------------------------------
    public void TurnDay() {
        tempPlayerObj.data.dayNumber += 1;
        tempPlayerObj.data.daytime = 0;
        if (tempPlayerObj.data.dayNumber > 30) {
            tempPlayerObj.data.season += 1;
            if (tempPlayerObj.data.season > 3) {
                tempPlayerObj.data.season = 0;
            }
        }
        tempScreenManager.UpdateValuesScreen();
  
    }

    //--------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------


    //end Line
    //--------------------------------------------------------------------------------------
}
