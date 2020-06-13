using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationGet : MonoBehaviour
{
    [Header("General")]
    public Renderer characterRenderer;
    public GameObject player;

    [Header("UI Components")]
    public Text nameDisplay;
    public Text classDisplay;
    public Text abilityDisplay;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LoadCanvas();
    }

    void LoadCanvas()
    {
        CanvasData data = CanvasSaveSystem.LoadCanvas();

        int skinIndex = data.skinIndex;
        int eyesIndex = data.eyesIndex;
        int mouthIndex = data.mouthIndex;
        int hairIndex = data.hairIndex;
        int armourIndex = data.armourIndex;
        int clothesIndex = data.clothesIndex;

        string characterName = data.playerName;
        string characterClass = data.playerClass;
        string characterAbility = data.playerAbility;

        SetTexture("Skin", skinIndex);
        SetTexture("Hair", hairIndex);
        SetTexture("Eyes", eyesIndex);
        SetTexture("Mouth", mouthIndex);
        SetTexture("Clothes", clothesIndex);
        SetTexture("Armour", armourIndex);

        player.name = characterName;
        nameDisplay.text = "NAME: " + characterName;

        classDisplay.text = "CLASS: " + characterClass;
        abilityDisplay.text = "ABILITY: " + characterAbility;
    }

    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int matIndex = 0;

        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 4;
                break;
            case "Clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                matIndex = 5;
                break;
            case "Armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 6;
                break;
        }

        Material[] mats = characterRenderer.materials;
        mats[matIndex].mainTexture = texture;
        characterRenderer.materials = mats;
    }
}
