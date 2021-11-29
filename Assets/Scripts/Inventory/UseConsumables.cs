using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseConsumables : MonoBehaviour
{
    PlayerHandler playerH;
    HealthPotion hpScript;
    public Image[] cooldownTimer;
    float duration = 5f;
    float normalisedTime = 0;
    bool isCooldown;

    private void Start()
    {
        playerH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    void Update()
    {
        int slot;
        Debug.Log("before press "+isCooldown);
        if (InventoryManager.invMan.isThereCon || isCooldown==false)//slot isnt empty cooldown isnt happening
        {
            Debug.Log("after check "+isCooldown);
            if (Input.GetKeyDown(KeyCode.Alpha1)) //if input the corresponding position on the hotbar
            {
                if (isCooldown == false) //if no other cooldown is happenin
                {
                    StartCoroutine(Cooldown(0)); //start cooldown
                }

                slot = 0; //matching slot to the consumable in the consumable array
                Debug.Log("slot 1 used");
                //increase stat
                if (InventoryManager.invMan.consumables[slot].itemName == "Health Potion") //for the different potion types
                {
                    //get the player attribute relevant (e.g. health) and add bonus
                    playerH.attributes[0].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Stamina Potion")
                {                   
                    playerH.attributes[1].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Mana Potion")
                {                   
                    playerH.attributes[2].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }

                //get the relevant index consumable and decrease count           
                InventoryManager.invMan.consumables[0].count--;
                if (InventoryManager.invMan.consumables[0].count == 0) //if count is now 0
                {
                    InventoryManager.invMan.ClearConSlot(slot); //reset slot
                }
                else
                {
                    InventoryManager.invMan.UpdateConSlot(slot); //otherwise, update the count appearance
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (isCooldown == false)
                {
                    StartCoroutine(Cooldown(1));
                }
                slot = 1;
                Debug.Log("slot 2 used");
                if (InventoryManager.invMan.consumables[slot].itemName == "Health Potion")
                {
                    playerH.attributes[0].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Stamina Potion")
                {
                    playerH.attributes[1].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Mana Potion")
                {
                    playerH.attributes[2].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                //get the 1st index item and decrease count           
                InventoryManager.invMan.consumables[1].count--;
                if (InventoryManager.invMan.consumables[1].count == 0)
                {
                    InventoryManager.invMan.ClearConSlot(slot);
                }
                else
                {
                    InventoryManager.invMan.UpdateConSlot(slot);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (isCooldown == false)
                {
                    StartCoroutine(Cooldown(2));
                }
                slot = 2;
                Debug.Log("slot 3 used");
                if (InventoryManager.invMan.consumables[slot].itemName == "Health Potion")
                {
                    playerH.attributes[0].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Stamina Potion")
                {
                    playerH.attributes[1].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Mana Potion")
                {
                    playerH.attributes[2].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                //get the 1st index item and decrease count           
                InventoryManager.invMan.consumables[2].count--;
                if (InventoryManager.invMan.consumables[2].count == 0)
                {
                    InventoryManager.invMan.ClearConSlot(slot);
                }
                else
                {
                    InventoryManager.invMan.UpdateConSlot(slot);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (isCooldown == false)
                {
                    StartCoroutine(Cooldown(3));
                }
                slot = 3;
                Debug.Log("slot 4 used");
                if (InventoryManager.invMan.consumables[slot].itemName == "Health Potion")
                {
                    playerH.attributes[0].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Stamina Potion")
                {
                    playerH.attributes[1].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                if (InventoryManager.invMan.consumables[slot].itemName == "Mana Potion")
                {
                    playerH.attributes[2].currentValue += InventoryManager.invMan.consumables[slot].itemBonus;
                }
                //get the 1st index item and decrease count           
                InventoryManager.invMan.consumables[3].count--;
                if (InventoryManager.invMan.consumables[3].count == 0)
                {
                    InventoryManager.invMan.ClearConSlot(slot);
                }
                else
                {
                    InventoryManager.invMan.UpdateConSlot(slot);
                }
            }

        }
        else
        {
            Debug.Log("con isnt working ig");
        }
    }

    public IEnumerator Cooldown(int slot)
    {
        isCooldown = true;
        duration = 5f;
        normalisedTime = 0;
        while (normalisedTime<=1f)
        {
            cooldownTimer[slot].fillAmount = normalisedTime;
            normalisedTime += Time.deltaTime / duration;
            yield return null;
        }
        cooldownTimer[slot].fillAmount = 0f;
        isCooldown = false;
        Debug.Log("cooldown finished");
        yield return null;
    }
}
