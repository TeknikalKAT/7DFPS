using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    [SerializeField] GameObject fadeOut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeOut.SetActive(false);
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
        Time.timeScale = 1;
        StartCoroutine(FadeOut());
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeOut()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        StartGame();
    }


}
