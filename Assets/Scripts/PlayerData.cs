[System.Serializable]
public class PlayerData
{
    public int Points;
    public int UnlockedLevel;
    public string LastUnlockedPicture;
    public Dictionary<string, HashSet<int>> PuzzleProgress = new Dictionary<string, HashSet<int>>();
}
