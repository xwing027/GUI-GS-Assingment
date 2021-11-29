using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Equipping : MonoBehaviour
{
    //armour
    public ItemData[] body = new ItemData[2]; //this is the actual inventory here. can increase to make inv bigger
    public Image[] armourIcons;
    public static Equipping equipScript;
    public Sprite emptyEquipSlot;

    //weapon
    public ItemData[] hands = new ItemData[2];
    public Image[] weaponIcons;
    public Sprite emptyWeaponSlot;

    private void Start()
    {
        if (equipScript == null)
        {
            //if there isnt one, make this the manager
            equipScript = this;
        }
        else
        {
            //otherwise, destroy it
            Destroy(this);
        }
    }

    #region Armour
    public void UpdateEquipSlot(int index)
    {
        armourIcons[index].GetComponent<Image>().sprite = body[index].icon;
    }
    #endregion

    #region Weapons
    public void UpdateWSlot(int index)
    {
        weaponIcons[index].GetComponent<Image>().sprite = hands[index].icon;
    }   
    #endregion
}
