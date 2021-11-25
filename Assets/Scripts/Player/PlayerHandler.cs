using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : Stats
{
    [Header("Damage Flash and Death")]
    public Image damageImage;
    public Image deathImage;
    public Text deathText;
    public AudioClip deathClip;
    public AudioSource playersAudio;
    public Transform currentCheckPoint;

    public Color flashColour = new Color(1, 0, 0, 0.7f);
    public float flashSpeed = 10f;
    public static bool isDead;
    public bool isDamaged;
    public bool canHeal;
    public float healDelayTimer;
    int constitutionMultiplier;

    private void Start()
    {
        constitutionMultiplier = characterStats[2].tempValue + characterStats[2].tempCustomValue + characterStats[2].value;
    }

    private void Update()
    {
        #region Debug Buttons
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            DamagePlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            attributes[1].currentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            attributes[2].currentValue -= 10;
        }
        #endregion
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].displayImage.fillAmount = Mathf.Clamp01(attributes[i].currentValue / attributes[i].maxValue);
        }

        #region Damage Flash
        if (isDamaged && !isDead)
        {
            damageImage.color = flashColour;
            isDamaged = false;
        }
        else if (damageImage.color.a > 0)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        #endregion

        #region Can Heal
        if (!canHeal)
        {
            //if we can't heal, start counting up
            healDelayTimer += Time.deltaTime;
            if (healDelayTimer >= 5)
            {
                canHeal = true;
            }
        }
        if (canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue > 0)
        {
            RegenOverTime(0);
        }
        #endregion
    }

    void DeathText()
    {
        deathText.text = "You died...";
    }

    void RespawnText()
    {
        deathText.text = "...But you have been reborn";
    }

    void Respawn()
    {
        //reset everything
        deathText.text = "";

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].currentValue = attributes[i].maxValue;
        }

        isDead = false;

        //load pos
        transform.position = currentCheckPoint.position;
        transform.rotation = currentCheckPoint.rotation;

        //respawn
        deathImage.GetComponent<Animator>().SetTrigger("Respawn");
    }

    void Death()
    {
        isDead = true;
        deathText.text = "";

        playersAudio.clip = deathClip;
        playersAudio.Play();

        deathImage.GetComponent<Animator>().SetTrigger("isDead");

        Invoke("DeathText", 2f);
        Invoke("RespawnText", 6f);
        Invoke("Respawn", 9f);
    }

    public void RegenOverTime(int valueIndex)
    {
        attributes[valueIndex].currentValue += Time.deltaTime * (attributes[valueIndex].regenValue * constitutionMultiplier);
    }

    public void DamagePlayer(float damage)
    {
        isDamaged = true;

        //take damage
        attributes[0].currentValue -= damage;

        //delay regen
        canHeal = false;
        healDelayTimer = 0;
        if (attributes[0].currentValue <= 0 && !isDead) //if points are zero and we're not already dead
        {
            Death(); //run death
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint")) //once in check point
        {
            currentCheckPoint = other.transform; //set checkpoint
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 7; //temp boost regen
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint")) //once leaving check point
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 7; //remove temp boost
            }
        }
    }
}
