using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Person
{
    public Person()
    {
        name = "name";
        Coins = 0;
        carrots = new List<CarrotData>();
        InterestRate = 1;
        GrowthInMin = 960;
    }
    public string name;
    public int Coins;
    public List<CarrotData> carrots = new List<CarrotData>();
    public int InterestRate, GrowthInMin;
}

[Serializable]
public class CarrotData
{
    public CarrotData(float time, DateTime datePlanted, int interestRateWhenPlanted)
    {
        growthTimeSeconds = time;
        plantDate = datePlanted;
        this.interestRateWhenPlanted = interestRateWhenPlanted;
    }
    public DateTime plantDate;
    public int row;
    public float growthTimeSeconds;
    public int interestRateWhenPlanted;
    public bool isGrowing;

    public void DisplayInfo()
    {
        Debug.Log(
                 $"Planted: {plantDate.ToString("yyyy-MM-dd HH:mm")}");
    }

    // Method to calculate age of carrot in days
    public int GetAgeInMins()
    {
        TimeSpan age = DateTime.Now - plantDate;
        return age.Minutes;
    }
}



[Serializable]
public class GameData
{
    public List<Person> people = new List<Person>();
    
    public int score;
    public string lastSaveTime;
    // Add other game data as needed
}