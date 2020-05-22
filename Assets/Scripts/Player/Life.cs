using System;
using UnityEngine.UI;
using UnityEngine;

public class Life : MonoBehaviour
{
    #region Structs
    [Serializable]
    public struct Attributes
    {
        public string name;
        public float currentValue;
        public float maxValue;
        public float regenValue;
        public Image displayImage;
    }
    #endregion
    #region Variables
    public Attributes[] attributes = new Attributes[3];
    #endregion
}
