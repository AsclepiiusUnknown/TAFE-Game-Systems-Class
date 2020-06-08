using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Intro PRG/Player/Interact")]
public class Interact : MonoBehaviour
{
    #region  Variables
    [Header("Player & Camera connection")]
    public GameObject player;
    public GameObject mainCamera;
    public CanvasDialogueManager dlgMaster;
    #endregion

    #region Start

    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interact;

            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            RaycastHit hitInfo;

            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                #region NPC 
                //and that hits info is tagged NPC
                if (hitInfo.collider.CompareTag("NPC"))
                {
                    if (hitInfo.collider.GetComponent<NpcDialogueArray>())
                    {
                        NpcDialogueArray character = hitInfo.collider.GetComponent<NpcDialogueArray>();
                        dlgMaster.characterNpcName = character.characterName;
                        dlgMaster.currentDialogue = character.neuDlgText;
                        dlgMaster.SetUp();
                        dlgMaster.dialoguePanel.SetActive(true);

                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<Player.MouseLook>().enabled = false;
                        GetComponent<Player.MouseLook>().enabled = false;

                    }
                    #region HardCode Dialogue
                    //Debug that we hit a NPC    
                    Debug.Log("Talk to the NPC");
                    //THIS ONE HERE IS FOR DIALOGUE
                    if (hitInfo.collider.GetComponent<Dialogue>())
                    {
                        hitInfo.collider.GetComponent<Dialogue>().showDlg = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<Player.MouseLook>().enabled = false;
                        GetComponent<Player.MouseLook>().enabled = false;
                    }

                    //THIS ONE HERE IS FOR OptionLinearDialogue
                    if (hitInfo.collider.GetComponent<OptionLinearDialogue>())
                    {
                        hitInfo.collider.GetComponent<OptionLinearDialogue>().showDlg = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<Player.MouseLook>().enabled = false;
                        GetComponent<Player.MouseLook>().enabled = false;
                    }

                    //THIS ONE HERE IS FOR ApprovalDialogue
                    if (hitInfo.collider.GetComponent<ApprovalDialogue>())
                    {
                        hitInfo.collider.GetComponent<ApprovalDialogue>().showDlg = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Camera.main.GetComponent<Player.MouseLook>().enabled = false;
                        GetComponent<Player.MouseLook>().enabled = false;
                    }
                    #endregion
                }
                #endregion

                if (hitInfo.collider.CompareTag("Item"))
                {
                    Debug.Log("Pick Up Item");
                    ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                    if (handler != null)
                    {
                        handler.OnCollection();
                    }
                }

                if (hitInfo.collider.CompareTag("Chest"))
                {
                    Debug.Log("Open Chest");
                    Chest chest = hitInfo.transform.GetComponent<Chest>();

                    if (chest != null)
                    {
                        chest.showChestInv = true;
                        LinearInventory.showInv = true;
                        LinearInventory.currentChest = chest;

                        Time.timeScale = 0;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }

                    /*if (Input.GetKeyDown(KeyCode.Escape) && chest.showChestInv == true)
                    {
                        chest.showChestInv = false;
                        LinearInventory.showInv = false;
                        LinearInventory.currentChest = null;

                        Time.timeScale = 1;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }*/
                }
            }
        }
    }
}