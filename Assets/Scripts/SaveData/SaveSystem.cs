using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData();

        formatter.Serialize( fileStream, playerData ); 
        fileStream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data= formatter.Deserialize( fileStream ) as PlayerData;
            fileStream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in "+path);
            return null;
        }

    }
}
