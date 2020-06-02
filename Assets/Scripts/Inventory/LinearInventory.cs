using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    public Equipment[] equipmentSlots;

    void Start()
    {
        player = this.gameObject.GetComponent<PlayerHandler>();
        enumTypesForItems = new string[] { "Food", "Weapon", "Apparel", "Crafting", "Ingredients", "Potions", "Scrolls", "Quest", "Money" };

        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(1));
        inv.Add(ItemData.CreateItem(30)); //broken example
        inv.Add(ItemData.CreateItem(100));
        inv.Add(ItemData.CreateItem(101));
        inv.Add(ItemData.CreateItem(102));
        inv.Add(ItemData.CreateItem(201));
    }

    void Update()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            showInv = !showInv;
            if (showInv)
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
                return;
            }
        }
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
        GUI.Box(new Rect(4.75f * scr.x, 0.25f * scr.y, 4 * scr.x, 4 * scr.y), selectedItem.Icon, Styles[0]);
        GUI.Box(new Rect(4.75f * scr.x - 7, 0.25f * scr.y - 7, 4 * scr.x + 14, 4 * scr.y + 14), "", Styles[1]);

        string armourTemptext = "", healTemptext = "", damageTemptext = "";

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

        GUI.Box(new Rect(4.75f * scr.x, 0.25f * scr.y + 4 * scr.x, 4 * scr.x, 2 * scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount + armourTemptext + healTemptext + damageTemptext, Styles[3]);




        switch (selectedItem.Type)
        {
            case ItemType.Food:
                if (player.attributes[0].currentValue < player.attributes[0].maxValue)
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Eat"))
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
                if (equipmentSlots[3].currentItem == null || selectedItem.Name != equipmentSlots[3].currentItem.name)
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                    {
                        if (equipmentSlots[3].currentItem != null)
                        {
                            Destroy(equipmentSlots[3].currentItem);
                        }
                        GameObject currentItem = Instantiate(selectedItem.Mesh, equipmentSlots[3].equipLocation);
                        currentItem.name = selectedItem.Name;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Unequip"))
                    {
                        Destroy(equipmentSlots[3].currentItem);
                    }
                }

                break;
            case ItemType.Apparel:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                {

                }
                break;
            case ItemType.Crafting:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Craft"))
                {

                }
                break;
            case ItemType.Ingredients:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Create"))
                {

                }
                break;
            case ItemType.Potions:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Consume"))
                {

                }
                break;
            case ItemType.Scrolls:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Use"))
                {

                }
                break;
            case ItemType.Quest:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Accept"))
                {

                }
                break;
            case ItemType.Money:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                {

                }
                break;
            default:
                break;
        }
    }

    private void OnGUI()
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
    }
}
