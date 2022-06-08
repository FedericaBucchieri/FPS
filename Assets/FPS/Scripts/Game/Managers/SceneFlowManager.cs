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
        if (currentCondition.Equals('z'))
        {
            Debug.Log("prima condition: " + currentCondition);
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

        Debug.Log("current condition: " + currentCondition);
        return currentCondition;
    }

}
