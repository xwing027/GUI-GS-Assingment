using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public ItemData[] items = new ItemData[20]; //this is the actual inventory here. can increase to make inv bigger
    public Button[] invButtons;
    public static InventoryManager invMan;
    public Sprite emptySlot;
    public GameObject invScreen;
    public static bool isInvActive;

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
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            if (InventoryManager.invMan.items[i].itemName == "" || InventoryManager.invMan.items[i].itemName == null)
            {
                return (i);
            }
        }
        return -1;
    }

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
}
