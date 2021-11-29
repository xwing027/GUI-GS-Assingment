using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    public ItemData[] shopItems = new ItemData[10]; //this is the actual inventory here. can increase to make inv bigger
    public Button[] shopButtons;
    public Text[] shopPrices;
    public string priceDisplay;
    public static Shop shopScript;
    public Sprite emptyShopSlot;
    public GameObject shopScreen;
    public static bool isShopActive;
    int itemIndex;
    int buyBonus = 3;

    private void Start()
    {
        //basically there should only be one inv manager
        if (shopScript == null)
        {
            //if there isnt one, make this the manager
            shopScript = this;
        }
        else
        {
            //otherwise, destroy it
            Destroy(this);
        }
        for (int i = 0; i < shopItems.Length; i++)
        {
            UpdateShopSlot(i);
        }
    }

    public void CloseShop()
    {
        shopScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isShopActive = false;
        InventoryManager.invMan.invScreen.SetActive(false);
    }

    public void OpenShop()
    {
        shopScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        isShopActive = true;
        InventoryManager.invMan.invScreen.SetActive(true);
    }

    public void UpdateShopSlot(int buttonIndex) //buttonIndex here is the button number we're updating
    {
        shopButtons[buttonIndex].GetComponent<Image>().sprite = shopItems[buttonIndex].icon; //get the matching icon and display

        if (shopItems[buttonIndex].isStackable)
        {
            shopButtons[buttonIndex].GetComponentInChildren<Text>().text = shopItems[buttonIndex].count + ""; //get the matching name and display
        }
        else
        {
            shopButtons[buttonIndex].GetComponentInChildren<Text>().text = ""; //dont display a number if there is only 1
        }
        if (shopItems[buttonIndex].icon == null)
        {
            shopButtons[buttonIndex].GetComponent<Image>().sprite = emptyShopSlot;
        }

        //update value slot
        shopPrices[buttonIndex].GetComponent<Text>().text = "$ "+ shopItems[buttonIndex].value.ToString();

        if (shopItems[buttonIndex].itemName == null || shopItems[buttonIndex].itemName == "")
        {
            shopButtons[buttonIndex].GetComponent<Button>().interactable = false;
        }
        else
        {
            //update functionality
            shopButtons[buttonIndex].GetComponent<Button>().interactable = true;
        }

        shopItems[buttonIndex].slot = buttonIndex; //set the item slot to the button index pos
    }

    public void ClearShopSlot(int index)
    {
        //remove item data
        shopItems[index] = new ItemData();

        //remove icon
        shopButtons[index].GetComponent<Image>().sprite = emptyShopSlot;

        //remove text
        shopButtons[index].GetComponentInChildren<Text>().text = "";

        //remove functionality (pressing buttons should do nothing)
        shopButtons[index].GetComponent<Button>().interactable = false;

        //remove price display
        shopPrices[index].GetComponent<Text>().text = "";
    }

    public int FindInvSlotStackShop(int item) //for stackable
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++) //loop through the inventory
        {
            //does the item we feed here have the same name as the item name in the item array (if so, its the same item)
            //AND if the slot we're checking has space to put all the items we've picked up
            //AND cost of item is not larger than the amount of money the player has
            if (shopItems[itemIndex].itemName == InventoryManager.invMan.items[i].itemName && InventoryManager.invMan.items[i].count <= 10 - shopItems[itemIndex].count && (shopItems[itemIndex].value+buyBonus) <= InventoryManager.invMan.playerMoney)
            {
                Debug.Log(shopItems[itemIndex].value! <= InventoryManager.invMan.playerMoney);
                return (i);
            }
        }
        return -1;
    }

    public int FindInvSlotShop(int item) //for non stack
    {
        for (int i = 0; i < InventoryManager.invMan.items.Length; i++)
        {
            //check if the item name is nothing (which will mean its empty)
            //AND the item value is not larger than the player money amount
            if ((InventoryManager.invMan.items[i].itemName == "" || InventoryManager.invMan.items[i].itemName == null) && (shopItems[itemIndex].value+buyBonus) <= InventoryManager.invMan.playerMoney)
            {
                return (i);
            }
        }
        return -1;
    }

    void OnShopClick()
    {
        #region Getting item info
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        bool buttonNameCheck = clickedButton.name == "0" || clickedButton.name == "1" || clickedButton.name == "2" || clickedButton.name == "3" || clickedButton.name == "4" || clickedButton.name == "5" || clickedButton.name == "6" || clickedButton.name == "7" || clickedButton.name == "8" || clickedButton.name == "9";
        if (buttonNameCheck)
        {
            itemIndex = Int32.Parse(clickedButton.name);
            Debug.Log("this button index is: " + itemIndex);
        }
        #endregion

        int slot;
        if (shopItems[itemIndex].isStackable)
        {
            slot = FindInvSlotStackShop(itemIndex);
            Debug.Log("stack slot: " + slot);
            if (slot >= 0)
            {
                InventoryManager.invMan.items[slot].count += shopItems[itemIndex].count;
                InventoryManager.invMan.UpdateSlot(slot);
                InventoryManager.invMan.UpdateDescription(slot);

                //InventoryManager.invMan.playerMoney -= shopItems[itemIndex].value;
                UpdateMoneyBuy(itemIndex);
                ClearShopSlot(itemIndex);
                
                return;
            }
            else
            {
                Debug.Log("no stack bye");
            }
        }
        slot = FindInvSlotShop(itemIndex);
        Debug.Log(slot);
        if (slot >= 0)
        {
            InventoryManager.invMan.items[slot] = shopItems[itemIndex];
            InventoryManager.invMan.UpdateSlot(slot);
            InventoryManager.invMan.UpdateDescription(slot);

            //InventoryManager.invMan.playerMoney -= shopItems[itemIndex].value;
            UpdateMoneyBuy(itemIndex);
            ClearShopSlot(itemIndex);
            
            return;
        }
    }

    public void UpdateMoneyBuy(int item)
    {
        InventoryManager.invMan.playerMoney -= shopItems[item].value+buyBonus;
    }
}
