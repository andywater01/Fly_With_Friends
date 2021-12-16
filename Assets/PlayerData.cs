//using UnityEngine;
//using System.Collections;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;

//[System.Serializable]
//public class PlayerData
//{
//    int[] hiScores;

//    public int maxLevels
//    {
//        get;
//        set;
//    }

//    protected PlayerData()
//    {
//    }

//    public PlayerData(int maxLevels)
//    {
//        this.maxLevels = maxLevels;
//        hiScores = new int[maxLevels];
//    }

//    public void SetScore(int levelNumber, int score)
//    {
//        hiScores[levelNumber] = score;
//    }

//    public int GetScore(int levelNumber)
//    {
//        return hiScores[levelNumber];
//    }

//    public void Save(string path)
//    {
//        BinaryFormatter bf = new BinaryFormatter();
//        FileStream file = File.Create(path);

//        bf.Serialize(file, this);
//        file.Close();
//    }

//    public static PlayerData Load(string path)
//    {
//        BinaryFormatter bf = new BinaryFormatter();
//        FileStream file = File.Open(path, FileMode.Open);

//        PlayerData data = (PlayerData)bf.Deserialize(file);
//        file.Close();
//        return data;
//    }
//}
