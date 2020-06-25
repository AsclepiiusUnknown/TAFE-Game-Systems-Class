using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public List<Item> chestInv = new List<Item>();
    public Item selectedItem;
    public bool showChestInv;
    public Vector2 scr;
    public LinearInventory playerInv;

    public static bool showChest;
    public Slots[] chestSlots;
    public GameObject chestContainer;
    //*CHEST SELECTION//
    [System.Serializable]
    public struct ChestSelection
    {
        //The general selection parts
        public Selection selection;

        //Buttons for en/disabling
        public Button takeItemBtn;
    }
    public ChestSelection chestSelection;


    void Start()
    {
        chestInv.Add(ItemData.CreateItem(Random.Range(0, 2)));
        chestInv.Add(ItemData.CreateItem(Random.Range(100, 102)));

        for (int i = 0; i < chestSlots.Length; i++)
        {
            if (chestSlots[i].slot.GetComponentInChildren<Text>() != null)
            {
                chestSlots[i].slotName = chestSlots[i].slot.GetComponentInChildren<Text>();
            }
        }
    }

    private void Update()
    {
        ToggleChestSelection(showChestInv);
    }

    public void ToggleChestSelection(bool open)
    {
        Selection selected = chestSelection.selection;

        if (selectedItem != null && open)
        {
            selected.selectionContainer.SetActive(true);

            selected.selectionIcon.sprite = Sprite.Create(selectedItem.Icon, new Rect(0, 0, selectedItem.Icon.width, selectedItem.Icon.height), Vector2.zero);
            selected.selectionName.text = selectedItem.Name;
            #region Description
            selected.selectionDescription.text =
            "Description: " + selectedItem.Description +
            "\n Amount: " + selectedItem.Amount +
            "\n Value: " + selectedItem.Value +
            "\n Damage: " + selectedItem.Damage +
            "\n Heal: " + selectedItem.Heal +
            "\n Armour: " + selectedItem.Armour;
            #endregion

            #region Take Item Btn
            if (selectedItem != null && LinearInventory.inv.Count < playerInv.invSlots.Length)
            {
                chestSelection.takeItemBtn.gameObject.SetActive(true);
            }
            else
            {
                chestSelection.takeItemBtn.gameObject.SetActive(false);
            }
            #endregion
        }
        else
        {
            selected.selectionContainer.SetActive(false);

            #region Selection Handling
            if (selectedItem != null)
            {
                selected.selectionIcon = null;
                selected.selectionName.text = "";
                selected.selectionDescription.text = "";

                chestSelection.takeItemBtn.gameObject.SetActive(false);
            }
            #endregion
        }
    }

    public void SelectChestSlot(int slotIndex)
    {
        selectedItem = chestInv[slotIndex];
        ToggleChestSelection(true);
    }

    public void RefreshChest()
    {
        #region Chest Slots
        for (int i = 0; i < chestSlots.Length; i++)
        {
            if (i < chestInv.Count && chestInv[i] != null)
            {
                //enable the slot
                chestSlots[i].slot.gameObject.SetActive(true);
                // display the name
                chestSlots[i].slotName.text = chestInv[i].Name + " : " + chestInv[i].Amount;
            }
            else
            {
                //disable the slot
                chestSlots[i].slot.gameObject.SetActive(false);
                // set the name to nothing
                chestSlots[i].slotName.text = "";
            }
        }
        #endregion
    }
}
