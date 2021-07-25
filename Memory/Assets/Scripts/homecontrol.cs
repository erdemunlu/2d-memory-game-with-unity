using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class homecontrol : MonoBehaviour
{
    public AudioSource song;
    public Button[] buttons;
    public GameObject creditPanel;
    
    private void Start()
    {
        
        if (!PlayerPrefs.HasKey("level2"))
        {
            PlayerPrefs.SetInt("level2", 0);
        }
        if (!PlayerPrefs.HasKey("level3"))
        {
            PlayerPrefs.SetInt("level3", 0);
        }
        if (!PlayerPrefs.HasKey("currentLevel"))
        {
            PlayerPrefs.SetInt("currentLevel", 0);
        }

        if(PlayerPrefs.GetInt("level2") == 1)
        {
            buttons[0].interactable = true;
        }
        else
        {
            buttons[0].interactable = false;

        }
        if (PlayerPrefs.GetInt("level3") == 2)
        {
            buttons[1].interactable = true;
        }
        else
        {
            buttons[1].interactable = false;

        }
    }
    public void creditButton()
    {
        song.Play();
        creditPanel.SetActive(true);
    }
    public void closeButton()
    {
        song.Play();
        creditPanel.SetActive(false);
    }
    public void level1Button()
    {
        song.Play();
        PlayerPrefs.SetInt("currentLevel", 1);
        SceneManager.LoadScene(1);
    }
    public void level2Button()
    {
        song.Play();
        PlayerPrefs.SetInt("currentLevel", 2);
        SceneManager.LoadScene(2);
    }
    public void level3Button()
    {
        song.Play();
        PlayerPrefs.SetInt("currentLevel", 3);
        SceneManager.LoadScene(3);
    }
}
