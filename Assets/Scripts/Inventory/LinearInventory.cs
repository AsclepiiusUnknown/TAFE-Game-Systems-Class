using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInventory : MonoBehaviour
{
    public static List<Item> inv = new List<Item>();
    public Item selectedItem;
    public static bool showInv;

    public Vector2 scr;
    public Vector2 scrollPos;

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

    void Start()
    {
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
    }

    void Display()
    {
        for (int i = 0; i < inv.Count; i++)
        {
            if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
            {
                selectedItem = inv[i];
            }
        }
    }

    void UseItem()
    {
        GUI.Box(new Rect(4.75f * scr.x, 0.25f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon);
        switch (selectedItem.Type)
        {
            case ItemType.Food:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Eat"))
                {

                }
                break;
            case ItemType.Weapon:
                if (GUI.Button(new Rect(4.75f * scr.x, 8 * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                {

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
            Display();
            if (selectedItem != null)
            {
                UseItem();
            }
        }
    }
}
