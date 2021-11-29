/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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
    public int skinIndex;
    public int mouthIndex, eyesIndex,hairIndex,clothesIndex,armourIndex, helmetIndex;

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
    public string[] materialNames = new string[7] {"Skin", "Mouth", "Eyes", "Hair", "Clothes", "Armour", "Helmet"};

    public string[] attributeName = new string[3] { "Health", "Stamina", "Mana" };
    public string[] statName = new string[6] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" };

    public bool raceDrop;
    public string racedropDisplay = "Select Race";
    public bool classDrop;
    public string classdropDisplay = "Select Class";
    public Vector2 scrollPosRace, scrollPosClass;
    public int bonusStats = 6;
    
    #endregion

    private void Start()
    {
        for (int i = 0; i < attributeName.Length; i++) //3
        {
            attributes[i].name = attributeName[i];
        }
        for (int i = 0; i < statName.Length; i++) //6
        {
            characterStats[i].name = statName[i];
        }

        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_"+i)as Texture2D;

            //add our temp texture that we just found to the skin List
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

        #region do this after making the function SetTexture
        SetTexture("Skin", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Hair", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        #endregion
    }

    public void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, materialIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        Renderer curRend = new Renderer();

        #region Switch Material
        switch(type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                materialIndex = 1;
                curRend = character;
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
        if (index > max-1)
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
        //create another switch that is goverened by the same string name of our material
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

    //create a switch statement that holds the base stats
    void ChooseClass(int classIndex)
    {
        switch (classIndex)
        {
            //15,14,13,12,10,8
            case 0:
                characterStats[0].value = 15; //str
                characterStats[1].value = 13; //dex
                characterStats[2].value = 14; //con
                characterStats[3].value = 12; //int
                characterStats[4].value = 8; //wis
                characterStats[5].value = 12; //cha
                characterClass = CharacterClass.Barbarian;
                break;
            case 1:
                characterStats[0].value = 10; 
                characterStats[1].value = 13; 
                characterStats[2].value = 8; 
                characterStats[3].value = 14; 
                characterStats[4].value = 12; 
                characterStats[5].value = 15; 
                characterClass = CharacterClass.Bard;
                break;
            case 2:
                characterStats[0].value = 8;
                characterStats[1].value = 14;
                characterStats[2].value = 13;
                characterStats[3].value = 12;
                characterStats[4].value = 15;
                characterStats[5].value = 10;
                characterClass = CharacterClass.Cleric;
                break;
            case 3:
                characterStats[0].value = 10; //str
                characterStats[1].value = 13; //dex
                characterStats[2].value = 14; //con
                characterStats[3].value = 12; //int
                characterStats[4].value = 15; //wis
                characterStats[5].value = 8; //cha
                characterClass = CharacterClass.Druid;
                break;
            case 4:
                characterStats[0].value = 12; //str
                characterStats[1].value = 15; //dex
                characterStats[2].value = 14; //con
                characterStats[3].value = 10; //int
                characterStats[4].value = 13; //wis
                characterStats[5].value = 8; //cha
                characterClass = CharacterClass.Monk;
                break;
            case 5:
                characterStats[0].value = 15; //str
                characterStats[1].value = 8; //dex
                characterStats[2].value = 13; //con
                characterStats[3].value = 10; //int
                characterStats[4].value = 12; //wis
                characterStats[5].value = 14; //cha
                characterClass = CharacterClass.Paladin;
                break;
            case 6:
                characterStats[0].value = 10; //str
                characterStats[1].value = 15; //dex
                characterStats[2].value = 14; //con
                characterStats[3].value = 12; //int
                characterStats[4].value = 13; //wis
                characterStats[5].value = 8; //cha
                characterClass = CharacterClass.Ranger;
                break;
            case 7:
                characterStats[0].value = 8; //str
                characterStats[1].value = 15; //dex
                characterStats[2].value = 14; //con
                characterStats[3].value = 10; //int
                characterStats[4].value = 13; //wis
                characterStats[5].value = 12; //cha
                characterClass = CharacterClass.Rogue;
                break;
            case 8:
                characterStats[0].value = 8; //str
                characterStats[1].value = 14; //dex
                characterStats[2].value = 13; //con
                characterStats[3].value = 12; //int
                characterStats[4].value = 10; //wis
                characterStats[5].value = 15; //cha
                characterClass = CharacterClass.Sorcerer;
                break;
            case 9:
                characterStats[0].value = 8; //str
                characterStats[1].value = 15; //dex
                characterStats[2].value = 13; //con
                characterStats[3].value = 10; //int
                characterStats[4].value = 12; //wis
                characterStats[5].value = 14; //cha
                characterClass = CharacterClass.Warlock;
                break;
            case 10:
                characterStats[0].value = 8; //str
                characterStats[1].value = 13; //dex
                characterStats[2].value = 14; //con
                characterStats[3].value = 15; //int
                characterStats[4].value = 12; //wis
                characterStats[5].value = 10; //cha
                characterClass = CharacterClass.Wizard;
                break;

        }
    }

    void ChooseRace(int raceIndex)
    {
        switch (raceIndex)
        {
            case 0:
                characterStats[0].tempValue = 2; //str
                characterStats[1].tempValue = 0; //dex
                characterStats[2].tempValue = 0; //con
                characterStats[3].tempValue = 0; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 4; //cha
                characterRace = CharacterRace.Dragonborn;
                break;
            case 1:
                characterStats[0].tempValue = 2; //str
                characterStats[1].tempValue = 0; //dex
                characterStats[2].tempValue = 4; //con
                characterStats[3].tempValue = 0; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 0; //cha
                characterRace = CharacterRace.Dwarf;
                break;
            case 2:
                characterStats[0].tempValue = 0; //str
                characterStats[1].tempValue = 4; //dex
                characterStats[2].tempValue = 0; //con
                characterStats[3].tempValue = 2; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 0; //cha
                characterRace = CharacterRace.Elf;
                break;
            case 3:
                characterStats[0].tempValue = 0; //str
                characterStats[1].tempValue = 3; //dex
                characterStats[2].tempValue = 0; //con
                characterStats[3].tempValue = 3; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 0; //cha
                characterRace = CharacterRace.Gnome;
                break;
            case 4:
                characterStats[0].tempValue = 0; //str
                characterStats[1].tempValue = 0; //dex
                characterStats[2].tempValue = 3; //con
                characterStats[3].tempValue = 0; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 3; //cha
                characterRace = CharacterRace.HalfElf;
                break;
            case 5:
                characterStats[0].tempValue = 0; //str
                characterStats[1].tempValue = 0; //dex
                characterStats[2].tempValue = 3; //con
                characterStats[3].tempValue = 0; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 3; //cha
                characterRace = CharacterRace.Halfling;
                break;
            case 6:
                characterStats[0].tempValue = 4; //str
                characterStats[1].tempValue = 0; //dex
                characterStats[2].tempValue = 2; //con
                characterStats[3].tempValue = 0; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 0; //cha
                characterRace = CharacterRace.HalfOrc;
                break;
            case 7:
                characterStats[0].tempValue = 1; //str
                characterStats[1].tempValue = 1; //dex
                characterStats[2].tempValue = 1; //con
                characterStats[3].tempValue = 1; //int
                characterStats[4].tempValue = 1; //wis
                characterStats[5].tempValue = 1; //cha
                characterRace = CharacterRace.Human;
                break;
            case 8:
                characterStats[0].tempValue = 0; //str
                characterStats[1].tempValue = 0; //dex
                characterStats[2].tempValue = 0; //con
                characterStats[3].tempValue = 3; //int
                characterStats[4].tempValue = 0; //wis
                characterStats[5].tempValue = 3; //cha
                characterRace = CharacterRace.Tiefling;
                break;
        }
    }

    void SaveCharacter()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("HelmetIndex", helmetIndex);

        PlayerPrefs.SetString("CharacterName", characterName);
        PlayerPrefs.SetString("CharacterClass", characterClass.ToString());
        PlayerPrefs.SetString("CharacterRace", characterRace.ToString());

        for (int i = 0; i < characterStats.Length; i++)
        {
            PlayerPrefs.SetInt(characterStats[i].name,(characterStats[i].value + characterStats[i].tempValue + characterStats[i].levelTempValue));
        }
    }

    private void OnGUI()
    {
        //create the floats scrW and scrH that govern our 16:9 ratio
        Vector2 scr = new Vector2(Screen.width / 16, Screen.height / 9);
        //create an int that will help with shuffling your GUI elements under eachother

        #region Button and Display for custom
        for (int i = 0; i < materialNames.Length; i++)
        {
            //GUI button on the left of the screen
            if (GUI.Button(new Rect(0.25f*scr.x, 2.5f*scr.y +(i* 0.75f * scr.y), 1*scr.x, 0.75f * scr.y),"<"))
            {
                //button will run settexture and grab the material, move the texture index in the direction
                SetTexture(materialNames[i], -1);
            }

            //GUI Box or Label on the left of the screen
            GUI.Box(new Rect(1.25f * scr.x, 2.5f * scr.y + (i * 0.75f * scr.y), 1.75f * scr.x, 0.75f * scr.y),materialNames[i]);
           
            //GUI Button on the left of the screen
            if (GUI.Button(new Rect(3f * scr.x, 2.5f * scr.y + (i * 0.75f * scr.y), 1 * scr.x, 0.75f * scr.y), ">"))
            {
                SetTexture(materialNames[i], 1);
            }
        }
        #endregion

        #region Random/Reset
        //create 2 buttons one Random and one Reset
        if (GUI.Button(new Rect(13 * scr.x, 2.5f * scr.y, 2f * scr.x, 0.75f * scr.y), "Random"))
        {
            //Random will feed a random amount to the direction 
            SetTexture("Skin",UnityEngine.Random.Range(0, skinMax -1));
            SetTexture("Mouth", UnityEngine.Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", UnityEngine.Random.Range(0, eyesMax - 1));
            SetTexture("Hair", UnityEngine.Random.Range(0, hairMax - 1));
            SetTexture("Clothes", UnityEngine.Random.Range(0, clothesMax - 1));
            SetTexture("Armour", UnityEngine.Random.Range(0, armourMax - 1));
            SetTexture("Helmet", UnityEngine.Random.Range(0, armourMax - 1));
        }
        if (GUI.Button(new Rect(13 * scr.x, 3.25f * scr.y, 1.5f * scr.x, 0.75f * scr.y), "Reset"))
        {
            //reset will set all to 0 both use SetTexture
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetTexture("Helmet", helmetIndex = 0);
        }
        #endregion

        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        characterName = GUI.TextField(new Rect(4.5f * scr.x, 8 * scr.y, 3 * scr.x, 0.75f * scr.y),characterName, 100);
        
        if (characterName!="" && classdropDisplay !="" && racedropDisplay != "" && bonusStats ==0) //if the character's options have been set
        {
            if (GUI.Button(new Rect(0.25f * scr.x, 8 * scr.y, 3.75f * scr.x, 0.75f * scr.y), "Save and Play")) //show save and play button
            {
                SaveCharacter();
                SceneManager.LoadScene(2);
            }
        }
        
        #endregion

        #region Select Class
        //button for toggling dropdown
        if (GUI.Button(new Rect(13 * scr.x, 4.5f * scr.y, 3f * scr.x, 0.75f * scr.y),classdropDisplay))
        {
            classDrop = !classDrop;
        }
        //if dropdown - scroll view that displays our classes as selectable buttons 
        if (classDrop)
        {
            float listSize = System.Enum.GetNames(typeof(CharacterClass)).Length;
            scrollPosClass = GUI.BeginScrollView(new Rect(13 * scr.x, 5.5f * scr.y, 3f * scr.x, 3f * scr.y),scrollPosClass,new Rect(0,0,0,listSize *0.5f*scr.y));
            //scroll view content goes inbetween begin and end
            GUI.Box(new Rect(0, 0, 1.9f*scr.x,listSize* 0.5f * scr.y), "");
            for (int i = 0; i < listSize; i++)
            {
                if (GUI.Button(new Rect(0,0.5f*scr.y*i,1.9f*scr.x,0.5f*scr.y), System.Enum.GetNames(typeof(CharacterClass))[i]))
                {
                    ChooseClass(i); //runs choose Class
                    classdropDisplay = System.Enum.GetNames(typeof(CharacterClass))[i]; //changes button to display picked class
                    classDrop = false; //hides the dropdown once selected
                }
            }
            GUI.EndScrollView();
        }
        //when button is selected, apply class and stats - display stats
        #endregion

        #region Select Race
        if (!classDrop)
        {
            if (GUI.Button(new Rect(13 * scr.x, 5.25f * scr.y, 3f * scr.x, 0.75f * scr.y), racedropDisplay))
            {
                raceDrop = !raceDrop;
            }
            //if dropdown - scroll view that displays our classes as selectable buttons 
            if (raceDrop)
            {
                float listSize = System.Enum.GetNames(typeof(CharacterRace)).Length;
                scrollPosRace = GUI.BeginScrollView(new Rect(13 * scr.x, 6 * scr.y, 3f * scr.x, 3f * scr.y), scrollPosRace, new Rect(0, 0, 0, listSize * 0.5f * scr.y));
                //scroll view content goes inbetween begin and end
                GUI.Box(new Rect(0, 0, 1.9f * scr.x, listSize * 0.5f * scr.y), "");
                for (int i = 0; i < listSize; i++)
                {
                    if (GUI.Button(new Rect(0, 0.5f * scr.y * i, 1.9f * scr.x, 0.5f * scr.y), System.Enum.GetNames(typeof(CharacterRace))[i]))
                    {
                        ChooseRace(i);
                        racedropDisplay = System.Enum.GetNames(typeof(CharacterRace))[i]; 
                        raceDrop = false;
                    }
                }
                GUI.EndScrollView();
            }
        }

        #endregion

        #region Add Points
        //stats - display points
        if (!classDrop || !raceDrop)
        {
            //box for points to spend
            GUI.Box(new Rect(9 * scr.x, 3.5f * scr.y, 3f * scr.x, 0.75f * scr.y), "Points: " + bonusStats);

            //+ and - buttons on either side of box/label
            for (int i = 0; i < characterStats.Length; i++)
            {
                if (bonusStats < 6 && characterStats[i].levelTempValue > 0) //-
                {
                    if (GUI.Button(new Rect(8.25f * scr.x, 4.25f * scr.y + (i * 0.75f * scr.y), 0.75f * scr.x, 0.75f * scr.y),"-"))
                    {
                        bonusStats++;
                        characterStats[i].levelTempValue--;
                    }
                }

                //type
                //display total stats and stat name 
                GUI.Box(new Rect(9 * scr.x, 4.25f * scr.y + (i*0.75f*scr.y), 3f * scr.x, 0.75f * scr.y), characterStats[i].name + ": " + (characterStats[i].value + characterStats[i].tempValue + characterStats[i].levelTempValue));


                if (bonusStats >0) //+
                {
                    if (GUI.Button(new Rect(12 * scr.x, 4.25f * scr.y+(i * 0.75f * scr.y), 0.75f * scr.x, 0.75f * scr.y), "+"))
                    {
                        bonusStats--;
                        characterStats[i].levelTempValue++;
                    }
                }

            }
        }
        #endregion
    }
}

*/
