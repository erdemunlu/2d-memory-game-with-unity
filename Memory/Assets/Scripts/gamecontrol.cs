using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class gamecontrol : MonoBehaviour
{

    public int pickValue, maxMatch;
    public int firstPickValue;
    public float totalTime;
    int minute, second, matchCount;
    GameObject choosenObject;
    GameObject firstObject;
    bool timeController;
    bool isAllCardChoosen;
    public Sprite defaultImage;
    public GameObject pool,pool2;
    public GameObject grid;
    public TextMeshProUGUI countDownText;
    public GameObject[] Images;
    public GameObject[] Panels;
    public AudioSource[] Songs;


    private void Start()
    {
        Time.timeScale = 1;
        dealCards();
        minute = 0;
        second = 0;
        matchCount = 0;
        timeController = true;
        isAllCardChoosen = false;
        
    }
    private void Update()
    {
        if (timeController)
        {
            totalTime -= Time.deltaTime;
            minute = Mathf.FloorToInt(totalTime / 60);
            second = Mathf.FloorToInt(totalTime % 60);
            countDownText.text = string.Format("{0:00}:{1:00}", minute, second);
            if (totalTime < 1)
            {
                gameOver();
                timeController = false;
            }
        }
        
    }
    public void playAgainButton()
    {
        Songs[2].Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void homeButton()
    {
        Songs[2].Play();
        SceneManager.LoadScene("home");
    }
    public void continueButton()
    {
        Songs[2].Play();
        Panels[2].SetActive(false);
        Time.timeScale = 1;
    }
    public void stopButton()
    {
        Songs[2].Play();
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void gameOver()
    {
        raycastSituation(false);
        Panels[1].SetActive(true);
        Time.timeScale = 0;
    }
    public void win()
    {
        raycastSituation(false);
        Panels[0].SetActive(true);
        if(PlayerPrefs.GetInt("currentLevel") == 1)
        {
            PlayerPrefs.SetInt("level2", 1);
        }
        if (PlayerPrefs.GetInt("currentLevel") == 2)
        {
            PlayerPrefs.SetInt("level3", 2);
        }


        Time.timeScale = 0;
    }

    public void dealCards()
    {
        
        while (grid.transform.childCount != maxMatch*2)
        {
            if(maxMatch != 18 && isAllCardChoosen == false)
            {
                int randomNumber = Random.Range(0, pool.transform.childCount);
                if (randomNumber % 2 != 0)
                {
                    randomNumber--;
                }
                if(pool.transform.GetChild(randomNumber).gameObject != null && pool.transform.GetChild(randomNumber+1).gameObject != null)
                {
                    pool.transform.GetChild(randomNumber).transform.SetParent(pool2.transform);
                    pool.transform.GetChild(randomNumber).transform.SetParent(pool2.transform);
                    if(pool2.transform.childCount == maxMatch * 2)
                    {
                        while(grid.transform.childCount != maxMatch * 2)
                        {
                            randomNumber = Random.Range(0, pool2.transform.childCount);
                            pool2.transform.GetChild(randomNumber).transform.SetParent(grid.transform);
                        }
                        Destroy(pool2);
                        isAllCardChoosen = true;
                        
                    }
                }
            }
            else
            {
                int randomNumber = Random.Range(0, pool.transform.childCount);
                if (pool.transform.GetChild(randomNumber).gameObject != null)
                {
                    pool.transform.GetChild(randomNumber).transform.SetParent(grid.transform);
                }
            }
            
        }
        Destroy(pool);
    }

    public void getValue(int value)
    {
        pickValue = value;
        control(choosenObject);
    }
    public void getObject(GameObject obj)
    {
        Songs[2].Play();
        choosenObject = obj;
        obj.GetComponent<Image>().raycastTarget = false;
        obj.GetComponent<Image>().sprite = obj.GetComponentInChildren<SpriteRenderer>().sprite;
        
    }
    public void raycastSituation(bool state)
    {
        foreach(var item in Images)
        {
            if(item != null)
            {
                item.GetComponent<Image>().raycastTarget = state;

            }
        }
    }
    public void control(GameObject obj)
    {
        if(firstPickValue == 0)
        {
            firstObject = obj;
            firstPickValue = pickValue;
        }
        else
        {
            raycastSituation(false);
            StartCoroutine(wait());
           

        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);

        if (firstPickValue == pickValue)
        {
            firstObject.GetComponent<Image>().enabled = false;
            firstObject.GetComponent<Button>().enabled = false;
            choosenObject.GetComponent<Image>().enabled = false;
            choosenObject.GetComponent<Button>().enabled = false;
            matchCount++;
            Songs[0].Play();
            if(maxMatch == matchCount)
            {
                win();
                UnityEngine.Debug.Log("Congratulations!");
            }
        }
        else
        {
            firstObject.GetComponent<Image>().sprite = defaultImage;
            choosenObject.GetComponent<Image>().sprite = defaultImage;
            Songs[1].Play();
        }
        firstPickValue = 0;
        choosenObject = null;
        firstObject = null;
        raycastSituation(true);
    }

}
