﻿using System.Collections;
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
        Load();
    }

    void Load()
    {
        SetTexture("Skin", PlayerPrefs.GetInt("Skin Index"));
        SetTexture("Hair", PlayerPrefs.GetInt("Hair Index"));
        SetTexture("Eyes", PlayerPrefs.GetInt("Eyes Index"));
        SetTexture("Mouth", PlayerPrefs.GetInt("Mouth Index"));
        SetTexture("Clothes", PlayerPrefs.GetInt("Clothes Index"));
        SetTexture("Armour", PlayerPrefs.GetInt("Armour Index"));

        if (PlayerPrefs.GetString("Character Name") != null)
        {
            player.name = PlayerPrefs.GetString("Character Name");
            nameDisplay.text = "NAME: " + PlayerPrefs.GetString("Character Name");
        }

        classDisplay.text = "CLASS: " + PlayerPrefs.GetString("Character Class");

        abilityDisplay.text = "ABILITY: " + PlayerPrefs.GetString("Character Ability");
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
