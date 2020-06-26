using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    [Header("Functionality Object References")]
    public PlayerHandler player; //The player script
    public LinearInventory inventory; //The inventory script

    [Header("UI Components")]
    public GameObject questWindow; //The panel/window used to hold all our quest elements
    public Button claimBtn, acceptBtn, declineBtn;
    public Text statusText;
    public Text titleText, descriptionText, experienceText, goldText; //the text elements for the quest window

    public QuestGiver currentQuestGiver;
    [HideInInspector]
    public Quest currentQuest;

    private void Start()
    {
        if (currentQuestGiver != null)
        {
            if (currentQuestGiver.quest != null)
            {
                currentQuest = currentQuestGiver.quest;
            }
        }
    }

    public void OpenQuestWindow()
    {
        //Enable the window so we can see it
        questWindow.SetActive(true);

        //Setup all the text elements with correspodning quest elements
        titleText.text = currentQuest.title;
        descriptionText.text = currentQuest.description;
        experienceText.text = currentQuest.experienceReward.ToString() + "EXP";
        goldText.text = "$" + currentQuest.goldReward.ToString();

        #region Quest Status
        if (currentQuest.goal.questState == QuestState.Active)
        {
            statusText.text = "Status: Active";
        }
        else if (currentQuest.goal.questState == QuestState.Available)
        {
            acceptBtn.gameObject.SetActive(true);
            declineBtn.gameObject.SetActive(true);
            statusText.text = "Status: Avalable";
        }
        else if (currentQuest.goal.questState == QuestState.Claimed)
        {
            statusText.text = "Status: Incomplete";
        }
        else if (currentQuest.goal.questState == QuestState.Completed)
        {
            statusText.text = "Status: Completed. I have no other quests for you at this point in time, maybe someone else does though...";
            claimBtn.gameObject.SetActive(true);
        }

        if (!(PlayerHandler.level >= currentQuest.requiredLevel))
        {
            acceptBtn.gameObject.SetActive(false);
            declineBtn.gameObject.SetActive(false);
            statusText.text = "Status: Unavailable. You are not high enough level to access this currentQuest.";
        }
        #endregion
    }

    //Used to accept the quest
    public void AcceptQuest()
    {
        currentQuest.goal.questState = QuestState.Active; //Set the enum to Active cause we accepted it
        questWindow.SetActive(false); //Disable the window so the player can go on the quest and keep playing
        PlayerHandler.quest = currentQuest; //Set the players quest to be the one they have accepted
        player.currentExp += 10;
    }

    public void DeclineQuest()
    {
        questWindow.SetActive(false); //Disable the window so the player can go on the quest and keep playing
        PlayerHandler.quest = null; //Set the players quest to be the one they have accepted
    }

    public void ClaimQuest()
    {
        if (currentQuest.goal.isReached)
        {
            player.currentExp += currentQuest.experienceReward;
            LinearInventory.money += currentQuest.goldReward;
            inventory.moneyUI.text = "MONEY: $" + LinearInventory.money;

            currentQuest.goal.questState = QuestState.Claimed;
            Debug.Log("You got " + currentQuest.experienceReward + " EXP points and $" + currentQuest.goldReward);
            PlayerHandler.quest = currentQuest; //Set the players quest to be the one they have accepted
        }
        else
        {
            Debug.Log("Quest not yet completed.");
        }
    }
}
