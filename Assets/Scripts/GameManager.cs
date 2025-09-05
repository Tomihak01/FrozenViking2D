using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public float health;
    public float previousHealth;
    public float maxHealth;

    public float historyHealth;
    public float historyMaxHealth;
    public float historypreviousHealth;



    public string previousLevel;
    public string currentLevel;

    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public bool Level5;
    public bool Level6;




    private void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;

        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Map") 
            {
                SceneManager.LoadScene("MainMenu");
            }
            
        }
    }

    public void Save()
    {
        Debug.Log("Game Save");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.health = health;
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        data.Level5 = Level5;
        data.Level6 = Level6;
        data.currentLevel = currentLevel;
        bf.Serialize(file, data);
        file.Close();


    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game Loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            currentLevel = data.currentLevel;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
            Level4 = data.Level4;
            Level5 = data.Level5;
            Level6 = data.Level6;




        }
    }

}

[Serializable]
class PlayerData
{
    public float health;
    public float previousHealth;
    public float maxHealth;
    public string currentLevel;
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public bool Level5;
    public bool Level6;
}