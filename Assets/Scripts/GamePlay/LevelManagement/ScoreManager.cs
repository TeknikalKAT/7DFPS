using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] Text scoreText;
    int score = 0;

    GameStatus gameStatus;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        gameStatus = GetComponent<GameStatus>();
        scoreText.text = score.ToString();
    }

    public void AddPoints(int points)
    {
        if (gameStatus.Over())
            return;
        score += points;
    }
}
