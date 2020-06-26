using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<Item> shopInv = new List<Item>();
    public Item selectedItem;
    public bool showShopInv;
    public Vector2 scr;
    public LinearInventory playerInv;
    private int itemPrice;

    public static bool showShop;
    public Slots[] ShopSlots;
    public GameObject shopContainer;
    //*CHEST SELECTION//
    [System.Serializable]
    public struct ShopSelection
    {
        //The general selection parts
        public Selection selection;

        //Buttons for en/disabling
        public Button buyBtn;

        public Text costText;
    }
    public ShopSelection shopSelection;


    void Start()
    {
        shopInv.Add(ItemData.CreateItem(Random.Range(0, 2)));
        shopInv.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInv.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInv.Add(ItemData.CreateItem(Random.Range(100, 102)));
        shopInv.Add(ItemData.CreateItem(Random.Range(200, 202)));
        shopInv.Add(ItemData.CreateItem(Random.Range(200, 202)));
        shopInv.Add(ItemData.CreateItem(Random.Range(200, 202)));

        for (int i = 0; i < ShopSlots.Length; i++)
        {
            if (ShopSlots[i].slot.GetComponentInChildren<Text>() != null)
            {
                ShopSlots[i].slotName = ShopSlots[i].slot.GetComponentInChildren<Text>();
            }
        }
    }

    private void Update()
    {
        ToggleShopSelection(showShopInv);
    }

    public void EnterShopBtn()
    {
        shopContainer.SetActive(true);

        LinearInventory.currentShop = this;
        playerInv.ToggleInv();

        showShopInv = true;
        RefreshShop();
    }

    public void ExitShop()
    {
        shopContainer.SetActive(false);

        LinearInventory.currentShop = null;
        playerInv.ToggleInv();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        showShopInv = false;
    }

    public void BuyItem()
    {
        LinearInventory.money -= itemPrice;
        playerInv.moneyUI.text = "MONEY: $" + LinearInventory.money;

        //Add to player
        LinearInventory.inv.Add(ItemData.CreateItem(selectedItem.ID));
        //Remove from shop
        shopInv.Remove(selectedItem);
        selectedItem = null;

        RefreshShop();

        if (playerInv.showAll)
        {
            playerInv.FilterType("All");
        }
        else
        {
            playerInv.FilterType(playerInv.filterType.ToString());
        }
    }

    public void ToggleShopSelection(bool open)
    {
        Selection selected = shopSelection.selection;

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

            //Cost text
            itemPrice = selectedItem.Value / 3 * 4;
            shopSelection.costText.text = "COST: $" + itemPrice;

            #region Buy Item Btn
            if (selectedItem != null && LinearInventory.inv.Count < playerInv.invSlots.Length && LinearInventory.money >= itemPrice)
            {
                shopSelection.buyBtn.gameObject.SetActive(true);
            }
            else
            {
                shopSelection.buyBtn.gameObject.SetActive(false);
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

                shopSelection.buyBtn.gameObject.SetActive(false);
            }
            #endregion
        }
    }

    public void SelectShopSlot(int slotIndex)
    {
        selectedItem = shopInv[slotIndex];
        ToggleShopSelection(true);
    }

    public void RefreshShop()
    {
        #region Shop Slots
        for (int i = 0; i < ShopSlots.Length; i++)
        {
            if (i < shopInv.Count && shopInv[i] != null)
            {
                //enable the slot
                ShopSlots[i].slot.gameObject.SetActive(true);
                // display the name
                ShopSlots[i].slotName.text = shopInv[i].Name + " : " + shopInv[i].Amount;
            }
            else
            {
                //disable the slot
                ShopSlots[i].slot.gameObject.SetActive(false);
                // set the name to nothing
                ShopSlots[i].slotName.text = "";
            }
        }
        #endregion
    }
}
