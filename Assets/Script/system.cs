using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class system : MonoBehaviour {

    private int gameTime;
    private float zoneTime;
    private float startScore;
    private int nowZone;
    private int wasZone;
    private GameObject player;
    private GameObject genelator;
    private Vector2 playerDistance;
    private float playerRad;
    private bool changeZone;
    private float score;
    public Text scoreText;
    public Text time1Text;
    public Text time2Text;
    public Text time3Text;
    public Text time4Text;
    public Text timeAllText;
    public Text scoreUp;
    public Text scoreDown;
    private float upTime;
    private float downTime;
    private float delta;
    private float alltime;

    private bool isTutorial;

    private void Awake() {
        if (PlayerPrefs.GetInt("highscore") < 0 && SceneManager.GetActiveScene().name == "mainGame") {
            PlayerPrefs.SetInt("highscore", 0);
        }
        gameTime = 60;
        startScore = 100;
        zoneTime = startScore;
        changeZone = false;
        score = 0f;
        nowZone = 1;
        wasZone = 1;
        upTime = 0;
        downTime = 0;
        alltime = 0;

        if (SceneManager.GetActiveScene().name != "title") {
            scoreDown.text = "0";
            scoreUp.text = "0";
            scoreUp.enabled = false;
            scoreDown.enabled = false;
        }

        if (SceneManager.GetActiveScene().name == "Tutorial")
            isTutorial = true;
        else
            isTutorial = false;
    }

    void Start () {
        player = GameObject.FindWithTag("Player");
        genelator = GameObject.FindWithTag("Genelator");
        genelator.GetComponent<objectCreate>().setIsTutorial(isTutorial);
    }

    void Update () {
        delta = Time.deltaTime;
        alltime += delta;
        if (!isTutorial) {
            if (zoneTime > 0) {
                zoneTime -= delta * 30;
            } else {
                zoneTime = 0;
            }
        }
        getPlayerRad();
        setNowZone();
        if (SceneManager.GetActiveScene().name != "title") {
            if (changeZone) {
                scoreChange();
            }
            GUIChange();

            if (!isTutorial) {
                checkScoreUpDown();
            } else {
                scoreDown.enabled = true;
                scoreUp.enabled = true;
            }
            if (gameTime - (int)alltime <= 0 && !isTutorial) {
                //timeUp
                PlayerPrefs.SetInt("score", (int)score);
                PlayerPrefs.Save();
                SceneManager.LoadScene("result");
            }
        }
    }

    void getPlayerRad() {
        playerDistance = new Vector2(player.transform.position.x - genelator.transform.position.x, player.transform.position.y - genelator.transform.position.y);
        playerRad = Mathf.Atan2(playerDistance.x, playerDistance.y) * Mathf.Rad2Deg;
        if (playerRad < 0) {
            playerRad = 360 + playerRad;
        }
        return;
    }

    void setNowZone() {
        wasZone = nowZone;
        if ((playerRad < 45f && playerRad >= 0f) || (playerRad <= 360f && playerRad >= 315f )) {
            nowZone = 1;
        }else if (playerRad < 135f && playerRad >= 0f) {
            nowZone = 2;
        }else if (playerRad < 225f && playerRad >= 0f) {
            nowZone = 3;
        }else if (playerRad < 315f && playerRad >= 0f) {
            nowZone = 4;
        }

        if (wasZone != nowZone)
            changeZone = true;
        else
            changeZone = false;
    }

    bool setCorrectOrNot() {
        switch (nowZone) {
            case 1:
                if (wasZone == 4)
                    return true;
                else
                    return false;
            case 2:
                if (wasZone == 1)
                    return true;
                else
                    return false;
            case 3:
                if (wasZone == 2)
                    return true;
                else
                    return false;
            case 4:
                if (wasZone == 3)
                    return true;
                else
                    return false;
            case 0:
                if (wasZone == 0)
                    return true;
                else
                    return false;
            default:
                return false;

        }
    }

    void scoreChange() {
        if (setCorrectOrNot()) {
            score += zoneTime;
            upTime = 0;
            scoreUp.enabled = true;
            scoreUp.text = ((int)zoneTime).ToString();
            zoneTime = startScore;
        } else {
            penalty(player.GetComponent<playerControll>().getPenaltyOfLeft());
            zoneTime = startScore;
        }
    }

    void GUIChange() {
        if (!isTutorial) {
            scoreText.text = ((int)score).ToString();
            if (nowZone == 1) {
                time1Text.enabled = true;
                time2Text.enabled = false;
                time3Text.enabled = false;
                time4Text.enabled = false;
                time1Text.text = ((int)zoneTime).ToString();
            } else if (nowZone == 2) {
                time1Text.enabled = false;
                time2Text.enabled = true;
                time3Text.enabled = false;
                time4Text.enabled = false;
                time2Text.text = ((int)zoneTime).ToString();
            } else if (nowZone == 3) {
                time1Text.enabled = false;
                time2Text.enabled = false;
                time3Text.enabled = true;
                time4Text.enabled = false;
                time3Text.text = ((int)zoneTime).ToString();
            } else if (nowZone == 4) {
                time1Text.enabled = false;
                time2Text.enabled = false;
                time3Text.enabled = false;
                time4Text.enabled = true;
                time4Text.text = ((int)zoneTime).ToString();
            }
            timeAllText.text = (gameTime - (int)alltime).ToString();
        } else {
            time1Text.enabled = true;
            time2Text.enabled = false;
            time3Text.enabled = false;
            time4Text.enabled = false;
            time1Text.text = ((int)zoneTime).ToString();
            scoreText.text = ((int)score).ToString();
            timeAllText.text = "60";
        }
    }

    public void penalty(int point) {
        if (SceneManager.GetActiveScene().name == "mainGame" || SceneManager.GetActiveScene().name == "Tutorial") {
            score -= point;
            downTime = 0;
            scoreDown.enabled = true;
            scoreDown.text = point.ToString();
        }
    }

    void checkScoreUpDown() {
        if (scoreUp.enabled) {
            upTime++;
            if (upTime >= 30) {
                upTime = 0;
                scoreUp.enabled = false;
            }
        }else if (scoreDown.enabled) {
            downTime++;
            if (downTime >= 30) {
                downTime = 0;
                scoreDown.enabled = false;
            }
        }
    }

    public bool isTutorialScene() {
        return isTutorial;
    }
}
