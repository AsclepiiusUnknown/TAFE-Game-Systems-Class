using UnityEngine;
using UnityEngine.UI;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        string _name = "";
        string _description = "";
        int _value = 0;
        int _amount = 0;
        string _icon = "";
        string _mesh = "";
        ItemType _type = ItemType.Apparel;
        int _damage = 0;
        int _armour = 0;
        int _heal = 0;

        void Start()
        {

        }

        switch (itemID)
        {
            #region Food 0 - 99
            case 0:
                _name = "Apple";
                _description = "Munchies & Crunchies";
                _value = 1;
                _amount = 1;
                _icon = "Food/Apple";
                _mesh = "Food/Apple";
                _type = ItemType.Food;
                _heal = 5;
                break;
            case 1:
                _name = "Meat";
                _description = "Steamed Hams";
                _value = 10;
                _amount = 1;
                _icon = "Food/Meat";
                _mesh = "Food/Meat";
                _type = ItemType.Food;
                _heal = 10;
                break;
            #endregion
            #region Weapons 100 - 199
            case 100:
                _name = "Axe";
                _description = "AXE is AXE";
                _value = 150;
                _amount = 1;
                _icon = "Weapons/Axe";
                _mesh = "Weapons/Axe";
                _type = ItemType.Weapon;
                _damage = 25;
                break;
            case 101:
                _name = "Bow";
                _description = "Pew Pew";
                _value = 75;
                _amount = 1;
                _icon = "Weapons/Bow";
                _mesh = "Weapons/Bow";
                _type = ItemType.Weapon;
                _damage = 7;
                break;
            case 102:
                _name = "Sword";
                _description = "Stick 'em with the pointy end";
                _value = 200;
                _amount = 1;
                _icon = "Weapons/Sword";
                _mesh = "Weapons/Sword";
                _type = ItemType.Weapon;
                _damage = 20;
                break;
            #endregion
            #region Apparel 200 - 299
            case 200:
                _name = "Armour";
                _description = "Protect the wearer";
                _value = 75;
                _amount = 1;
                _icon = "Apparel/Armour/Armour";
                _mesh = "Apparel/Armour/Armour";
                _type = ItemType.Apparel;
                _armour = 45;
                break;
            case 201:
                _name = "Boots";
                _description = "Running shoes";
                _value = 25;
                _amount = 1;
                _icon = "Apparel/Armour/Boots";
                _mesh = "Apparel/Armour/Boots";
                _type = ItemType.Apparel;
                _armour = 15;
                break;
            case 202:
                _name = "Bracers";
                _description = "Looking fancy and chucking a wonderwoman";
                _value = 10;
                _amount = 1;
                _icon = "Apparel/Armour/Armour";
                _mesh = "Apparel/Armour/Armour";
                _type = ItemType.Apparel;
                _armour = 8;
                break;
            case 203:
                _name = "Gloves";
                _description = "'Conceal dont feel, dont let them know...''";
                _value = 15;
                _amount = 1;
                _icon = "Apparel/Armour/Gloves";
                _mesh = "Apparel/Armour/Gloves";
                _type = ItemType.Apparel;
                _armour = 5;
                break;
            case 204:
                _name = "Helmet";
                _description = "'I dont wanna use my head!'";
                _value = 50;
                _amount = 1;
                _icon = "Apparel/Armour/Helmet";
                _mesh = "Apparel/Armour/Helmet";
                _type = ItemType.Apparel;
                _armour = 30;
                break;
            case 205:
                _name = "Pauldrons";
                _description = "Broaden your shoulders";
                _value = 20;
                _amount = 1;
                _icon = "Apparel/Armour/Pauldrons";
                _mesh = "Apparel/Armour/Pauldrons";
                _type = ItemType.Apparel;
                _armour = 15;
                break;
            case 206:
                _name = "Shield";
                _description = "Defense cause you're a wimp";
                _value = 25;
                _amount = 1;
                _icon = "Apparel/Armour/Shield";
                _mesh = "Apparel/Armour/Shield";
                _type = ItemType.Apparel;
                _armour = 20;
                break;
            case 207:
                _name = "Belt";
                _description = "Keep them up, nobody wants to see that";
                _value = 15;
                _amount = 1;
                _icon = "Apparel/Belt";
                _mesh = "Apparel/Belt";
                _type = ItemType.Apparel;
                break;
            case 208:
                _name = "Cloak";
                _description = "Yes pull off that Neo look";
                _value = 15;
                _amount = 1;
                _icon = "Apparel/Belt";
                _mesh = "Apparel/Belt";
                _type = ItemType.Apparel;
                break;
            case 209:
                _name = "Necklace";
                _description = "Ooooh Prettyyyy";
                _value = 40;
                _amount = 1;
                _icon = "Apparel/Necklace";
                _mesh = "Apparel/Necklace";
                _type = ItemType.Apparel;
                break;
            case 210:
                _name = "Pants";
                _description = "Long ones";
                _value = 20;
                _amount = 1;
                _icon = "Apparel/Pants";
                _mesh = "Apparel/Pants";
                _type = ItemType.Apparel;
                _armour = 5;
                break;
            case 211:
                _name = "Ring";
                _description = "Be a rich gold boi/girly";
                _value = 50;
                _amount = 1;
                _icon = "Apparel/Ring";
                _mesh = "Apparel/Ring";
                _type = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300 - 399
            case 300:
                _name = "Gem";
                _description = "Shiny solid water, or something like that...";
                _value = 75;
                _amount = 1;
                _icon = "Crafting/Gem";
                _mesh = "Crafting/Gem";
                _type = ItemType.Crafting;
                break;
            case 301:
                _name = "Ingot";
                _description = "To make things, hard things";
                _value = 40;
                _amount = 1;
                _icon = "Crafting/Ingot";
                _mesh = "Crafting/Ingot";
                _type = ItemType.Crafting;
                break;
            #endregion
            #region Ingredients 400 - 499
            #endregion
            #region Potions 500 - 599
            case 500:
                _name = "Health Potion";
                _description = "";
                _value = 120;
                _amount = 1;
                _icon = "Potions/HealthPotion";
                _mesh = "Potions/HealthPotion";
                _type = ItemType.Potions;
                break;
            case 501:
                _name = "Ring";
                _description = "";
                _value = 140;
                _amount = 1;
                _icon = "Potions/Mana";
                _mesh = "Potions/Mana";
                _type = ItemType.Potions;
                break;
            #endregion
            #region Scrolls 600 - 699
            case 600:
                _name = "Book";
                _description = "";
                _value = 25;
                _amount = 1;
                _icon = "Scrolls/Book";
                _mesh = "Scrolls/Bookv";
                _type = ItemType.Scrolls;
                break;
            case 601:
                _name = "Scroll";
                _description = "";
                _value = 10;
                _amount = 1;
                _icon = "Scrolls/Book";
                _mesh = "Scrolls/Book";
                _type = ItemType.Scrolls;
                break;
            #endregion
            #region Quests 700 - 799
            #endregion

            #region Default/Backup Item
            default:
                itemID = 0;
                _name = "Apple";
                _description = "Munchies & Crunchies";
                _value = 1;
                _amount = 1;
                _icon = "Food/Apple";
                _mesh = "Food/Apple";
                _type = ItemType.Food;
                break;
                #endregion
        }

        Item temp = new Item
        {
            ID = itemID,
            Name = _name,
            Description = _description,
            Value = _value,
            Amount = _amount,
            Type = _type,
            Icon = Resources.Load("Icons/" + _icon) as Image,
            Mesh = Resources.Load("Mesh/" + _mesh) as GameObject,
        };

        return temp;
    }
}