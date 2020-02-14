using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManagerScript : MonoBehaviour
{

    public static ScreenManagerScript Screeninstance = null;

    [Header("Func Painel")]
    public Text jobTxt;
    public Text unmarriedTxt;
    public Text homeTxt;
    public Text statusTxt;

    [Header("Stats")]
    public Text health;
    public Text energy;
    public Text hunger;
    public Text thirst;
    public Text age;

    [Header("Skill")]
    public Text strenght;
    public Text respect;
    public Text charm;
    public Text karma;
    public Text wisdom;

    [Header("Command")]
    public Text gold;
    public Text daytime;
    public Text day;
    public Text season;

    public bool getObj=false;
    public Serializer tempPlayerObj;
    public string tempGoldText;

    private string daytimeTemp;
    private string seasonTemp;
    private Color tempCol;
    private Color healthCol;
    private Color energyCol;
    private Color thirsthCol;
    private Color hungerCol;
    private Color[] statsColorTemp = new Color[4];
    private Color[] statsColor = new Color[4];
    private int[] statusValuesCol = new int[4];

    //----------------------------------------------------------
    void Awake()
    {
        statsColorTemp[0] = health.color;
        statsColorTemp[1] = energy.color;
        statsColorTemp[2] = hunger.color;
        statsColorTemp[3] = thirst.color;

    }


    //----------------------------------------------------------
    public void Update()
    {
        
        if (!getObj) {
            tempPlayerObj = GameObject.Find("PlayerStats(Clone)").GetComponent<Serializer>();
            if (tempPlayerObj != null) {
                getObj = true;
               
            }
            
        }
    }

    //----------------------------------------------------------

    public void UpdateValuesScreen() {
        tempPlayerObj = GameObject.Find("PlayerStats(Clone)").GetComponent<Serializer>();
        jobTxt.text = tempPlayerObj.data.job.ToString();
        unmarriedTxt.text = tempPlayerObj.data.relationship.ToString();
        homeTxt.text = tempPlayerObj.data.home.ToString();
        statusTxt.text = tempPlayerObj.data.title.ToString();

        ChangeColorStats();

        health.color = statsColor[0];
        health.text = tempPlayerObj.data.health.ToString();
        energy.color = statsColor[1];
        energy.text = tempPlayerObj.data.energy.ToString();
        hunger.color = statsColor[2];
        hunger.text = tempPlayerObj.data.hunger.ToString();
        thirst.color = statsColor[3];
        thirst.text = tempPlayerObj.data.thirst.ToString();
        age.text = tempPlayerObj.data.age.ToString();

        strenght.text = tempPlayerObj.data.strength.ToString(); 
        respect.text = tempPlayerObj.data.respect.ToString();
        charm.text = tempPlayerObj.data.charm.ToString();
        karma.text = tempPlayerObj.data.karma.ToString();
        wisdom.text = tempPlayerObj.data.wisdom.ToString();

        tempCol = gold.color;
        gold.color = tempCol;
        gold.text = tempPlayerObj.data.gold.ToString();

        InputDaytime(tempPlayerObj.data.daytime);
        daytime.text = daytimeTemp;
        day.text = tempPlayerObj.data.dayNumber.ToString();
        InputSeason(tempPlayerObj.data.season);
        season.text = seasonTemp;

    }

    //----------------------------------------------------------

    public void InputDaytime(int daytimeNumber) {
        switch (daytimeNumber) {
            case 0:daytimeTemp = "Morning";
                break;
            case 1:daytimeTemp = "Noon";
                break;
            case 2:
                daytimeTemp = "Afternoon";
                break;
            case 3:
                daytimeTemp = "Night";
                break;
            default:daytimeTemp = "not find";
                break;
        }
    }

    //----------------------------------------------------------


    public void InputSeason(int seasonNumber)
    {
        switch (seasonNumber)
        {
            case 0:
                seasonTemp = "Spring";
                break;
            case 1:
                seasonTemp = "Summer";
                break;
            case 2:
                seasonTemp = "Autum";
                break;
            case 3:
                seasonTemp = "Winter";
                break;
            default:
                seasonTemp = "not find";
                break;
        }
    }



    //----------------------------------------------------------

    public void ChangeColorStats() {

        statusValuesCol[0] = tempPlayerObj.data.health;
        statusValuesCol[1] = tempPlayerObj.data.energy;
        statusValuesCol[2] = tempPlayerObj.data.hunger;
        statusValuesCol[3] = tempPlayerObj.data.thirst;

        for (int i = 0; i < statusValuesCol.Length; i++)
        {
            if (statusValuesCol[i] < 50 && statusValuesCol[i] > 30)
            {
                statsColor[i] = Color.yellow;
            }
            else if (statusValuesCol[i] < 30 &&statusValuesCol[i]>0)
            {
                statsColor[i] = Color.red;
            }
            else {
                statsColor[i] = statsColorTemp[i];
            }
        }
    }

    //----------------------------------------------------------

}
