using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    public TouchController touches;
    public GeneralController general;

    private float volume;
    public List<AudioSource> sounds;

    public float wavespeed;
    public float wavespeedlevel;
    public int hp;
    public List<Heart> hearts;

    private bool reloadThis;
    private bool reload;
    private float loadingtimer = 3;

    public GameObject volumeOn;
    public GameObject volumeOff;
    public GameObject loadingScreen;
    public GameObject settingScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    private float mode; // unique level
    public int howManyLevelsDone; // real number of last level
    private int levelMax; // how many levels total
    public float chosenLevel; // real number of level

    // all ui
    public int levelreward;
    public TMP_Text levelcoins;
    public int coins;
    public int price1;
    public int price2;
    public TMP_Text price1text;
    public TMP_Text price2text;
    public List<TMP_Text> coinsText;

    public int scoreIndex2 = 1;
    public int scoreIndex;
    public int bestScore;
    public int currentScore;
    public int targetScore;
    public TMP_Text bestScoreText;
    public TMP_Text currentScoreText;

    private float speed;

    // skills
    public float a2timer;
    public float a2timerMax;
    public Image a2activeskale;
    public bool a2active;
    public float a1timer;
    public float a1timerMax;
    public Image a1activeskale;
    public bool a1active;

    // tips
    public Animator tipAnimator;

    public int tutorial1;
    public int tutorial2;
    public int tutorial3;


  public GameObject lightObject;
    private bool lighton;
    public void Start()
    {
        Time.timeScale = 1;
            asyncOperation = SceneManager.LoadSceneAsync("MainMenu");
        asyncOperation.allowSceneActivation = false;

        coins = PlayerPrefs.GetInt("coins");
        mode = PlayerPrefs.GetFloat("mode");
        levelMax = PlayerPrefs.GetInt("levelMax");
        volume = PlayerPrefs.GetFloat("volume");
        chosenLevel = PlayerPrefs.GetFloat("chosenLevel");
        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");
        bestScore = PlayerPrefs.GetInt("bestScore");

        tutorial1 = PlayerPrefs.GetInt("tutorial1");
        tutorial2 = PlayerPrefs.GetInt("tutorial2");
        tutorial3 = PlayerPrefs.GetInt("tutorial3");



        a2activeskale.fillAmount = 0;
        a1activeskale.fillAmount = 0;

        sounds[0].Play();
        if (volume == 1)
        {
            Sound(true);
        }
        else
        {
            Sound(false);
        }

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        settingScreen.SetActive(false);
        loadingScreen.SetActive(false);

        tipAnimator.enabled = false;
        price1text.text = price1.ToString("0");
        price2text.text = price2.ToString("0");


        // levels
       
            if (mode == 1 || mode == 0)
            {
                wavespeedlevel = 0.05f;
                general.spawnTimerMin = 10;
                general.spawnTimerMax = 15;
            general.minpositionToSpawn = 0.8f;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;
            }
            else if (mode == 2)
            {
                wavespeedlevel = 0.05f;
                general.spawnTimerMin = 10;
                general.spawnTimerMax = 12;
            general.minpositionToSpawn = 0.7f;
            targetScore = scoreIndex * (int)mode;
                levelreward = 100;

            }
            else if (mode == 3)
            {
                wavespeedlevel = 0.06f;
                general.spawnTimerMin = 10;
                general.spawnTimerMax = 11;
            general.minpositionToSpawn = 0.6f;
            targetScore = scoreIndex * (int)mode;
                levelreward = 100;
            }
            else if (mode == 4)
            {
                wavespeedlevel = 0.06f;
                general.spawnTimerMin = 10;
                general.spawnTimerMax = 11;
            targetScore = scoreIndex * (int)mode;
                levelreward = 100;

            }
            else if (mode == 5)
            {
                wavespeedlevel = 0.07f;
                general.spawnTimerMin = 8;
                general.spawnTimerMax = 10;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;
            }
            else if (mode == 6)
            {

                wavespeedlevel = 0.07f;
                general.spawnTimerMin = 7;
                general.spawnTimerMax = 10;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;
            }
            else if (mode == 7)
            {
                wavespeedlevel = 0.08f;
                general.spawnTimerMin = 7;
                general.spawnTimerMax = 10;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;

            }
            else if (mode == 8)
            {
                wavespeedlevel = 0.08f;
                general.spawnTimerMin = 7;
                general.spawnTimerMax = 10;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;

            }
            else if (mode == 9)
            {
                wavespeedlevel = 0.08f;
                general.spawnTimerMin = 7;
                general.spawnTimerMax = 10;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;

            }
            else if (mode == 10)
            {
                wavespeedlevel = 0.08f;
                general.spawnTimerMin = 5;
                general.spawnTimerMax = 10;
                targetScore = scoreIndex * (int)mode;
                levelreward = 100;
            }

        
        wavespeed = wavespeedlevel;

        bestScoreText.text = "best score: " + bestScore.ToString("0");
        currentScoreText.text = currentScore.ToString("0") + "/" + targetScore.ToString("0");

        
        levelcoins.text = "+" + levelreward.ToString("0") + "!";

        if (tutorial1 == 0)
        {
            tipAnimator.enabled = false;
            tipAnimator.Play("Start");
            tipAnimator.enabled = true;
        }
        else if (tutorial1 != 0 && tutorial3 == 0 && coins >= price1)
        {
            PlayerPrefs.SetInt("tutorial3", 1);
            PlayerPrefs.Save();
            tipAnimator.enabled = false;
            tipAnimator.Play("Bonuses");
            tipAnimator.enabled = true;
        }
        else
        {
            tipAnimator.enabled = false;
        }
    }

   public void hpLost()
    {
        hp -= 1;
        if (hp >= 0)
        {
            hearts[hp].enabled = true;
        }
        if (hp <= 0)
        {
            lose();
        }
    }


    public void win()
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
        general.paused = true;
        Debug.Log("win");
        winScreen.SetActive(true);
        if (chosenLevel > howManyLevelsDone)
        {
            PlayerPrefs.SetInt("howManyLevelsDone", (int)chosenLevel);
        }

        coins += levelreward;
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.Save();
    }

    public void lose()
    {

        PlayerPrefs.SetInt("bestScore", bestScore);
        general.paused = true;
        loseScreen.SetActive(true);

        PlayerPrefs.Save();
    }
    public void shieldActive()
    {
        if (!general.shieldactive)
        {
            sounds[1].Play();
            general.shieldactive = true;
            general.shield.SetActive(true);
            touches.blocked = true;
        }
    }

    public void Update()
    {
        //if (!lighton)
        //{
        //    lighton = true;
        //    Instantiate(lightObject, transform.position, Quaternion.identity);
        //    //GameObject lightObj = Instantiate(lightObject, transform.position, Quaternion.Euler(60, -30, 0));
        //    //  lightObj.transform.position = Vector3.zero;
        //}


        foreach (TMP_Text text in coinsText)
        {
            text.text = coins.ToString("0");
        }

        if(currentScore > bestScore)
        {
            bestScore = currentScore;
        }

        if (currentScore >= targetScore && !winScreen.activeSelf)
        {
            win();
        }

        bestScoreText.text = "best score: " + bestScore.ToString("0");
        currentScoreText.text = currentScore.ToString("0") + "/" + targetScore.ToString("0");


        if (loadingScreen.activeSelf == true)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = 0;
            }

            if (loadingtimer > 0)
            {
                loadingtimer -= Time.deltaTime;
            }
            else
            {
                if (!reload)
                {
                    reload = true;
                    if (reloadThis)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
           }
        }
        if (!loadingScreen.activeSelf)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = volume;
            }
        }
    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        sounds[1].Play();
        general.paused = false;
        asyncOperation.allowSceneActivation = true;
        loadingScreen.SetActive(true);
    }
    public void reloadScene()
    {
        Time.timeScale = 1;
        sounds[1].Play();
        //general.paused = false;
        reloadThis = true;
        loadingScreen.SetActive(true);
    }
    public void Sound(bool volumeBool)
    {
        if (volumeBool)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
            volume = 1;
        }
        else
        {
            volume = 0;
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void closeIt()
    {
        sounds[1].Play();
        touches.blocked = true;
        general.paused = false;
        settingScreen.SetActive(false);
        wavespeed = wavespeedlevel;
    }

    public void Settings()
    {
        sounds[1].Play();
        touches.blocked = true;
        general.paused = true;
        settingScreen.SetActive(true);
        wavespeed = 0;
    }

    public void a1()
    {
        sounds[1].Play();
        touches.blocked = true;

        if (coins >= price1)
        {
            if (!a1active)
            {
                coins -= price1;
                PlayerPrefs.SetInt("coins", coins);
                PlayerPrefs.Save();
               a1active = true;
               a1timer = a1timerMax;

            }
        }
        else
        {
            tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }

    public void a2()
    {
        sounds[1].Play();
        touches.blocked = true;

        if (coins >= price2)
        {
            if (!a2active)
            {
                coins -= price2;
                PlayerPrefs.SetInt("coins", coins);
                PlayerPrefs.Save();
                a2active = true;
                a2timer = a2timerMax;
            }
        }
        else
        {

            tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        sounds[1].Play();
        if (chosenLevel <= howManyLevelsDone + 1 && chosenLevel != levelMax)
        {
            chosenLevel += 1;
            mode += 1;
            if (mode > 10)
            {
                mode = 1;
            }


            PlayerPrefs.SetFloat("chosenLevel", chosenLevel);
            PlayerPrefs.SetFloat("mode", mode);
            PlayerPrefs.Save();
            reloadScene();
        }
    }
}
