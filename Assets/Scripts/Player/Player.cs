using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int Points { get; private set; }
    public int UnlockedLevel { get; private set; }
    public string LastUnlockedPicture { get; private set; }
    public Dictionary<string, HashSet<int>> PuzzleProgress { get; private set; } = new Dictionary<string, HashSet<int>>();
    public TextMeshProUGUI myText;
    public int teddyBearCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load player data
        LoadFromJSON();
        UpdatePoints(Points);
    }

    // Methods to update player data
    public void UpdatePoints(int points)
    {
        Points += points;
        GameObject textObject = GameObject.Find("pointsText");
        if (textObject != null)
        {
            myText = textObject.GetComponent<TextMeshProUGUI>();
        }
        myText.text = $"Points: {Points}";
    }

    public void UnlockLevel(int level)
    {
        if (level > UnlockedLevel)
        {
            UnlockedLevel = level;
        }
    }

    public void UnlockPicturePiece(string pictureName, int pieceIndex)
    {
        if (!PuzzleProgress.ContainsKey(pictureName))
        {
            PuzzleProgress[pictureName] = new HashSet<int>();
        }

        PuzzleProgress[pictureName].Add(pieceIndex);
        SaveToJSON();  // Save progress
    }

    public void SetLastUnlockedPicture(string pictureName)
    {
        LastUnlockedPicture = pictureName;
    }

    // Debug helper
    public void PrintPlayerData()
    {
        Debug.Log($"Points: {Points}, Unlocked Level: {UnlockedLevel}, Last Unlocked Picture: {LastUnlockedPicture}");
        foreach (var puzzle in PuzzleProgress)
        {
            Debug.Log($"Picture: {puzzle.Key}, Pieces: {string.Join(",", puzzle.Value)}");
        }
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Points", Points);
        PlayerPrefs.SetInt("UnlockedLevel", UnlockedLevel);
        PlayerPrefs.SetString("LastUnlockedPicture", LastUnlockedPicture);
        
        foreach (var puzzle in PuzzleProgress)
        {
            PlayerPrefs.SetString($"Puzzle_{puzzle.Key}", string.Join(",", puzzle.Value));
        }

        PlayerPrefs.Save();
    }

    public void LoadFromJSON()
{
    string filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

    if (File.Exists(filePath))
    {
        string json = File.ReadAllText(filePath);
        PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json);

        Points = data.Points;
        UnlockedLevel = data.UnlockedLevel;
        LastUnlockedPicture = data.LastUnlockedPicture;
        PuzzleProgress = data.PuzzleProgress;

        Debug.Log("Player data loaded successfully.");
    }
    else
    {
        Debug.LogWarning("Save file not found. Creating a new one with default data.");
        ResetPlayerData(); 
        SaveToJSON();  
    }
}

    public void SaveToJSON()
    {
        PlayerData data = new PlayerData
        {
            Points = Points,
            UnlockedLevel = UnlockedLevel,
            LastUnlockedPicture = LastUnlockedPicture,
            PuzzleProgress = PuzzleProgress
        };

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        string filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

        File.WriteAllText(filePath, json);

        Debug.Log($"Player data saved to: {filePath}");
    }

    public void ResetPlayerData()
    {
        Points = 0;
        UnlockedLevel = 1; 
        LastUnlockedPicture = string.Empty;
        PuzzleProgress.Clear();

        Debug.Log("Player data has been reset.");
    }

    public void AddTeddyBear()
    {
        teddyBearCount++;
    }

    public bool CanAfford(int cost)
    {
        return Points >= cost;
    }


    public void SpendPoints(int cost)
    {
        if (CanAfford(cost))
        {
            Points -= cost;
        }
    }

}
