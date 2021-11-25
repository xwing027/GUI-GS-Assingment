using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        #region Variables
        string name = "";
        string description = "";
        int amount = 0;
        int value = 0;

        string icon = "";
        string mesh = "";
        ItemTypes type = ItemTypes.Misc;

        int heal = 0;
        int armour = 0;
        int damage = 0;
        #endregion

        switch (itemID)
        {
            #region Armour 0-99
            case 0:
                name = "Rags"; //get the name
                description = "A mouldy piece of cloth."; //the description
                amount = 1; //how many
                value = 1; //the cost
                icon = "Armour/Rags"; //where to find the icon
                mesh = "Armour/Rags"; //whree to find the mesh
                type = ItemTypes.Armour; //what type to sort it as
                armour = 1; //how much protection it offers
                break;
            case 1:
                name = "Sheild";
                description = "A sturdy wooden shield.";
                amount = 1;
                value = 15;
                icon = "Armour/Shield";
                mesh = "Armour/Shield";
                type = ItemTypes.Armour;
                armour = 5;
                break;
            case 2:
                name = "Iron Helmet";
                description = "An iron helm.";
                amount = 1;
                value = 15;
                icon = "Armour/IronHelmet";
                mesh = "Armour/Iron Helmet";
                type = ItemTypes.Armour;
                armour = 5;
                break;
            case 3:
                name = "Leather Chestplate";
                description = "A simple chestplate made from leather.";
                amount = 1;
                value = 10;
                icon = "Armour/LeatherChestplate";
                mesh = "Armour/LeatherChestplate";
                type = ItemTypes.Armour;
                armour = 10;
                break;
            case 4:
                name = "Iron Boots";
                description = "Boots plated with iron.";
                amount = 1;
                value = 13;
                icon = "Armour/IronBoots";
                mesh = "Armour/IronBoots";
                type = ItemTypes.Armour;
                armour = 5;
                break;
            case 5:
                name = "Iron Gloves";
                description = "Armoured gloves made from iron.";
                amount = 1;
                value = 13;
                icon = "Armour/IronGloves";
                mesh = "Armour/IronGloves";
                type = ItemTypes.Armour;
                armour = 5;
                break;
            case 6:
                name = "Iron Shoulder Plates";
                description = "Shoulder plates made from iron.";
                amount = 1;
                value = 5;
                icon = "Armour/IronShoulderPlates";
                mesh = "Armour/IronShoulderPlates";
                type = ItemTypes.Armour;
                armour = 5;
                break;
            case 7:
                name = "Cloak";
                description = "A cloak made from wool.";
                amount = 1;
                value = 10;
                icon = "Armour/Cloak";
                mesh = "Armour/Cloak";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            case 8:
                name = "Ring";
                description = "A small golden ring.";
                amount = 1;
                value = 5;
                icon = "Armour/Ring";
                mesh = "Armour/Ring";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            case 9:
                name = "Belt";
                description = "A belt. Not much protection, but will keep your pants up.";
                amount = 1;
                value = 5;
                icon = "Armour/Belt";
                mesh = "Armour/Belt";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            case 10:
                name = "Bracers";
                description = "Armour for your wrists.";
                amount = 1;
                value = 5;
                icon = "Armour/Bracers";
                mesh = "Armour/Bracers";
                type = ItemTypes.Armour;
                armour = 5;
                break;
            case 11:
                name = "Pants";
                description = "Some simple brown pants.";
                amount = 1;
                value = 5;
                icon = "Armour/Pants";
                mesh = "Armour/Pants";
                type = ItemTypes.Armour;
                armour = 3;
                break;
            case 12:
                name = "Necklace";
                description = "A silver necklace with a blue gem.";
                amount = 1;
                value = 5;
                icon = "Armour/Necklace";
                mesh = "Armour/Necklace";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            case 13:
                name = "Bag";
                description = "A simple bag for holding things.";
                amount = 1;
                value = 5;
                icon = "Armour/Bag";
                mesh = "Armour/Bag";
                type = ItemTypes.Armour;
                armour = 1;
                break;
            #endregion

            #region Weapon 100-199
            case 100:
                name = "Sword";
                description = "A regular longsword.";
                amount = 1;
                value = 10;
                damage = 15;
                icon = "Weapon/Sword";
                mesh = "Weapon/Sword";
                type = ItemTypes.Weapon;
                break;
            case 101:
                name = "Axe";
                description = "An axe for cutting wood, or your enemies.";
                amount = 1;
                value = 12;
                damage = 17;
                icon = "Weapon/Axe";
                mesh = "Weapon/Axe";
                type = ItemTypes.Weapon;
                break;
            case 102:
                name = "Bow";
                description = "A lightweight bow made from wood.";
                amount = 1;
                value = 7;
                damage = 12;
                icon = "Weapon/Bow";
                mesh = "Weapon/Bow";
                type = ItemTypes.Weapon;
                break;
            #endregion

            #region Potion 200-299
            case 200:
                name = "Health Potion";
                description = "A red potion that heals you.";
                amount = 1;
                value = 5;
                heal = 10;
                icon = "Potion/HP";
                mesh = "Potion/HP";
                type = ItemTypes.Potion;
                break;
            case 201:
                name = "Mana Potion";
                description = "A blue potion that restores your mana.";
                amount = 1;
                value = 6;
                heal = 10;
                icon = "Potion/MP";
                mesh = "Potion/MP";
                type = ItemTypes.Potion;
                break;
            #endregion

            #region Food 300-399
            case 300:
                name = "Apple";
                description = "A tasty looking red apple.";
                amount = 1;
                value = 5;
                heal = 5;
                icon = "Food/Apple";
                mesh = "Food/Apple";
                type = ItemTypes.Food;
                break;
            case 301:
                name = "Meat";
                description = "A hunk of cooked meat.";
                amount = 1;
                value = 5;
                heal = 10;
                icon = "Food/Meat";
                mesh = "Food/Meat";
                type = ItemTypes.Food;
                break;
            case 302:
                name = "Cabbage";
                description = "A bug-fixing cabbage.";
                amount = 1;
                value = 5;
                icon = "Food/Cabbage";
                mesh = "Food/Cabbage";
                type = ItemTypes.Food;
                break;
            #endregion

            #region Ingredient 400-499

            #endregion

            #region Craftable 500-599
            case 500:
                name = "Gem";
                description = "A small gem. Maybe you could use it for something.";
                amount = 1;
                value = 5;
                icon = "Craftable/Gem";
                mesh = "Craftable/Gem";
                type = ItemTypes.Craftable;
                break;
            case 501:
                name = "Iron Ingots";
                description = "Some iron ingots.";
                amount = 1;
                value = 10;
                icon = "Craftable/IronIngots";
                mesh = "Craftable/IronIngots";
                type = ItemTypes.Craftable;
                break;
            #endregion

            #region Money 600-699
            case 600:
                name = "Silver Coins";
                description = "A handful of silver coins.";
                amount = 1;
                value = 5;
                icon = "Money/SilverCoins";
                mesh = "Money/SilverCoins";
                type = ItemTypes.Money;
                break;
            #endregion

            #region Quest 700-799
            #endregion

            #region Misc 800-899
            #endregion
            default: //a default item that will replace any items that have errors to prevent more from occuring
                name = "Cabbage";
                description = "A bug-fixing cabbage.";
                amount = 1;
                value = 5;
                icon = "Food/Cabbage";
                mesh = "Food/Cabbage";
                type = ItemTypes.Food;
                break;
        }

        Item temp = new Item
        {   //set the variables to hold the information
            ID = itemID,
            Name = name,
            Description = description,
            Amount = amount,
            Value = value,
            Damage = damage,
            Armour = armour,
            Heal = heal,
            ItemType = type,
            IconName = Resources.Load("Icons/" + icon) as Texture2D,
            MeshName = Resources.Load("Mesh/" + mesh) as GameObject
        };
        return temp;
    }
}
