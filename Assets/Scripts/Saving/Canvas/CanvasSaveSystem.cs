using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class CanvasSaveSystem
{
    public static void SaveCanvas(CanvasSet canvas) //Used to save the canvas data
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Create a new binary formatter to use later on
        string path = Application.persistentDataPath + "/playerCust.bin"; //Create a sctring to store the path to the location we want to save the file
        FileStream stream = new FileStream(path, FileMode.Create);//Start a new file stream for creating the save file and put in the path

        CanvasData data = new CanvasData(canvas); //Create a new instance of our canvas data

        formatter.Serialize(stream, data); //Write the data we created to the file using our current file stream (created above)
        stream.Close(); //close the file stream
    }

    public static CanvasData LoadCanvas() //Used to load the canvas data
    {
        string path = Application.persistentDataPath + "/playerCust.bin"; //Create a sctring to store the path to the location we want to save the file

        if (File.Exists(path)) //If the file exists at our given location
        {
            BinaryFormatter formatter = new BinaryFormatter(); //Create a new binary formatter to use later on
            FileStream stream = new FileStream(path, FileMode.Open); //Start a new file stream for opening the save file at the given path

            CanvasData data = formatter.Deserialize(stream) as CanvasData; //Set the data to equal the deserialized form
            stream.Close(); //Close the file stream

            return data; //Return the data
        }
        else //otherwise
        {
            Debug.LogError("Save file not found in " + path); //debug the missing file
            return null;
        }
    }
}