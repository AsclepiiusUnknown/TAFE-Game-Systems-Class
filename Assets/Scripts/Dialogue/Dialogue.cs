using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script can be found in the Component section under the option NPC/Dialogue
[AddComponentMenu("Game Systems/RPG/NPC/Dialogue Linear")]

public class Dialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDlg;
    //index for our current line of dialogue and an index for a set question marker of the dialogue 
    public int index;
    public Vector2 scr;
    //object reference to the player
    public Player.MouseLook playerMouseLook;
    //mouselook script reference for the maincamera
    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public new string name;
    //array for text for our dialogue
    public string[] dialogueText;
    #endregion
    #region Start
    //find and reference the player object by tag
    //find and reference the maincamera by tag and get the mouse look component 
    #endregion

    #region OnGUI
    private void OnGUI()//OnGUI
    {
        //if our dialogue can be seen on screen
        if (showDlg)
        {
            //set up our ratio messurements for 16:9
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, scr.y * 3), name + " : " + dialogueText[index]);
            //if not at the end of the dialogue or not at the options part
            if (!(index >= dialogueText.Length - 1))
            {
                //next button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Next"))
                {
                    index++;
                }
            }
            //else if we are at options
            //Accept button allows us to skip forward to the next line of dialogue
            //Decline button skips us to the end of the characters dialogue 
            //else we are at the end
            //the Bye button allows up to end our dialogue
            else
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, scr.y * 0.5f), "Bye"))
                {
                    //close the dialogue box
                    showDlg = false;
                    //set index back to 0 
                    index = 0;
                    //allow cameras mouselook to be turned back on
                    //get the component mouselook on the character and turn that back on
                    Camera.main.GetComponent<Player.MouseLook>().enabled = true;
                    //get the component movement on the character and turn that back on
                    playerMouseLook.enabled = true;

                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible       
                    Cursor.visible = false;
                }

            }

        }
    }
    #endregion
}
