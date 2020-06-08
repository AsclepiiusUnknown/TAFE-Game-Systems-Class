using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text buttonText;
    public string characterNpcName;
    public string[] currentDialogue;
    public int index;
    public Player.MouseLook playerMouseLook;

    // Start is called before the first frame update
    void Start()
    {
        playerMouseLook = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.MouseLook>();
    }

    public void SetUp()
    {
        dialogueText.text = characterNpcName + ": " + currentDialogue[index];
        buttonText.text = "Next";
    }

    public void ButtonInteraction()
    {
        if (!(index >= currentDialogue.Length - 1))
        {
            index++;
            if (index >= currentDialogue.Length - 1)
            {
                buttonText.text = "Bye.";
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
        dialogueText.text = characterNpcName + ": " + currentDialogue[index];
    }
}
