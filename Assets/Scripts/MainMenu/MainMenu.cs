using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AllMenusOff();
    }


    void AllMenusOff()
    {
        for(int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }

    public void SwitchMenu(string name)
    {
        foreach (var menu in menus)
        {
            if (menu.name != name)
                menu.SetActive(false);
            else
                menu.SetActive(true);
        }
    }
    public void WaveNumber(int waveNumber)
    {
        PlayerPrefs.SetInt("Wave number", waveNumber);
        StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
           

}
