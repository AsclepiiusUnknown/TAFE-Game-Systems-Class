using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class CustomisationSet : MonoBehaviour
{
    #region Variables
    [Header("Texture Arrays")]
    public List<Texture2D> textures = new List<Texture2D>();

    [Header("Index")]
    public Texture2D[] skinIndex;
    public Texture2D[] hairIndex;
    public Texture2D[] eyesIndex;
    public Texture2D[] mouthIndex;

    [Header("Renderer")]
    public SkinnedMeshRenderer skinRenderer;

    [Header("Max Index")] //???????????????
    public int maxSkinIndex = 4;
    public int maxHairIndex = 4;
    public int maxEyesIndex = 4;
    public int maxMouthIndex = 4;
    //max amount of skin, hair, mouth, and eye textures that our lists are filled with

    [Header("Character Name")]
    public string characterName;
    #endregion

    void Start()
    {
        #region Texture Cycling
        for (int i = 0; i < skinIndex.Length; i++)
        {
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            textures.Add(temp);
        }

        for (int i = 0; i < hairIndex.Length; i++)
        {
            Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;
            textures.Add(temp);
        }

        for (int i = 0; i < eyesIndex.Length; i++)
        {
            Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;
            textures.Add(temp);
        }

        for (int i = 0; i < skinIndex.Length; i++)
        {
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            textures.Add(temp);
        }
        #endregion

        if (GetComponent<SkinnedMeshRenderer>() != null)
        {
            skinRenderer = GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            Debug.LogError("CusstomisationSet.cs could not find skinRenderer");
        }

        //SetTextures SetTexture()
    }

    void SetTexture(string type, int index)
    {
        int indexNumbers;
        int maxNumbers;
        int matIndex = 0;
        Texture2D texture = null;
        Texture2D[] textures;

        switch (type)
        {
            case "Skin":
                textures = skinIndex;
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;

                if (matIndex > skinIndex.Length)
                {
                    matIndex = 0;
                }
                else if (matIndex < 0)
                {
                    matIndex = skinIndex.Length;
                }
                break;
            case "Eyes":
                textures = eyesIndex;
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 2;

                if (matIndex > eyesIndex.Length)
                {
                    matIndex = 0;
                }
                else if (matIndex < 0)
                {
                    matIndex = eyesIndex.Length;
                }
                break;
            case "Mouth":
                textures = mouthIndex;
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;

                if (matIndex > mouthIndex.Length)
                {
                    matIndex = 0;
                }
                else if (matIndex < 0)
                {
                    matIndex = mouthIndex.Length;
                }
                break;
            case "Hair":
                textures = hairIndex;
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 4;

                if (matIndex > hairIndex.Length)
                {
                    matIndex = 0;
                }
                else if (matIndex < 0)
                {
                    matIndex = hairIndex.Length;
                }
                break;
        }
        matIndex += index;


        Material[] mats = skinRenderer.materials;
        mats[matIndex].mainTexture = texture;
        skinRenderer.materials = mats;
    }

    public void Save(int matIndex)
    {
        PlayerPrefs.SetInt("SkinIndex", matIndex);
        PlayerPrefs.SetInt("EyesIndex", matIndex);
        PlayerPrefs.SetInt("MouthIndex", matIndex);
        PlayerPrefs.SetInt("HairIndex", matIndex);
    }
}