using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearInventory : MonoBehaviour
{
    #region Variables
    public PlayerHandler player;
    public static List<Item> inv = new List<Item>();
    public Item selectedItem;
    public static bool showInv;
    public GUIStyle[] Styles;

    public Vector2 scr;
    public Vector2 scrollPos;
    public string sortType = "";
    public string[] enumTypesForItems;
    public static int money;

    public Transform dropLocation;

    [System.Serializable]
    public struct Equipment
    {
        public string slotName;
        public Transform equipLocation;
        public GameObject currentItem;
    };

    public Equipment[] equipmentSlots;

    public static Chest currentChest;
    public static Shop currentShop;

    #region SLOTS
    [System.Serializable]
    public struct Slots
    {
        public Button slot;
        public Text slotName;
        public Item slotItem;
    }
    [Header("Slots")]
    public Slots[] invSlots;
    public Slots[] chestSlots;
    #endregion

    #region SELECTION
    //*ITEM SELECTION//
    [System.Serializable]
    public struct Selection
    {
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

    //*CHEST SELECTION//
    [System.Serializable]
    public struct ChestSelection
    {
        //The general selection parts
        public Selection selection;

        //Buttons for en/disabling
        public Button takeItemBtn;
    }

    [Header("Selection")]
    public InvSelection invSelection;
    public ChestSelection chestSelection;
    public GameObject InvContainer;
    #endregion
    #endregion

    void Start()
    {
        player = this.gameObject.GetComponent<PlayerHandler>();
        enumTypesForItems = new string[] { "Food", "Weapon", "Apparel", "Crafting", "Ingredients", "Potions", "Scrolls", "Quest", "Money" };

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
    }

    void Update()
    {
        #region Screen Dimensions
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        #endregion

        #region Inventory Toggling
        if (Input.GetKeyDown(KeyBindManager.keys["Inventory"]) && !PauseMenu.isPaused)
        {
            showInv = !showInv;
        }
        #endregion

        #region Show/Hide Inventory Items
        if (showInv)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            InvContainer.SetActive(true);

            #region Selection Handling
            if (selectedItem != null)
            {
                invSelection.selection.selectionIcon = selectedItem.Icon;
                invSelection.selection.selectionName.text = selectedItem.Name;
                #region Description
                invSelection.selection.selectionDescription.text =
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
                    invSelection.useItemBtn.enabled = false;
                }
                else
                {
                    invSelection.useItemBtn.enabled = true;
                }
                #endregion

                #region Move Item Btn
                if (currentChest != null)
                {
                    invSelection.moveItemBtn.enabled = true;
                }
                else
                {
                    invSelection.moveItemBtn.enabled = false;
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
            #endregion
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

            #region Selection Handling
            if (selectedItem != null)
            {
                invSelection.selection.selectionIcon = null;
                invSelection.selection.selectionName.text = "";
                invSelection.selection.selectionDescription.text = "";
                invSelection.useItemBtn.enabled = false;
                invSelection.moveItemBtn.enabled = false;
                invSelection.discardItemBtn.enabled = false;
                invSelection.useItemText.text = "";
            }
            #endregion
        }

        /**if (showInv)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (currentChest != null)
                currentChest.showChestInv = false;
            currentChest = null;
            return;
        }*/
        #endregion

        #region Inventory Slots
        for (int i = 0; i < invSlots.Length; i++)
        {
            //invSlots[i].slot.enabled = (inv[i] != null) ? true : false;
            if (inv[i] != null)
            {
                //enable the slot
                invSlots[i].slot.enabled = true;
                // display the name
                invSlots[i].slotName.text = inv[i].Name + " : " + inv[i].Amount;
            }
            else
            {
                //disable the slot
                invSlots[i].slot.enabled = false;
                // set the name to nothing
                invSlots[1].slotName.text = "";
            }
        }
        #endregion

        #region Chest Slots
        if (currentChest != null)
        {
            for (int i = 0; i < chestSlots.Length; i++)
            {
                //invSlots[i].slot.enabled = (inv[i] != null) ? true : false;
                if (currentChest.chestInv[i] != null)
                {
                    //enable the slot
                    chestSlots[i].slot.enabled = true;
                    // display the name
                    chestSlots[i].slotName.text = currentChest.chestInv[i].Name + " : " + inv[i].Amount;
                }
                else
                {
                    //disable the slot
                    chestSlots[i].slot.enabled = false;
                    // set the name to nothing
                    chestSlots[1].slotName.text = "";
                }
            }
        }
        else
        {
            for (int i = 0; i < chestSlots.Length; i++)
            {

                //disable the slot
                chestSlots[i].slot.enabled = false;
                // set the name to nothing
                chestSlots[1].slotName.text = "";
            }
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

    void Display()
    {
        //IF we want to display everything in our inventory
        if (sortType == "All" || sortType == "")
        {
            //if we have max amount or less (space at top and bottom)
            if (inv.Count <= 34)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
            }
            //More than max amount
            else
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, inv.Count * .25f * scr.y), false, true);

                #region Scroll Content
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(.5f * scr.x, i * (.25f * scr.y), 3 * scr.x, .25f * scr.y), inv[i].Name))
                    {
                        selectedItem = inv[i];
                    }
                }
                #endregion

                GUI.EndScrollView();
            }
        }
        //if we are displaying off a type or category
        else
        {
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);

            //Amount of that type
            int a = 0;
            //Slot position
            int s = 0;

            for (int i = 0; i < inv.Count; i++)
            {
                if (inv[i].Type == type)
                {
                    a++; //Increase count of that type
                }
            }
            if (a <= 34)//Within max
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                        s++;
                    }
                }
            }
            else //larger than max
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, a * .25f * scr.y), false, true);

                #region Scroll Content
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        if (GUI.Button(new Rect(.5f * scr.x, s * (.25f * scr.y), 3 * scr.x, .25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                        s++;
                    }
                }
                #endregion

                GUI.EndScrollView();
            }
        }
    }

    void UseItem()
    {
        //GUI.Box(new Rect(4.25f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon, Styles[0]);
        GUI.Box(new Rect(4.55f * scr.x, 3.5f * scr.y, 2.5f * scr.x, .5f * scr.y), selectedItem.Name);
        GUI.Box(new Rect(4.25f * scr.x, 4 * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount, Styles[3]);
        GUI.Box(new Rect(4 * scr.x, 0.25f * scr.y, 3.5f * scr.x, 7 * scr.y), "", Styles[1]);


        string armourTemptext = "", healTemptext = "", damageTemptext = "";

        #region Extra Checks
        if (selectedItem.Armour > 0)
        {
            armourTemptext = "\nArmour: " + selectedItem.Armour;
        }
        if (selectedItem.Heal > 0)
        {
            healTemptext = "\nHeal: " + selectedItem.Heal;
        }
        if (selectedItem.Damage > 0)
        {
            damageTemptext = "\nDamage: " + selectedItem.Damage;
        }
        #endregion

        switch (selectedItem.Type)
        {
            case ItemType.Food:
                if (player.attributes[0].currentValue < player.attributes[0].maxValue)
                {
                    if (GUI.Button(new Rect(4.5f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                    {
                        //player.attributes[0].currentValue = mathf
                        if (selectedItem.Amount > 1)
                        {
                            selectedItem.Amount--;
                        }
                        else
                        {
                            inv.Remove(selectedItem);
                            selectedItem = null;
                            return;
                        }
                        player.attributes[0].currentValue += selectedItem.Heal;
                    }
                }

                break;
            case ItemType.Weapon:
                if (equipmentSlots[2].currentItem == null || selectedItem.Name != equipmentSlots[2].currentItem.name)
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                    {
                        //IF we have something equipped 
                        if (equipmentSlots[2].currentItem != null)
                        {
                            Destroy(equipmentSlots[2].currentItem);
                        }
                        GameObject currentItem = Instantiate(selectedItem.Mesh, equipmentSlots[3].equipLocation);
                        equipmentSlots[2].currentItem = currentItem;
                        currentItem.name = selectedItem.Name;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Unequip"))
                    {
                        Destroy(equipmentSlots[2].currentItem);
                    }
                }

                break;
            case ItemType.Apparel:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                {

                }
                break;
            case ItemType.Crafting:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Craft"))
                {

                }
                break;
            case ItemType.Ingredients:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Create"))
                {

                }
                break;
            case ItemType.Potions:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Consume"))
                {

                }
                break;
            case ItemType.Scrolls:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Use"))
                {

                }
                break;
            case ItemType.Quest:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Accept"))
                {

                }
                break;
            case ItemType.Money:
                if (GUI.Button(new Rect(4.75f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                {

                }
                break;
            default:
                break;
        }

        if (GUI.Button(new Rect(5.25f * scr.x, 6.75f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                //Check Slots
                if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                {
                    //Destroy the one in our equiptment
                    Destroy(equipmentSlots[i].currentItem);
                }
            }

            //Spawn the item
            GameObject droppedItem = Instantiate(selectedItem.Mesh, dropLocation.position, Quaternion.identity);
            droppedItem.name = selectedItem.Name;
            droppedItem.AddComponent<Rigidbody>().useGravity = true;
            droppedItem.GetComponent<ItemHandler>().enabled = true;

            if (selectedItem.Amount > 1)
            {
                Debug.Log(selectedItem.Amount.ToString());
                selectedItem.Amount--;
            }
            else
            {
                inv.Remove(selectedItem);
                selectedItem = null;
                return;
            }
        }

        if (currentChest != null)
        {
            if (GUI.Button(new Rect(5.75f * scr.x, 6.5f * scr.y, scr.x, .25f * scr.y), "Move Item"))
            {
                for (int i = 0; i < equipmentSlots.Length; i++)
                {
                    if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                    {
                        Destroy(equipmentSlots[i].currentItem);
                    }
                }
                currentChest.chestInv.Add(selectedItem);
                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
        if (currentShop != null)
        {
            if (GUI.Button(new Rect(5.75f * scr.x, 6.5f * scr.y, scr.x, .25f * scr.y), "Sell Item"))
            {
                for (int i = 0; i < equipmentSlots.Length; i++)
                {
                    if (equipmentSlots[i].currentItem != null && selectedItem.Name == equipmentSlots[i].currentItem.name)
                    {
                        Destroy(equipmentSlots[i].currentItem);
                    }
                }
                money += selectedItem.Value;
                currentShop.shopInv.Add(selectedItem);
                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
        GUI.skin = null;
    }

    /**private void OnGUI()
    {
        if (showInv)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            for (int i = 0; i < enumTypesForItems.Length; i++)
            {
                if (GUI.Button(new Rect(4 * scr.x + i * scr.x, 0, scr.x, 0.25f * scr.y), enumTypesForItems[i]))
                {
                    sortType = enumTypesForItems[i];
                }
            }
            Display();
            if (selectedItem != null)
            {
                UseItem();
            }
        }
    }*/
}
