using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public QuestState questState;

    public GoalType goalType;

    public string enemyType;
    public int itemId;
    public int requiredAmount;
    public int currentAmount;
    public bool isReached;
    //(currentAmount >= requiredAmount);

    public void EnemyKilled(string type)
    {
        if (goalType == GoalType.Kill && type == enemyType)
        {
            currentAmount++;
            if (currentAmount >= requiredAmount)
            {
                isReached = true;
                questState = QuestState.Completed;
                Debug.Log("QUEST COMPLETE");
            }
        }
    }

    public void ItemCollected(int ID)
    {
        if (goalType == GoalType.Gather && ID == itemId)
        {
            currentAmount++;
            if (currentAmount >= requiredAmount)
            {
                isReached = true;
                questState = QuestState.Completed;
                Debug.Log("QUEST COMPLETE");
            }
        }
    }


    #region Ignore
    /*public void Complete()
    {
        goal.questState = QuestState.Completed;
        Debug.Log(title + " was completed succesfully. Collect reward.");
    }

    public void Claimed()
    {
        questState = QuestState.Claimed;
        Debug.Log(title + " reward was collected.");
    }*/
    #endregion
}

public enum QuestState
{
    Available,
    Active,
    Completed,
    Claimed
}

public enum GoalType
{
    Kill,
    Gather
    //Escort
    //Locate
}