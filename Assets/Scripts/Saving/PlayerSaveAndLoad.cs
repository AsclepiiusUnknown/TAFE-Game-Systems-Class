using UnityEngine;

public class PlayerSaveAndLoad : MonoBehaviour
{
    public static PlayerHandler player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        if(!PlayerPrefs.HasKey("Loaded"))
        {
            PlayerPrefs.DeleteAll();
            //FirstLoad Function...sets up Player Data
            FirstLoad();
            //Save Data Creates first save file in binary
            Save();
            //We now have our first save file
            PlayerPrefs.SetInt("Loaded",0);
        }
        else
        {
            Load();
        }
    } 
    void FirstLoad()
    {
        player.name = "Adventurer";
        player.level = 1;
        player.currentCheckPoint = GameObject.Find("First Check Point").GetComponent<Transform>();

        player.maxExp = 60;

        for (int i = 0; i < 3; i++)
        {
            player.attributes[i].maxValue = 100;
            player.attributes[i].currentValue = 100;
        }
        for (int i = 0; i < 6; i++)
        {
            player.characterStats[i].value = 10;
        }
    }
    public static void Save()
    {
        //Do when Binary is done
        PlayerBinary.SavePlayerData(player);
    }
    public static void Load()
    {
        //Do this when Binary is done
        PlayerData data = PlayerBinary.LoadPlayerData(player);
        player.name = data.playerName;
        player.level = data.level;
        player.currentCheckPoint = GameObject.Find(data.checkPoint).GetComponent<Transform>();
        player.currentExp = data.currentExp;
        player.neededExp = data.neededExp;
        player.maxExp = data.maxExp;
        for (int i = 0; i < player.characterStats.Length; i++)
        {
            player.characterStats[i].value = data.stats[i];
        }
        for (int i = 0; i < player.attributes.Length; i++)
        {
            player.attributes[i].maxValue = data.maxAttributes[i];
            player.attributes[i].currentValue = data.currentAttributes[i];
        }

        player.transform.position = new Vector3(data.pX, data.pY, data.pZ);
        player.transform.rotation = new Quaternion(data.rX, data.rY, data.rZ,data.rW);        
    }
}
