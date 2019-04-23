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
    public TextMesh scoreText;
    public TextMesh time1Text;
    public TextMesh time2Text;
    public TextMesh time3Text;
    public TextMesh time4Text;
    public TextMesh timeAllText;
    public TextMesh scoreUp;
    public TextMesh scoreDown;
    private MeshRenderer scoreMesh;
    private MeshRenderer time1Mesh;
    private MeshRenderer time2Mesh;
    private MeshRenderer time3Mesh;
    private MeshRenderer time4Mesh;
    private MeshRenderer timeAllMesh;
    private MeshRenderer scoreUpMesh;
    private MeshRenderer scoreDownMesh;

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

        scoreMesh = scoreText.transform.GetComponent<MeshRenderer>();
        time1Mesh = time1Text.transform.GetComponent<MeshRenderer>();
        time2Mesh = time2Text.transform.GetComponent<MeshRenderer>();
        time3Mesh = time3Text.transform.GetComponent<MeshRenderer>();
        time4Mesh = time4Text.transform.GetComponent<MeshRenderer>();
        timeAllMesh = timeAllText.transform.GetComponent<MeshRenderer>();
        scoreUpMesh = scoreUp.transform.GetComponent<MeshRenderer>();
        scoreDownMesh = scoreDown.transform.GetComponent<MeshRenderer>();


        if (SceneManager.GetActiveScene().name != "title") {
            scoreDown.text = "0";
            scoreUp.text = "0";
            scoreUpMesh.enabled = false;
            scoreDownMesh.enabled = false;
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
                scoreDownMesh.enabled = true;
                scoreUpMesh.enabled = true;
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
            scoreUpMesh.enabled = true;
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
                time1Mesh.enabled = true;
                time2Mesh.enabled = false;
                time3Mesh.enabled = false;
                time4Mesh.enabled = false;
                time1Text.text = ((int)zoneTime).ToString();
            } else if (nowZone == 2) {
                time1Mesh.enabled = false;
                time2Mesh.enabled = true;
                time3Mesh.enabled = false;
                time4Mesh.enabled = false;
                time2Text.text = ((int)zoneTime).ToString();
            } else if (nowZone == 3) {
                time1Mesh.enabled = false;
                time2Mesh.enabled = false;
                time3Mesh.enabled = true;
                time4Mesh.enabled = false;
                time3Text.text = ((int)zoneTime).ToString();
            } else if (nowZone == 4) {
                time1Mesh.enabled = false;
                time2Mesh.enabled = false;
                time3Mesh.enabled = false;
                time4Mesh.enabled = true;
                time4Text.text = ((int)zoneTime).ToString();
            }
            timeAllText.text = (gameTime - (int)alltime).ToString();
        } else {
            time1Mesh.enabled = true;
            time2Mesh.enabled = false;
            time3Mesh.enabled = false;
            time4Mesh.enabled = false;
            time1Text.text = ((int)zoneTime).ToString();
            scoreText.text = ((int)score).ToString();
            timeAllText.text = "60";
        }
    }

    public void penalty(int point) {
        if (SceneManager.GetActiveScene().name == "mainGame" || SceneManager.GetActiveScene().name == "Tutorial") {
            score -= point;
            downTime = 0;
            scoreDownMesh.enabled = true;
            scoreDown.text = point.ToString();
        }
    }

    void checkScoreUpDown() {
        if (scoreUpMesh.enabled) {
            upTime++;
            if (upTime >= 30) {
                upTime = 0;
                scoreUpMesh.enabled = false;
            }
        }else if (scoreDownMesh.enabled) {
            downTime++;
            if (downTime >= 30) {
                downTime = 0;
                scoreDownMesh.enabled = false;
            }
        }
    }

    public bool isTutorialScene() {
        return isTutorial;
    }
}
