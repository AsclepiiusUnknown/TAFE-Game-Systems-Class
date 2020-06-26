using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Header("Functionality Object References")]
    public PlayerHandler player; //The player script
    public LinearInventory inventory; //The inventory script
    public int requiredLevel;

    [Header("UI Components")]
    public GameObject questWindow; //The panel/window used to hold all our quest elements
    public Button claimBtn, acceptBtn, declineBtn;
    public Text statusText;
    public Text titleText, descriptionText, experienceText, goldText; //the text elements for the quest window
    public Quest quest; //The quest script


    //Used to opent he querst window and prepare the elements
    public void OpenQuestWindow()
    {
        //Enable the window so we can see it
        questWindow.SetActive(true);

        //Setup all the text elements with correspodning quest elements
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString() + "EXP";
        goldText.text = "$" + quest.goldReward.ToString();

        if (quest.goal.questState == QuestState.Active)
        {
            statusText.text = "Status: Active";
        }
        else if (quest.goal.questState == QuestState.Available)
        {
            acceptBtn.gameObject.SetActive(true);
            declineBtn.gameObject.SetActive(true);
            statusText.text = "Status: Avalable";
        }
        else if (quest.goal.questState == QuestState.Claimed)
        {
            statusText.text = "Status: Incomplete";
        }
        else if (quest.goal.questState == QuestState.Completed)
        {
            statusText.text = "Status: Completed. I have no other quests for you at this point in time, maybe someone else does though...";
            claimBtn.gameObject.SetActive(true);
        }

        if (!(PlayerHandler.level >= requiredLevel))
        {
            acceptBtn.gameObject.SetActive(false);
            declineBtn.gameObject.SetActive(false);
            statusText.text = "Status: Unavailable. You are not high enough level to access this quest.";
        }
    }

    //Used to accept the quest
    public void AcceptQuest()
    {
        quest.goal.questState = QuestState.Active; //Set the enum to Active cause we accepted it
        questWindow.SetActive(false); //Disable the window so the player can go on the quest and keep playing
        player.quest = quest; //Set the players quest to be the one they have accepted
        player.currentExp += 10;
    }

    public void DeclineQuest()
    {
        questWindow.SetActive(false); //Disable the window so the player can go on the quest and keep playing
        player.quest = null; //Set the players quest to be the one they have accepted
    }

    public void ClaimQuest()
    {
        if (quest.goal.isReached)
        {
            player.currentExp += quest.experienceReward;
            LinearInventory.money += quest.goldReward;
            inventory.moneyUI.text = "MONEY: $" + LinearInventory.money;

            quest.goal.questState = QuestState.Claimed;
            Debug.Log("You got " + quest.experienceReward + " EXP points and $" + quest.goldReward);
        }
        else
        {
            Debug.Log("Quest not yet completed.");
        }
    }
}
