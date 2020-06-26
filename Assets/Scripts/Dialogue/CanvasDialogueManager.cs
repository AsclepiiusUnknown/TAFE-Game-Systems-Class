using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text buttonText;
    public Button[] buttons;
    public int byeBtnIndex;
    string textHolder;

    public string[] currentDialogue;
    public int index;
    public Player.MouseLook playerMouseLook;

    [Header("Quest")]
    public Button questBtn;
    public QuestGiver currentQuest;

    [Header("From Player")]
    public NpcDialogueArray currentNPC;

    // Start is called before the first frame update
    void Start()
    {
        playerMouseLook = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.MouseLook>();

        textHolder = buttonText.text;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            index = 0;
            Camera.main.GetComponent<Player.MouseLook>().enabled = true;
            playerMouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            dialoguePanel.SetActive(false);
        }
    }

    void ApprovalDlg()
    {
        if (currentNPC.approval <= -2)
        {
            currentDialogue = currentNPC.extraNegDlgText;
        }
        if (currentNPC.approval == -1)
        {
            currentDialogue = currentNPC.negDlgText;
        }
        if (currentNPC.approval == 0)
        {
            currentDialogue = currentNPC.neuDlgText;
        }
        if (currentNPC.approval == 1)
        {
            currentDialogue = currentNPC.posDlgText;
        }
        if (currentNPC.approval >= 2)
        {
            currentDialogue = currentNPC.extraPosDlgText;
        }
    }

    public void SetUp()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        buttonText.text = textHolder;

        ApprovalDlg();
        dialogueText.text = currentNPC.characterName + " : " + currentDialogue[index];
        //buttonText.text = "Next";

        if ((currentQuest.quest.goal.questState == QuestState.Completed || currentQuest.quest.goal.questState == QuestState.Available) && currentQuest.requiredLevel <= PlayerHandler.level)
        {
            questBtn.gameObject.SetActive(true);
        }
        else
        {
            questBtn.gameObject.SetActive(false);
        }
    }

    public void ButtonInteraction(int approvalChange)
    {
        currentNPC.approval = Mathf.Clamp(approvalChange + currentNPC.approval, -2, 2);

        if (!(index >= currentDialogue.Length - 1))
        {
            index++;
            if (index >= currentDialogue.Length - 1)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (i != byeBtnIndex)
                    {
                        buttons[i].gameObject.SetActive(false);
                    }
                }

                if (buttonText != null)
                {
                    buttonText.text = "Bye.";
                }
                else
                {
                    buttonText = buttons[byeBtnIndex].GetComponentInChildren<Text>();
                    buttonText.text = "Bye.";
                }
            }
        }
        else
        {
            index = 0;
            Camera.main.GetComponent<Player.MouseLook>().enabled = true;
            playerMouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            dialoguePanel.SetActive(false);
        }
        dialogueText.text = currentNPC.characterName + ": " + currentDialogue[index];
    }
}
