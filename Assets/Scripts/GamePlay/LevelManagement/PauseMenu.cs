using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject[] menus;

    bool isPaused;
    GameStatus gameStatus;
    InputController inputController;
    Weapon_Manager weaponManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponManager = GameObject.FindAnyObjectByType<Weapon_Manager>();
        gameStatus = GetComponent<GameStatus>();
        inputController = GetComponent<InputController>();
        pausePanel.SetActive(false);
        AllMenusOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputController.togglePause)
            TogglePause();
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            if(!gameStatus.Over())
            {
                pausePanel.SetActive(true);
                weaponManager.enabled = false;
                Time.timeScale = 0;
            }    
        }
        else
        {
            Time.timeScale = 1;
            weaponManager.enabled = true;
            pausePanel.SetActive(false);
            AllMenusOff();
        }
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        AllMenusOff();
    }

    public void AllMenusOff()
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(false);
    }

    public void SwitchMenu(string name)
    {
        foreach(var menu in menus)
        {
            if (menu.name != name)
                menu.SetActive(false);
            else
                menu.SetActive(true);
        }
    }
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool PauseStatus()
    {
        return isPaused;
    }
}
