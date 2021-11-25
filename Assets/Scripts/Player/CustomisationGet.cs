using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisationGet : MonoBehaviour
{
    public Renderer character, helmet;
    public PlayerHandler player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();


        Load();
    }

    void Load()
    {
        player.name = PlayerPrefs.GetString("CharacterName");
        player.gameObject.name = player.name;

        //set the texture for each area in player prefs using the index
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        SetTexture("Helmet", PlayerPrefs.GetInt("HelmetIndex"));

        for (int i = 0; i < player.characterStats.Length; i++)
        {
            player.characterStats[i].value = PlayerPrefs.GetInt(player.characterStats[i].name);
        }
    }

    void SetTexture(string type, int index)
    {
        Texture2D texture = null; //default texture is null
        int materialIndex = 0; //default material index is 0 
        Renderer rend = new Renderer();

        switch (type)
        {
            case "Skin": //for the skin texture
                texture = Resources.Load("Character/Skin_" + index) as Texture2D; //find skin textures in the character folder
                materialIndex = 1; //set the index as 1
                rend = character; //use the character's renderer to show the selection
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                materialIndex = 2;
                rend = character;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                materialIndex = 3;
                rend = character;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                materialIndex = 4;
                rend = character;
                break;
            case "Clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                materialIndex = 5;
                rend = character;
                break;
            case "Armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                materialIndex = 6;
                rend = character;
                break;
            case "Helmet":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                materialIndex = 1;
                rend = helmet;
                break;
        }

        Material[] mats = rend.materials; //put the rendered materials in a list
        mats[materialIndex].mainTexture = texture; //set the texture to the material selected in the list
        rend.materials = mats; //render the material

    }
}
