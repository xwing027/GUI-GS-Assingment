using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public ItemData[] items = new ItemData[10]; //this is the actual inventory here. can increase to make inv bigger
    public Button[] invButtons;
    public static InventoryManager invMan;
    public Sprite emptySlot;
    public GameObject invScreen;
    public static bool isInvActive;

    public ItemData[] body = new ItemData[2];
    public Image[] armourIcons;

    public GameObject extraScreen;

    private void Start()
    {
        //basically there should only be one inv manager
        if (invMan == null)
        {
            //if there isnt one, make this the manager
            invMan = this;
        }
        else
        {
            //otherwise, destroy it
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeybindsManager.keys["Inventory"])&&!PauseManager.isPaused)
        {
            isInvActive = !isInvActive;
            invScreen.SetActive(isInvActive);
            if (isInvActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
        
    }

    #region pickupitem functions
    public int FindAvailableStackSlot(PickupItem item) //btw int means it will return an int at the end of the function, void means nothing
    {
        for (int i = 0; i < items.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            if (item.data.itemName == items[i].itemName && items[i].count <= 10 -item.data.count)
            {
                Debug.Log("available slot found");
                return (i);
            }
        }
        return -1;
    }

    public int FindEmptySlot(PickupItem item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (items[i].itemName == "" || items[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }
    #endregion

    #region equipping item functions
    public int FindEquipSlot(Equipping equipItem)
    {
        for (int i = 0; i < body.Length; i++)
        {
            if (body[i].itemName == "" || body[i].itemName== null)
            {
                return (i);
            }
        }
        return -1;
    }
    #endregion

    #region armour slot display
    public void UpdateArmourSlot(int iconIndex)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType == "Armour")
            {
                armourIcons[iconIndex].GetComponent<Image>().sprite = items[iconIndex].icon;
            }
        }     
    }
    #endregion

    #region inventory slot display
    public void UpdateSlot(int buttonIndex) //buttonIndex here is the button number we're updating
    {
        #region update icon and text
        invButtons[buttonIndex].GetComponent<Image>().sprite = items[buttonIndex].icon; //get the matching icon and display

        if (items[buttonIndex].isStackable)
        {
            invButtons[buttonIndex].GetComponentInChildren<Text>().text = items[buttonIndex].count + ""; //get the matching name and display
        }
        else
        {
            invButtons[buttonIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }
        #endregion

        #region update functionality
        invButtons[buttonIndex].GetComponent<ClickableObject>().leftClick = items[buttonIndex].Use; //use on left click
        invButtons[buttonIndex].GetComponent<ClickableObject>().rightClick = items[buttonIndex].Drop; //drop from inv on right click

        items[buttonIndex].slot = buttonIndex; //set the item slot to the button index pos
        #endregion
    }

    public void ClearSlot(int index)
    {
        //remove item data
        items[index] = new ItemData();

        //remove icon
        invButtons[index].GetComponent<Image>().sprite = emptySlot;

        //remove text
        invButtons[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        invButtons[index].GetComponent<ClickableObject>().leftClick = null;
        invButtons[index].GetComponent<ClickableObject>().rightClick = null;
        invButtons[index].GetComponent<ClickableObject>().middleClick = null;
    }
    #endregion

    #region sorting buttons
    public void SortFood()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Food") //if its not food, hide it
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icon
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear; //hide text

                //stop functionality
                invButtons[i].GetComponent<ClickableObject>().leftClick = null;
                invButtons[i].GetComponent<ClickableObject>().rightClick = null;
                invButtons[i].GetComponent<ClickableObject>().middleClick = null;
            }
            if (items[i].itemType == "Food") //if it is food, show it (in case it was already hidden)
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<ClickableObject>().leftClick = items[i].Use; //use on left click
                invButtons[i].GetComponent<ClickableObject>().rightClick = items[i].Drop; //drop from inv on right click
            }
        } 
    }

    public void SortArmour()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Armour")
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icon
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                //stop functionality
                invButtons[i].GetComponent<ClickableObject>().leftClick = null;
                invButtons[i].GetComponent<ClickableObject>().rightClick = null;
                invButtons[i].GetComponent<ClickableObject>().middleClick = null;
            }
            if (items[i].itemType == "Armour")
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<ClickableObject>().leftClick = items[i].Use; //use on left click
                invButtons[i].GetComponent<ClickableObject>().rightClick = items[i].Drop; //drop from inv on right click
            }
        }
    }

    public void SortWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemType != "Weapon")
            {
                invButtons[i].GetComponent<Image>().color = Color.clear; //hide icon
                invButtons[i].GetComponentInChildren<Text>().color = Color.clear;

                //stop functionality
                invButtons[i].GetComponent<ClickableObject>().leftClick = null;
                invButtons[i].GetComponent<ClickableObject>().rightClick = null;
                invButtons[i].GetComponent<ClickableObject>().middleClick = null;
            }
            if (items[i].itemType == "Weapon")
            {
                //get icons back
                invButtons[i].GetComponent<Image>().color = Color.white;
                invButtons[i].GetComponentInChildren<Text>().color = Color.white; //hide text

                //bring back functionality
                invButtons[i].GetComponent<ClickableObject>().leftClick = items[i].Use; //use on left click
                invButtons[i].GetComponent<ClickableObject>().rightClick = items[i].Drop; //drop from inv on right click
            }
        }
        
    }

    public void ShowAll()
    {
        for (int i = 0; i < items.Length; i++)
        {
            //get icons back
            invButtons[i].GetComponent<Image>().color = Color.white;
            invButtons[i].GetComponentInChildren<Text>().color = Color.white;

            //bring back functionality
            invButtons[i].GetComponent<Button>().interactable = true;
        }
    }
    #endregion

    public void ShowOptions()
    {
        extraScreen.SetActive(true);
    }
}
