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
    ItemTypes type;

    //ui elements
    public GameObject inventory;
    public Transform contentContainer;
    public GameObject buttonPrefab;

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
    
    [System.Serializable]
    public struct ItemButtons
    {
        public ItemTypes itemTypes;
        public int itemTypeIndex; //corresponds to the item type enum
        public Text itemDisplay;
        public GameObject itemButton;
    }
    public ItemButtons[] itemButtons;

    public struct ItemInformation
    {
        public ItemTypes itemTypes;
    }
    public ItemInformation[] itemInformation;
    #endregion

    void Start()
    {
        type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);
    }

    private void Update()
    {
        //comment out when done
        #region Debugging button
        //this is just here to test if the inventory works
        if (Input.GetKey(KeyCode.Equals))
        {
            inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
        }
        #endregion

        if (Input.GetKeyDown(KeybindsManager.keys["Inventory"]) && !PauseManager.isPaused)
        {   //if we press inventory button and are not on pause menu
            if (inventory.activeSelf == false) //show inv
            {
                showInv = true;
                
                if (selectedItem != null) //if an item has been selected
                {
                    Debug.Log("item has been selected");
                    ItemInfo();
                }

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

    public void SortButtons(int i)
    {   //used to sort the buttons
        sortType = typeNames[i]; //set the sorting type
        Debug.Log("Sorting: " + sortType);
        SortItems();
    }

    void SortItems()
    {
        if (!(sortType == "All" || sortType == "")) //if the items have been sorted...
        {
            int a = 0; //amount of this type
            int s = 0; //new slot position of the item

            for (int i = 0; i < inv.Count; i++) //find all items of type in our inv
            {
                if (inv[i].ItemType == type) //if current element matches type
                {
                    a++; //add amount to this type
                    s++;
                    for (int button = 0; button < itemButtons.Length; button++)
                    {
                        itemButtons[button].itemButton.SetActive(true);
                    }
                    
                }
            }
        }
        else //if all is selected then just show all
        {
            for (int button = 0; button < inv.Count; button++)
            {
                itemButtons[button].itemButton.SetActive(true);
            }
        }
    }

    void ItemInfo()
    {
        
    }

    public void SelectItemButton()
    {   //this is attached to the button that appears in the scrollview
        for (int i = 0; i < inv.Count; i++)
        {
            if (inv[i].ItemType == type)
            {
                selectedItem = inv[i];
            }
        }
    }

    public void EquipButton()
    {

    }
}
