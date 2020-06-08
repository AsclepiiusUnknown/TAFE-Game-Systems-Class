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

    [Header("Character Class")]
    public CanvasCharacterClass canvasCharClass = CanvasCharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedClassIndex = 0;
    public string classButton = "";
    public int statPoints = 10;

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
                break;
            case 1:
                //Base Stat Values
                characterStats[0].baseStats = 6;
                characterStats[1].baseStats = 13;
                characterStats[2].baseStats = 7;
                characterStats[3].baseStats = 10;
                characterStats[4].baseStats = 6;
                characterStats[5].baseStats = 18;

                canvasCharClass = CanvasCharacterClass.Bard;
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
                break;
        }
    }

    public void SetPointsPos(int i)
    {
        //Change the values
        statPoints--;
        pointSystem[i].stat.tempValue++;

        //If we have no points hide the + button
        if (statPoints <= 0)
        {
            for (int button = 0; button < pointSystem.Length; button++)
            {
                pointSystem[button].plusButton.SetActive(false);
            }

            //If we havent yet shownt he - button then do so
            if (pointSystem[i].minusButton.activeSelf == false)
            {
                pointSystem[i].minusButton.SetActive(true);
            }

            //Update the appropriate text element
            pointSystem[i].nameDisplay.text = pointSystem[i].stat.name + ": " + (pointSystem[i].stat.value + pointSystem[i].stat.tempValue);
        }
    }
    #endregion

    #region Random & Reset
    public void RandomiseChoice(int typeCount) //Type count is how many types of customisable types there are (eg. skin, hair, eyes, etc)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        for (int typeIndex = 0; typeIndex < typeCount; typeIndex++)
        {
            switch (typeIndex)
            {
                case 0: //Skin
                    index = skinIndex;
                    max = skinMax;
                    textures = skin.ToArray();
                    matIndex = 1;
                    break;
                case 1: //Eyes
                    index = eyesIndex;
                    max = eyesMax;
                    textures = eyes.ToArray();
                    matIndex = 2;
                    break;
                case 2: //Mouth
                    index = mouthIndex;
                    max = mouthMax;
                    textures = mouth.ToArray();
                    matIndex = 3;
                    break;
                case 3: //Hair
                    index = hairIndex;
                    max = hairMax;
                    textures = hair.ToArray();
                    matIndex = 4;
                    break;
                case 4: //Clothes
                    index = clothesIndex;
                    max = clothesMax;
                    textures = clothes.ToArray();
                    matIndex = 5;
                    break;
                case 5: //Armour
                    index = armourIndex;
                    max = armourMax;
                    textures = armour.ToArray();
                    matIndex = 6;
                    break;
            }

            int randomIndex = Random.Range(0, max);

            index += randomIndex;

            if (index < 0)
            {
                index = max - 1;
            }
            if (index > max - 1)
            {
                index = 0;
            }
            Material[] mat = characterRenderer.materials;
            mat[matIndex].mainTexture = textures[index];
            characterRenderer.materials = mat;
            //skinMax;
            //public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;

            switch (typeIndex)
            {
                case 0: //Skin
                    skinIndex = index;
                    max = skinMax;
                    textures = skin.ToArray();
                    matIndex = 1;
                    break;
                case 1: //Eyes
                    eyesIndex = index;
                    max = eyesMax;
                    textures = eyes.ToArray();
                    matIndex = 2;
                    break;
                case 2: //Mouth
                    mouthIndex = index;
                    max = mouthMax;
                    textures = mouth.ToArray();
                    matIndex = 3;
                    break;
                case 3: //Hair
                    hairIndex = index;
                    max = hairMax;
                    textures = hair.ToArray();
                    matIndex = 4;
                    break;
                case 4: //Clothes
                    armourIndex = index;
                    max = armourMax;
                    textures = armour.ToArray();
                    matIndex = 5;
                    break;
                case 5: //Armour
                    clothesIndex = index;
                    max = clothesMax;
                    textures = clothes.ToArray();
                    matIndex = 6;
                    break;
            }
        }
    }

    public void ResetAll(int typeCount)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        for (int typeIndex = 0; typeIndex < typeCount; typeIndex++)
        {
            switch (typeIndex)
            {
                case 0: //Skin
                    index = skinIndex;
                    max = skinMax;
                    textures = skin.ToArray();
                    matIndex = 1;
                    break;
                case 1: //Eyes
                    index = eyesIndex;
                    max = eyesMax;
                    textures = eyes.ToArray();
                    matIndex = 2;
                    break;
                case 2: //Mouth
                    index = mouthIndex;
                    max = mouthMax;
                    textures = mouth.ToArray();
                    matIndex = 3;
                    break;
                case 3: //Hair
                    index = hairIndex;
                    max = hairMax;
                    textures = hair.ToArray();
                    matIndex = 4;
                    break;
                case 4: //Clothes
                    index = clothesIndex;
                    max = clothesMax;
                    textures = clothes.ToArray();
                    matIndex = 5;
                    break;
                case 5: //Armour
                    index = armourIndex;
                    max = armourMax;
                    textures = armour.ToArray();
                    matIndex = 6;
                    break;
            }

            index += 0;

            if (index < 0)
            {
                index = max - 1;
            }
            if (index > max - 1)
            {
                index = 0;
            }
            Material[] mat = characterRenderer.materials;
            mat[matIndex].mainTexture = textures[index];
            characterRenderer.materials = mat;
            //skinMax;
            //public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;

            switch (typeIndex)
            {
                case 0: //Skin
                    skinIndex = index;
                    max = skinMax;
                    textures = skin.ToArray();
                    matIndex = 1;
                    break;
                case 1: //Eyes
                    eyesIndex = index;
                    max = eyesMax;
                    textures = eyes.ToArray();
                    matIndex = 2;
                    break;
                case 2: //Mouth
                    mouthIndex = index;
                    max = mouthMax;
                    textures = mouth.ToArray();
                    matIndex = 3;
                    break;
                case 3: //Hair
                    hairIndex = index;
                    max = hairMax;
                    textures = hair.ToArray();
                    matIndex = 4;
                    break;
                case 4: //Clothes
                    armourIndex = index;
                    max = armourMax;
                    textures = armour.ToArray();
                    matIndex = 5;
                    break;
                case 5: //Armour
                    clothesIndex = index;
                    max = clothesMax;
                    textures = clothes.ToArray();
                    matIndex = 6;
                    break;
            }
        }
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

        for (int i = 0; i < characterStats.Length; i++)
        {
            PlayerPrefs.SetInt(characterStats[i].baseStatsName, characterStats[i].baseStats + characterStats[i].tempStats);
        }

        PlayerPrefs.SetString("Character Class", selectedClass[selectedClassIndex]);

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
}