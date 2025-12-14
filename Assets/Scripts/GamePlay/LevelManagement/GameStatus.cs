using UnityEngine;

public class GameStatus : MonoBehaviour
{

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelCompletePanel;
    FPSController player;
    Weapon_Manager weaponManager;
    bool gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);

        player = GameObject.FindAnyObjectByType<FPSController>();
        weaponManager = GameObject.FindAnyObjectByType<Weapon_Manager>();
    }

    public void GameComplete()
    {
        gameOver = true;
        weaponManager.enabled = false;
        player.enabled = false;
        levelCompletePanel.SetActive(true);
    }
    public void GameOver()
    {
        gameOver = true;
        weaponManager.enabled = false;
        player.enabled = false;
        gameOverPanel.SetActive(true);

    }

    public bool Over()
    {
        return gameOver;
    }
}
