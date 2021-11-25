using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Variables
    public static List<Item> inv = new List<Item>(); //handles the inventory
    public static bool showInv;
    public Item selectedItem;
    public static int money;

    //ui elements
    public GameObject inventory;

    public string sortType = "All";            //all the sort types
    public string[] typeNames = new string[8] { "All", "Armour", "Weapon", "Potion", "Food", "Ingredient", "Craftable", "Misc" };
    public Transform dropLocation; //spawn location for discarding items
    [System.Serializable]
    public struct EquippedItems
    {
        public string slotName;
        public Transform equipLocation; 
        public GameObject equippedItem;
    };
    public EquippedItems[] equippedItemSlot;
    #endregion

    private void Update()
    {
        //comment out when done
        #region Debugging button
        //this is just here to test if the inventory works
        if (Input.GetKey(KeyCode.I))
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
        }
        #endregion

        if (Input.GetKeyDown(KeybindsManager.keys["Inventory"]) && !PauseManager.isPaused)
        {   //if we press inventory button and are not on pause menu
            if (inventory.activeSelf == false) //show inv
            {
                showInv = true;
                inventory.SetActive(true);
                //cursor can be seen
                Cursor.visible = true;
                //cursor not locked
                Cursor.lockState = CursorLockMode.None;
                //time paused
                Time.timeScale = 0f;
            }
            else //if inv is already shown hide it
            {
                showInv = false;
                inventory.SetActive(false);
                //cursor cannot be seen
                Cursor.visible = false;
                //cursor is locked
                Cursor.lockState = CursorLockMode.Locked;
                //time is not paused
                Time.timeScale = 1f;
            }
        }   
    }

    public void SortButtons()
    {
        for (int i = 0; i < typeNames.Length; i++) //for each type
        {
            if (true) //if the label matches the type name
            {
                sortType = typeNames[i]; //set the sorting type
                Debug.Log("Sorting: " + sortType);
            }
        }
        if (selectedItem!= null)
        {
            ItemInfo();
        }
    }

    void SortItems()
    {
        if (!(sortType == "All" || sortType == ""))
        {
            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
            int a = 0; //amount of this type
            int s = 0; //new slot position of the item

            for (int i = 0; i < inv.Count; i++) //find all items of type in our inv
            {
                if (inv[i].ItemType == type) //if current element matches type
                {
                    a++; //add amount to this type
                }
            }
        }
    }

    void DisplayItems()
    {

    }

    void ItemInfo()
    {

    }
}
