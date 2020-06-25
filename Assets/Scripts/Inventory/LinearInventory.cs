using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearInventory : MonoBehaviour
{
    #region Variables
    // * //
    #region SLOTS
    [Header("Slots")]
    public Slots[] invSlots;
    #endregion

    #region SELECTION
    [Header("Selection")]
    public InvSelection invSelection;
    public GameObject InvContainer;
    #endregion

    #region ITEMS
    [Header("Items")]
    public static List<Item> inv = new List<Item>();
    public Item selectedItem;
    public static bool showInv;
    #endregion

    #region EQUIPMENT
    [Header("Equipment")]
    public Equipment[] equipmentSlots;
    #endregion

    #region CHEST
    [Header("Chest")]
    public static Chest currentChest;
    #endregion

    #region SHOP
    [Header("Shop")]
    public static Shop currentShop;
    public static int money;
    #endregion

    #region SORTING
    [Header("Sorting")]
    public string sortType = "";
    public string[] enumTypesForItems;
    #endregion

    #region MISC.
    [Header("Misc.")]
    public GameObject invBacking;
    public PlayerHandler player;
    public Transform dropLocation;
    #endregion
    #endregion


    void Start()
    {
        #region Setup
        player = this.gameObject.GetComponent<PlayerHandler>();
        enumTypesForItems = new string[] { "Food", "Weapon", "Apparel", "Crafting", "Ingredients", "Potions", "Scrolls", "Quest", "Money" };
        #endregion

        #region Random Starting Items
        int a = Random.Range(1, 3);
        for (int i = 0; i < a; i++)
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 2)));
            inv.Add(ItemData.CreateItem(Random.Range(100, 103)));
            inv.Add(ItemData.CreateItem(Random.Range(200, 212)));
            inv.Add(ItemData.CreateItem(Random.Range(200, 212)));
            inv.Add(ItemData.CreateItem(Random.Range(300, 302)));
            inv.Add(ItemData.CreateItem(Random.Range(500, 502)));
            inv.Add(ItemData.CreateItem(Random.Range(600, 602)));
        }
        #endregion
    }

    void Update()
    {
        #region Inventory Toggling
        if (Input.GetKeyDown(KeyBindManager.keys["Inventory"]) && !PauseMenu.isPaused)
        {
            ToggleInv();
        }
        #endregion

        #region Selection Toggling
        if (selectedItem != null && !invSelection.selection.selectionContainer.activeSelf && showInv)
        {
            ToggleInvSelection(showInv);
        }
        else
        {
            ToggleInvSelection(false);
        }
        #endregion

        #region Backing Toggling
        if (showInv && !invBacking.activeSelf)
        {
            invBacking.SetActive(true);
        }
        else if (currentChest != null && currentChest.showChestInv && !invBacking.activeSelf)
        {
            invBacking.SetActive(true);
        }
        else
        {
            invBacking.SetActive(false);
        }
        #endregion

        #region Editor Testing
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.I))
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
            inv.Add(ItemData.CreateItem(Random.Range(100, 103)));
            inv.Add(ItemData.CreateItem(Random.Range(200, 203)));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            sortType = "Food";
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            sortType = "All";
        }
#endif
        #endregion
    }

    public void SelectInvSlot(int slotIndex)
    {
        Slots slot = invSlots[slotIndex];
        if (inv[slotIndex] != null)
        {
            selectedItem = invSlots[slotIndex].slotItem;
        }
    }

    public void ToggleInv()
    {
        showInv = !showInv;


        #region Show/Hide Inventory Items
        if (showInv)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            InvContainer.SetActive(true);

            ToggleInvSelection(false);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (currentChest != null)
                currentChest.showChestInv = false;
            currentChest = null;

            InvContainer.SetActive(false);


        }
        #endregion

        #region Inventory Slots
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (i < inv.Count && inv[i] != null)
            {
                //enable the slot
                invSlots[i].slot.gameObject.SetActive(true);
                // display the name
                invSlots[i].slotName.text = inv[i].Name + " : " + inv[i].Amount;
            }
            else
            {
                //disable the slot
                invSlots[i].slot.gameObject.SetActive(false);
                // set the name to nothing
                invSlots[i].slotName.text = "";
            }
            #endregion
        }
    }

    public void ToggleInvSelection(bool open)
    {
        Selection selected = invSelection.selection;

        if (selectedItem != null && open)
        {
            selected.selectionContainer.SetActive(true);

            selected.selectionIcon = selectedItem.Icon;
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

            #region Button Enabling
            // * //
            #region Use Item Btn
            if (selectedItem.Type == ItemType.Crafting || selectedItem.Type == ItemType.Ingredients || selectedItem.Type == ItemType.Scrolls || selectedItem.Type == ItemType.Quest || selectedItem.Type == ItemType.Money)
            {
                invSelection.useItemBtn.gameObject.SetActive(false);
            }
            else
            {
                invSelection.useItemBtn.gameObject.SetActive(true);
            }
            #endregion

            #region Move Item Btn
            if (currentChest != null)
            {
                invSelection.moveItemBtn.gameObject.SetActive(true);
            }
            else
            {
                invSelection.moveItemBtn.gameObject.SetActive(false);
            }
            #endregion

            invSelection.discardItemBtn.enabled = true;
            #endregion

            #region Use Btn Text Element
            if (invSelection.useItemText != null)
            {
                if (selectedItem.Type == ItemType.Food)
                {
                    invSelection.useItemText.text = "Eat";
                }
                else if (selectedItem.Type == ItemType.Weapon || selectedItem.Type == ItemType.Apparel)
                {
                    invSelection.useItemText.text = "Equip";
                }
                else if (selectedItem.Type == ItemType.Potions)
                {
                    invSelection.useItemText.text = "Consume";
                }
                else
                {
                    invSelection.useItemText.text = "Use";
                }
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

                invSelection.useItemBtn.gameObject.SetActive(false);
                invSelection.moveItemBtn.gameObject.SetActive(false);
                invSelection.discardItemBtn.gameObject.SetActive(false);
                invSelection.useItemText.text = "";
            }
            #endregion
        }
    }
}

//*SLOTS//
[System.Serializable]
public struct Slots
{
    public Button slot;
    public Text slotName;
    public Item slotItem;
}

//*ITEM SELECTION//
[System.Serializable]
public struct Selection
{
    public GameObject selectionContainer;

    public Image selectionIcon;
    public Text selectionName;
    public Text selectionDescription;
}

//*INV SELECTION//
[System.Serializable]
public struct InvSelection
{
    //The general selection parts
    public Selection selection;

    //Buttons for en/disabling
    public Button useItemBtn;
    public Button discardItemBtn;
    public Button moveItemBtn;

    //Button text to change depending on item type
    public Text useItemText;
}

//*EQUIPMENT//
[System.Serializable]
public struct Equipment
{
    public string slotName;
    public Transform equipLocation;
    public GameObject currentItem;
}