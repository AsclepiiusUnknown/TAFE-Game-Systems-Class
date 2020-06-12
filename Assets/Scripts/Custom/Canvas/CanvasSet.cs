using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasSet : MonoBehaviour
{
    #region Variables
    //Lists of texture 2D for skin, hair, etc
    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();

    [Header("Index Values")]
    //Index numbers for our current skin, hair, etc textures
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;

    [Header("Renderer")]
    //Renderer for our character mesh so we can reference material list within script for changing visuals
    public Renderer characterRenderer;

    [Header("Max Index")]
    //Max amount of skin, hair, etc textures that our lists are filled up with
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;

    [Header("Character Name")]
    //Name of the characters custom name
    public string characterName;

    [System.Serializable] //Used to format it so that Unity can read and edit it and makes it accessible within the inspector
    public struct PointUI
    {
        public string statName;
        public Stats.StatBlock stat;
        public Text nameDisplay;
        public GameObject plusButton;
        public GameObject minusButton;
    }
    [Header("Point UI")]
    public PointUI[] pointSystem;

    [Header("Point Pool")]
    public Text poolDisplay;

    [Header("Character Class")]
    public CanvasCharacterClass canvasCharClass = CanvasCharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedClassIndex = 0;
    public string classButton = "";
    public int statPoints = 10;
    [HideInInspector]
    public string classString;
    public CharacterAbility characterAbility;

    public CanvasStats[] characterStats;
    [System.Serializable]
    public struct CanvasStats
    {
        public string baseStatsName;
        public int baseStats;
        public int tempStats;
    }
    #endregion

    #region Start
    void Start()
    {
        #region For Loops to Pull textures from files
        #region Skin
        for (int i = 0; i < skinMax; i++) //For each element of this type
        {
            Texture2D tempTexture = Resources.Load("Character/Skin_" + i) as Texture2D; //Load it in from the files as a Texture 2D
            skin.Add(tempTexture); //Add it to the list of that type
        }
        #endregion
        #region Eyes
        for (int i = 0; i < eyesMax; i++) //For each element of this type
        {
            Texture2D tempTexture = Resources.Load("Character/Eyes_" + i) as Texture2D; //Load it in from the files as a Texture 2D
            eyes.Add(tempTexture); //Add it to the list of that type
        }
        #endregion
        #region Mouth
        for (int i = 0; i < mouthMax; i++) //For each element of this type
        {
            Texture2D tempTexture = Resources.Load("Character/Mouth_" + i) as Texture2D; //Load it in from the files as a Texture 2D
            mouth.Add(tempTexture); //Add it to the list of that type
        }
        #endregion
        #region Hair
        for (int i = 0; i < hairMax; i++) //For each element of this type
        {
            Texture2D tempTexture = Resources.Load("Character/Hair_" + i) as Texture2D; //Load it in from the files as a Texture 2D
            hair.Add(tempTexture); //Add it to the list of that type
        }
        #endregion
        #region Clothes
        for (int i = 0; i < clothesMax; i++) //For each element of this type
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i) as Texture2D; //Load it in from the files as a Texture 2D
            clothes.Add(tempTexture); //Add it to the list of that type
        }
        #endregion
        #region Armour
        for (int i = 0; i < armourMax; i++) //For each element of this type
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i) as Texture2D; //Load it in from the files as a Texture 2D
            armour.Add(tempTexture); //Add it to the list of that type
        }
        #endregion
        #endregion

        #region Set Default Textures on Start()
        //Set each texture type to the default of 0 in its appropriate index
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        #endregion

        //Set the calss to the default of 0 in the class index
        ChooseClass(0);

        #region Point System
        //Set up the point system
        for (int i = 0; i < pointSystem.Length; i++) //For each element within the point system
        {
            pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue); //Set up the names and value to display correctly and automatically
            pointSystem[i].minusButton.SetActive(false); //Disable the minus button as we dont need it yet
        }

        poolDisplay.text = "Points: " + statPoints;
        #endregion
    }
    #endregion

    #region Set Texture Functions
    public void SetTexturePositive(string type)
    {
        SetTexture(type, 1);
    }

    public void SetTextureNegative(string type)
    {
        SetTexture(type, -1);
    }

    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Eyes":
                index = eyesIndex;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 4;
                break;
            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 5;
                break;
            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 6;
                break;
        }

        index += dir;

        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        Material[] mat = characterRenderer.materials;
        //Debug.Log(matIndex.ToString());
        mat[matIndex].mainTexture = textures[index];
        characterRenderer.materials = mat;
        //skinMax;
        //public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;

        switch (type)
        {
            case "Skin":
                skinIndex = index;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Eyes":
                eyesIndex = index;
                max = eyesMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;
            case "Mouth":
                mouthIndex = index;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Hair":
                hairIndex = index;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 4;
                break;
            case "Armour":
                armourIndex = index;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 5;
                break;
            case "Clothes":
                clothesIndex = index;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 6;
                break;
        }
    }
    #endregion

    #region Stats & Class
    public void ChooseClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                //Base Stat Values
                characterStats[0].baseStats = 18;
                characterStats[1].baseStats = 10;
                characterStats[2].baseStats = 15;
                characterStats[3].baseStats = 6;
                characterStats[4].baseStats = 6;
                characterStats[5].baseStats = 5;

                canvasCharClass = CanvasCharacterClass.Barbarian;
                characterAbility = CharacterAbility.FeralMight;
                classString = "Barbarian";
                print("class set to " + classString);
                break;
            case 1:
                //Base Stat Values
                characterStats[0].baseStats = 6;
                characterStats[1].baseStats = 13;
                characterStats[2].baseStats = 7;
                characterStats[3].baseStats = 10;
                characterStats[4].baseStats = 6;
                characterStats[5].baseStats = 18;

                canvasCharClass = CanvasCharacterClass.Bard; characterAbility = CharacterAbility.FeralMight;
                characterAbility = CharacterAbility.SongOfRest;
                classString = "Bard";
                print("class set to " + classString);
                break;
            case 2:
                //Base Stat Values
                characterStats[0].baseStats = 5;
                characterStats[1].baseStats = 8;
                characterStats[2].baseStats = 8;
                characterStats[3].baseStats = 9;
                characterStats[4].baseStats = 15;
                characterStats[5].baseStats = 15;

                canvasCharClass = CanvasCharacterClass.Druid;
                characterAbility = CharacterAbility.BalanceOfNature;
                classString = "Druid";
                print("class set to " + classString);
                break;
            case 3:
                //Base Stat Values
                characterStats[0].baseStats = 8;
                characterStats[1].baseStats = 15;
                characterStats[2].baseStats = 10;
                characterStats[3].baseStats = 15;
                characterStats[4].baseStats = 8;
                characterStats[5].baseStats = 4;

                canvasCharClass = CanvasCharacterClass.Monk;
                characterAbility = CharacterAbility.MonasticTradition;
                classString = "Monk";
                print("class set to " + classString);
                break;
            case 4:
                //Base Stat Values
                characterStats[0].baseStats = 15;
                characterStats[1].baseStats = 6;
                characterStats[2].baseStats = 10;
                characterStats[3].baseStats = 8;
                characterStats[4].baseStats = 5;
                characterStats[5].baseStats = 18;

                canvasCharClass = CanvasCharacterClass.Paladin;
                characterAbility = CharacterAbility.ChannelDivinity;
                classString = "Paladin";
                print("class set to " + classString);
                break;
            case 5:
                //Base Stat Values
                characterStats[0].baseStats = 8;
                characterStats[1].baseStats = 16;
                characterStats[2].baseStats = 8;
                characterStats[3].baseStats = 12;
                characterStats[4].baseStats = 8;
                characterStats[5].baseStats = 8;

                canvasCharClass = CanvasCharacterClass.Ranger;
                characterAbility = CharacterAbility.BeastMastery;
                classString = "Ranger";
                print("class set to " + classString);
                break;
            case 6:
                //Base Stat Values
                characterStats[0].baseStats = 6;
                characterStats[1].baseStats = 8;
                characterStats[2].baseStats = 16;
                characterStats[3].baseStats = 8;
                characterStats[4].baseStats = 6;
                characterStats[5].baseStats = 5;

                canvasCharClass = CanvasCharacterClass.Sorcerer;
                characterAbility = CharacterAbility.CosmicPersistence;
                classString = "Sorcerer";
                print("class set to " + classString);
                break;
            case 7:
                //Base Stat Values
                characterStats[0].baseStats = 6;
                characterStats[1].baseStats = 6;
                characterStats[2].baseStats = 6;
                characterStats[3].baseStats = 10;
                characterStats[4].baseStats = 14;
                characterStats[5].baseStats = 18;

                canvasCharClass = CanvasCharacterClass.Warlock;
                characterAbility = CharacterAbility.EldritchBlast;
                classString = "Warlock";
                print("class set to " + classString);
                break;
        }
    }

    public void SetPointsPos(int i)
    {
        //Change the values
        statPoints--; //reduce the points in the point pool
        pointSystem[i].stat.tempValue++; //increase the correct temp value
        poolDisplay.text = "Points: " + statPoints; //Display the points in the point pool

        for (int a = 0; a < pointSystem.Length; a++) //For each element within the point system
        {
            pointSystem[a].nameDisplay.text = pointSystem[a].stat.name + ": " + (pointSystem[a].stat.value + pointSystem[a].stat.tempValue); //Set up the names and value to display correctly and automatically
        }

        //If we have no points hide the + button
        if (statPoints <= 0) //if there are no more points in the point pool
        {
            for (int b = 0; b < pointSystem.Length; b++) //for each element within the point system
            {
                pointSystem[b].plusButton.SetActive(false); //disable the + button
            }
        }

        if (pointSystem[i].minusButton.activeSelf == false) //if the - button is disabled
        {
            pointSystem[i].minusButton.SetActive(true); //enable the - button
        }

        //Update the appropriate text element
        pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);
    }

    public void SetPointsNeg(int i)
    {
        //Change the values
        statPoints++;
        pointSystem[i].stat.tempValue--;
        poolDisplay.text = "Points: " + statPoints;

        for (int a = 0; a < pointSystem.Length; a++) //For each element within the point system
        {
            pointSystem[a].nameDisplay.text = pointSystem[a].stat.name + ": " + (pointSystem[a].stat.value + pointSystem[a].stat.tempValue); //Set up the names and value to display correctly and automatically
        }

        //If we have no temp values disable the - button
        if (pointSystem[i].stat.tempValue <= 0)
        {
            pointSystem[i].minusButton.SetActive(false);
        }

        //If we have points to spend show all + buttons
        if (pointSystem[i].plusButton.activeSelf == false)
        {
            for (int b = 0; b < pointSystem.Length; b++)
            {
                pointSystem[b].plusButton.SetActive(true);
            }
        }

        //Update the appropriate text element
        pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);
    }
    #endregion

    #region Random & Reset
    public void RandomiseChoice(int typeCount) //Type count is how many types of customisable types there are (eg. skin, hair, eyes, etc)
    {
        SetTexture("Skin", skinIndex = Random.Range(0, skinMax - 1));
        SetTexture("Hair", hairIndex = Random.Range(0, hairMax - 1));
        SetTexture("Mouth", mouthIndex = Random.Range(0, mouthMax - 1));
        SetTexture("Eyes", eyesIndex = Random.Range(0, eyesMax - 1));
        SetTexture("Clothes", clothesIndex = Random.Range(0, clothesMax - 1));
        SetTexture("Armour", armourIndex = Random.Range(0, armourMax - 1));
    }

    public void ResetAll(int typeCount)
    {
        SetTexture("Skin", skinIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Eyes", eyesIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);
        SetTexture("Armour", armourIndex = 0);
    }
    #endregion

    #region Set Name
    public void SetCharacterName(string name)
    {
        characterName = name;
    }
    #endregion

    #region Save & Play
    public void SaveCharacter(bool play)
    {
        PlayerPrefs.SetInt("Skin Index", skinIndex);
        PlayerPrefs.SetInt("Hair Index", hairIndex);
        PlayerPrefs.SetInt("Eyes Index", eyesIndex);
        PlayerPrefs.SetInt("Mouth Index", mouthIndex);
        PlayerPrefs.SetInt("Clothes Index", clothesIndex);
        PlayerPrefs.SetInt("Armour Index", armourIndex);

        PlayerPrefs.SetString("Character Name", characterName);

        PlayerPrefs.SetString("Character Class", classString);

        PlayerPrefs.SetString("Character Ability", characterAbility.ToString());

        for (int i = 0; i < characterStats.Length; i++)
        {
            PlayerPrefs.SetInt(characterStats[i].baseStatsName, characterStats[i].baseStats + characterStats[i].tempStats);
        }

        if (play)
        {
            Play();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion

    public enum CanvasCharacterClass
    {   //Doesnt matter the type
        Barbarian,
        Bard,
        Druid,
        Monk,
        Paladin,
        Ranger,
        Sorcerer,
        Warlock
    }
    public enum CharacterAbility
    {
        FeralMight,
        SongOfRest,
        BalanceOfNature,
        MonasticTradition,
        ChannelDivinity,
        BeastMastery,
        CosmicPersistence,
        EldritchBlast
    }
}