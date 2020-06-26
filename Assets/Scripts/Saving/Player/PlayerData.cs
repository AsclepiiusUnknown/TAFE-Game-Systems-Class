[System.Serializable]
public class PlayerData
{
    //Data....Get from Game
    public string playerName;
    public int level;
    public float pX, pY, pZ;
    public float rX, rY, rZ, rW;
    /*public int SkinI, HairI, EyesI, MouthI, ClothesI, ArmourI;
    public string playerClass, playerAbility;*/
    public string checkPoint;
    public float currentExp, neededExp, maxExp;
    public int[] stats = new int[6];
    public float[] currentAttributes = new float[3];
    public float[] maxAttributes = new float[3];

    public PlayerData(PlayerHandler player)
    {
        //Basic
        //playerName = player.canvasSet.characterName;//player.name;
        level = PlayerHandler.level;
        checkPoint = player.currentCheckPoint.name;
        currentExp = player.currentExp;
        //neededExp = player.neededExp;
        //maxExp = player.maxExp;
        //Position
        pX = player.transform.position.x;
        pY = player.transform.position.y;
        pZ = player.transform.position.z;
        //Rotation
        rX = player.transform.rotation.x;
        rY = player.transform.rotation.y;
        rZ = player.transform.rotation.z;
        rW = player.transform.rotation.w;
        /*//Customisation
        SkinI = player.canvasSet.skinIndex;
        HairI = player.canvasSet.hairIndex;
        EyesI = player.canvasSet.eyesIndex;
        MouthI = player.canvasSet.mouthIndex;
        ClothesI = player.canvasSet.clothesIndex;
        ArmourI = player.canvasSet.armourIndex;
        //Class
        playerClass = player.canvasSet.classString;
        playerAbility = player.canvasSet.characterAbility.ToString();*/

        //Array
        for (int i = 0; i < currentAttributes.Length; i++)
        {
            currentAttributes[i] = player.attributes[i].currentValue;
            maxAttributes[i] = player.attributes[i].maxValue;
        }
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = player.characterStats[i].value + player.characterStats[i].tempValue;
        }
    }
}
