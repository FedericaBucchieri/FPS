using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneFlowManager
{
    public static string NextScene { get; set; }
    public static List<char> testCondition = new List<char>();
    public static char currentCondition = 'z';
    public static int LevelPlayed = 0;
    public static int numberOfTrials = 0;


    public static char getNextCondition()
    {

        Debug.Log("Change condition");

        if (currentCondition.Equals('z'))
        {
            currentCondition = testCondition[0];

        }
        else
        {
            int index = testCondition.IndexOf(currentCondition);
            if (index + 1 >= testCondition.Count)
                currentCondition = 'e';
            else 
                currentCondition = testCondition[index + 1];
        }

        return currentCondition;
    }

    public static bool needUpdateCondition()
    {
        if (LevelPlayed == numberOfTrials)
            return true;
        else
            return false;
    }
}
