using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Header("Stats")]
    public int health;
    public int energy;
    public int hunger;
    public int thirst;
    public int age;
   
    [Header("Skill")]
    public float strength;
    public float respect;
    public float charm;
    public float karma;
    public float wisdom;

    [Header("Titles")]
    public string job;
    public string relationship;
    public string home;
    public string title;

    [Header("Possessions")]
    public int gold;
    public List<Item> inventory;
    public List<Item> inventoryHome;

    [Header("Lover")]
    public string loverName;
    public string statusPreg;

    [Header("World")]
    public int daytime;
    public int dayNumber;
    public int season;

}



