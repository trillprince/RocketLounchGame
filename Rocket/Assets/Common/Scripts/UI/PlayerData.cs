using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    private int _highScore;

    public void SetHighScore(int value)
    {
        _highScore = value;
    }

    public int GetHighScore()
    {
        return _highScore;
    }
}