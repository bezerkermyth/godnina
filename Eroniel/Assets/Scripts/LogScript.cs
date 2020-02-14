using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour
{

    public static LogScript LogInstance = null;

    public Text log1;
    public Text log2;
    public Text log3;
    public Text log4;
    public Text log5;
    public Text log6;
    public Text log7;
    public Text log8;
    public Text log9;
    public Text log10;


    private int index=0;
    private int indexTemp = 0;
    private int[] listOrder = new int[10];
    private string[] logText = new string[10];


    //----------------------------------------------------------------------
    public void AddLog2()
    {
        string textToAdd = " this is index :" + index.ToString();
        
            logText[index] = textToAdd;
            index++;
           
            if (index > logText.Length-1)
            {
                index = 0;
            }
            SetOrderListLog();
            FeedLog();
        
    }
    //----------------------------------------------------------------------

    public void AddLog(string textToAdd) {

        logText[index] = textToAdd;
        index++;

        if (index > logText.Length - 1)
        {
            index = 0;
        }
        SetOrderListLog();
        FeedLog();

    }

    //----------------------------------------------------------------------

    private void SetOrderListLog() {
        indexTemp = index-1;
        if (indexTemp < 0) {
            indexTemp = logText.Length-1;
        }
        for (int i = 0; i < logText.Length; i++)
        {
            
            if (indexTemp >= 0) {
                listOrder[i] = indexTemp;
                indexTemp--;
            }
            if (indexTemp < 0)
            {
                indexTemp = logText.Length-1;
            }
        }
    }

    //----------------------------------------------------------------------
    public void FeedLog() {
        log1.text = logText[listOrder[9]];
        log2.text = logText[listOrder[8]];
        log3.text = logText[listOrder[7]];
        log4.text = logText[listOrder[6]];
        log5.text = logText[listOrder[5]];
        log6.text = logText[listOrder[4]];
        log7.text = logText[listOrder[3]];
        log8.text = logText[listOrder[2]];
        log9.text = logText[listOrder[1]];
        log10.text = logText[listOrder[0]];
        
    }


    //----------------------------------------------------------------------
    private void Awake()
    {
        ClearAllLog();

    }

    //----------------------------------------------------------------------
    public void ClearAllLog() {
        log1.text = "";
        log2.text = "";
        log3.text = "";
        log4.text = "";
        log5.text = "";
        log6.text = "";
        log7.text = "";
        log8.text = "";
        log9.text = "";
        log10.text = "";

        for (int i = 0; i < 10; i++)
        {
            logText[i] = "";
        }
    }

    //----------------------------------------------------------------------
  

    //----------------------------------------------------------------------

}
