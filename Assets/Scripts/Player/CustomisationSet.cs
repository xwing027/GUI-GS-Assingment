using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomisationSet : Stats
{
    #region Variables

    [Header("Texture List")]
    public List<Texture2D> skin = new List<Texture2D>();    //1
    public List<Texture2D> mouth = new List<Texture2D>();   //2
    public List<Texture2D> eyes = new List<Texture2D>();    //3
    public List<Texture2D> hair = new List<Texture2D>();    //4
    public List<Texture2D> clothes = new List<Texture2D>(); //5
    public List<Texture2D> armour = new List<Texture2D>();  //6

    [Header("Index")]
    //the position each part of the character is in, in their respective lists
    public int skinIndex;
    public int mouthIndex, eyesIndex, hairIndex, clothesIndex, armourIndex, helmetIndex;

    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;
    public Renderer helmetMesh;

    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int mouthMax, eyesMax, hairMax, clothesMax, armourMax;

    [Header("Character Name")]
    public string characterName;

    public GameObject inputField;
    
    //the name of each part the character can customise
    public string[] materialNames = new string[7] { "Skin", "Mouth", "Eyes", "Hair", "Clothes", "Armour", "Helmet" };
    public string[] attributeName = new string[3] { "Health", "Stamina", "Mana" };
    public string[] statName = new string[6] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };

    public int bonusStats = 6; //bonus stat points the player can use

    [Header("Display")]
    public Text points;
    public Text sText, dText, cText, iText, wText, chText;

    [System.Serializable]
    public struct PointUI
    { 
        public GameObject plusButton;
        public GameObject minusButton;
    };
    public PointUI[] pointSystem;
    #endregion

    void Start()
    {
        for (int i = 0; i < attributeName.Length; i++) //for the length of the attributes name array
        {
            attributes[i].name = attributeName[i]; //set the name from the attributes name array to the regular attributes array
        }
        for (int i = 0; i < statName.Length; i++) //same as above
        {
            characterStats[i].name = statName[i];
        }

        #region for loop to pull textures from file
        //for loop looping from 0 to the max
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            //add our temp texture that we just found to the skin List - same for the rest of the loops
            skin.Add(temp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(temp);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(temp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(temp);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(temp);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(temp);
        }
        #endregion

        //connect and find the renderer that's in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<Renderer>();
        helmetMesh = GameObject.Find("cap").GetComponent<Renderer>();

        #region Set Texture
        //sets each texture default to be the first index in the list, 0
        SetTexture("Skin", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Hair", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        #endregion

        #region Class Dropdown
        ChooseClass(0);
        var classDropdown = GameObject.Find("Class").GetComponent<Dropdown>(); //grab the drop down
        classDropdown.options.Clear(); //clear options just in case

        CharacterClass[] classes = (CharacterClass[])Enum.GetValues(typeof(CharacterClass)); //put the classes to an array

        foreach (var item in classes)
        {   
            //put the items from the array into the drop down options
            classDropdown.options.Add(new Dropdown.OptionData() { text = item.ToString() }); 
        }
        #endregion

        #region Race Dropdown
        ChooseRace(0);
        //works the same as the class dropdown
        var raceDropdown = GameObject.Find("Race").GetComponent<Dropdown>();
        raceDropdown.options.Clear();

        CharacterRace[] races = (CharacterRace[])Enum.GetValues(typeof(CharacterRace));

        foreach (var item in races)
        {
            raceDropdown.options.Add(new Dropdown.OptionData() { text = item.ToString() });
        }
        #endregion

        points.text = "Points: "+ bonusStats;
    }

    public void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, materialIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        Renderer curRend = new Renderer();

        #region Switch Material
        switch (type)
        {
            case "Skin":
                index = skinIndex; //use the skin index
                max = skinMax; //set the max for the list
                textures = skin.ToArray(); //convert the texture to an array
                materialIndex = 1; //set the material index to 1
                curRend = character; //the renderer is the character's renderer
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                materialIndex = 2;
                curRend = character;
                break;
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                materialIndex = 3;
                curRend = character;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                materialIndex = 4;
                curRend = character;
                break;
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                materialIndex = 5;
                curRend = character;
                break;
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                materialIndex = 6;
                curRend = character;
                break;
            case "Helmet":
                index = helmetIndex;
                max = armourMax;
                textures = armour.ToArray();
                materialIndex = 1;
                curRend = helmetMesh;
                break;
        }
        #endregion

        #region Assign Direction
        index += dir;

        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }

        Material[] mat = curRend.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[materialIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        curRend.materials = mat;
        #endregion

        #region Set Material Switch
        //another switch that is goverened by the same string name of our material
        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyesIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;
                break;
            case "Armour":
                armourIndex = index;
                break;
            case "Helmet":
                helmetIndex = index;
                break;
        }
        #endregion
    }

    public void ChooseClass(int classIndex)
    {
        //a switch statement that holds all the information for each class stats
        switch (classIndex)
        {
            case 0:
                characterStats[0].value = 15; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 13; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 14; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue + characterStats[2].tempCustomValue);
                characterStats[3].value = 12; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 8; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 12; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Barbarian;
                break;
            case 1:
                characterStats[0].value = 10;
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 13;
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 8;
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 14;
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 12;
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 15;
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Bard;
                break;
            case 2:
                characterStats[0].value = 8;
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 14;
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 13;
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 12;
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 15;
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 10;
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Cleric;
                break;
            case 3:
                characterStats[0].value = 10; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 13; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 14; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 12; //int
                //iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 15; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 8; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Druid;
                break;
            case 4:
                characterStats[0].value = 12; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 15; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 14; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 10; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 13; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 8; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Monk;
                break;
            case 5:
                characterStats[0].value = 15; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 8; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 13; //con
                //cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 10; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 12; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 14; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Paladin;
                break;
            case 6:
                characterStats[0].value = 10; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 15; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 14; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 12; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 13; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 8; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Ranger;
                break;
            case 7:
                characterStats[0].value = 8; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 15; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 14; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 10; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 13; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 12; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Rogue;
                break;
            case 8:
                characterStats[0].value = 8; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 14; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 13; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 12; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 10; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 15; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Sorcerer;
                break;
            case 9:
                characterStats[0].value = 8; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 15; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 13; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 10; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 12; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 14; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Warlock;
                break;
            case 10:
                characterStats[0].value = 8; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].value = 13; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].value = 14; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].value = 15; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].value = 12; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].value = 10; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterClass = CharacterClass.Wizard;
                break;

        }
    }

    public void ChooseRace(int raceIndex)
    {
        //switch statement that holds the additional stat boost for each stat for each race
        switch (raceIndex)
        {
            case 0:
                characterStats[0].tempValue = 2; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 0; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 0; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 0; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 4; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Dragonborn;
                
                break;
            case 1:
                characterStats[0].tempValue = 2; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 0; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 4; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 0; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 0; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Dwarf;
                break;
            case 2:
                characterStats[0].tempValue = 0; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 4; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 0; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 2; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 0; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Elf;
                break;
            case 3:
                characterStats[0].tempValue = 0; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 3; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 0; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 3; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 0; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Gnome;
                break;
            case 4:
                characterStats[0].tempValue = 0; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 0; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 3; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 0; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 3; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.HalfElf;
                break;
            case 5:
                characterStats[0].tempValue = 0; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 0; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 3; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 0; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 3; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Halfling;
                break;
            case 6:
                characterStats[0].tempValue = 4; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 0; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 2; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 0; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 0; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.HalfOrc;
                break;
            case 7:
                characterStats[0].tempValue = 1; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 1; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 1; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 1; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 1; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 1; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Human;
                break;
            case 8:
                characterStats[0].tempValue = 0; //str
                sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[0].tempCustomValue);
                characterStats[1].tempValue = 0; //dex
                dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
                characterStats[2].tempValue = 0; //con
                cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
                characterStats[3].tempValue = 3; //int
                iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
                characterStats[4].tempValue = 0; //wis
                wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
                characterStats[5].tempValue = 3; //cha
                chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);
                characterRace = CharacterRace.Tiefling;
                break;
        }
    }

    #region Buttons/Ui Attachement
    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex); //set each index to match the current texture index
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("HelmetIndex", helmetIndex);

        PlayerPrefs.SetString("CharacterName", characterName); //save the character name
        PlayerPrefs.SetString("CharacterClass", characterClass.ToString()); //save the chosen class
        PlayerPrefs.SetString("CharacterRace", characterRace.ToString()); //save the chosen race

        for (int i = 0; i < characterStats.Length; i++) //for the info in character stats
        {
            PlayerPrefs.SetInt(characterStats[i].name, (characterStats[i].value + characterStats[i].tempValue + characterStats[i].tempCustomValue)); //save the name and values
        }
    }

    public void SetTextureRight(string type)
    {
        SetTexture(type, 1);
    }

    public void SetTextureLeft(string type)
    {
        SetTexture(type, -1);
    }

    public void Random()
    {
        //randomly select a texture within the max range
        SetTexture("Skin", UnityEngine.Random.Range(0, skinMax - 1)); 
        SetTexture("Mouth", UnityEngine.Random.Range(0, mouthMax - 1));
        SetTexture("Eyes", UnityEngine.Random.Range(0, eyesMax - 1));
        SetTexture("Hair", UnityEngine.Random.Range(0, hairMax - 1));
        SetTexture("Clothes", UnityEngine.Random.Range(0, clothesMax - 1));
        SetTexture("Armour", UnityEngine.Random.Range(0, armourMax - 1));
        SetTexture("Helmet", UnityEngine.Random.Range(0, armourMax - 1));
    }

    public void ResetButton()
    {
        //reset texture to the default 0 index
        SetTexture("Skin", skinIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Eyes", eyesIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);
        SetTexture("Armour", armourIndex = 0);
        SetTexture("Helmet", helmetIndex = 0);
    }

    public void SaveAndPlay()
    {
        if (characterName!="" && bonusStats ==0)
        {
            SaveCharacter();
            SceneManager.LoadScene(2);
        }
    }

    public void MinusPoints(int i)
    { //set on button with -
        bonusStats++;
        characterStats[i].tempCustomValue--;

        if (characterStats[i].tempCustomValue <= 0)
        {
            pointSystem[i].minusButton.SetActive(false);           
        }
        if (pointSystem[i].plusButton.activeSelf == false)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(true);
            }
            
        }

        #region Update Text
        //i am aware how messy this is 
        //pointSystem[i].nameDisplay.text = characterStats[i].name + ": " + (characterStats[i].value + characterStats[i].tempValue);
        sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[i].tempCustomValue);
        dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
        cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
        iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
        wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
        chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);

        points.text = "Points: " + bonusStats;
        #endregion
    }

    public void AddPoints(int i)
    {   //set on button with +
        bonusStats--; //take from bonus total
        characterStats[i].tempCustomValue++; //add to the selected skill

        if (bonusStats <= 0) //if there are no bonus stat points left
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(false); //hide the plus button cuz u got nothing to add
            }
        }
        if (pointSystem[i].minusButton.activeSelf == false) //if the minus button is hidden
        {
            pointSystem[i].minusButton.SetActive(true); //show it
        }

        #region Update Text
        //i am aware how messy this is but if it aint broke...
        //pointSystem[i].nameDisplay.text = characterStats[i].name + ": " + (characterStats[i].value + characterStats[i].tempValue);
        sText.text = "Strength: " + (characterStats[0].tempValue + characterStats[0].value + characterStats[i].tempCustomValue);
        dText.text = "Dexterity: " + (characterStats[1].tempValue + characterStats[1].value + characterStats[1].tempCustomValue);
        cText.text = "Constitution: " + (characterStats[2].tempValue + characterStats[2].value + characterStats[2].tempCustomValue);
        iText.text = "Intelligence: " + (characterStats[3].tempValue + characterStats[3].value + characterStats[3].tempCustomValue);
        wText.text = "Wisdom: " + (characterStats[4].tempValue + characterStats[4].value + characterStats[4].tempCustomValue);
        chText.text = "Charisma: " + (characterStats[5].tempValue + characterStats[5].value + characterStats[5].tempCustomValue);

        points.text = "Points: " + bonusStats;
        #endregion
    }

    public void SetName()
    {
        characterName = inputField.GetComponent<InputField>().text;
    }

    public void BackButton()
    { //ik this isn't relevant to the script but i didnt want to make a whole new script just for this
        SceneManager.LoadScene(0);
    }
    #endregion
}
