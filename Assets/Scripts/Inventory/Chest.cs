using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> chestInv = new List<Item>();
    public Item selectedItem;
    public bool showChestInv;
    public Vector2 scr;

    void Start()
    {
        chestInv.Add(ItemData.CreateItem(Random.Range(0, 2)));
        chestInv.Add(ItemData.CreateItem(Random.Range(100, 102)));
    }

    private void OnGUI()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if (showChestInv)
        {
            //Display chest items
            for (int i = 0; i < chestInv.Count; i++)
            {
                if (GUI.Button(new Rect(12.5f * scr.x, (0.25f * scr.y) + i * (0.25f * scr.y), 3 * scr.x, .25f * scr.y), chestInv[i].Name))
                {
                    selectedItem = chestInv[i];
                }
            }

            if (selectedItem != null)
            {
                GUI.Box(new Rect(8.5f * scr.x, 0.25f * scr.y, 3.5f * scr.x, 7 * scr.y), "");
                //GUI.Box(new Rect(8.75f * scr.x, 0.5f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Icon);
                GUI.Box(new Rect(9.05f * scr.x, 3.5f * scr.y, 2.5f * scr.x, .5f * scr.y), selectedItem.Name);
                GUI.Box(new Rect(8.75f * scr.x, 4 * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);

                if (GUI.Button(new Rect(10.5f * scr.x, 6.5f * scr.y, scr.x, 0.25f * scr.y), "Take Item"))
                {
                    //Add to player
                    LinearInventory.inv.Add(ItemData.CreateItem(selectedItem.ID));
                    //Remove from chest
                    chestInv.Remove(selectedItem);

                    selectedItem = null;
                    return;
                }
            }

            //display items in players inv
        }
    }
}
