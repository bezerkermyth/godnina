using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Job", menuName = "Job")]
public class Job : ScriptableObject
{
    public string jobName;

    public int wage;

    public int time;
    public int energySpent;
    public float strength;
    public float respect;
    public float charm;
    public float karma;
    public float wisdom;
}
