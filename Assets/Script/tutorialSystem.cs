using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialSystem : MonoBehaviour {

    /*
     1（赤い点に注視する）
        このゲームは赤い点をマウスで操作して円の中をぐるぐるするゲームです。
     2（カウントに注視する）
        ゲームが始まるとこの数字が減り始めます。
     3（右セクタに注視する）
        数字が0になる前に時計回りに回っていきましょう。
     4（線と緑の文字に注視する）
        黒い線を超えられれば数字の分だけポイントがもらえます。
     5（赤い四角に注視する）
        ゲーム中は中心から赤い四角が出てきます。
     6（赤い文字に注視する）
        これに当たるとマイナス500ポイントです。
     7（左セクタに注視する）
        逆方向に回るとマイナス1000ポイントです
        また、中心を通過しようとするとマイナス3000です。
     8（円に注視する）
     　 外側の円にぶつかると約0.3秒ごとに500、1000、1500....とポイントがマイナスされていきます。
     9（制限時間に注視する）
        制限時間は60秒です。
     10（スコアに注視する）
        スコアをどんどん上げていきましょう。
*/
    public GameObject player;
    private MeshRenderer playerMesh;
    private playerControll playerCont;
    public Text time1;
    public SpriteRenderer rightSector;
    public SpriteRenderer cross;
    public Text scoreUp;
    private MeshRenderer objectRenderer;
    public Text scoreDown;
    public SpriteRenderer leftSector;
    public SpriteRenderer circle;
    public Text timeAll;
    public Text score;
    public SceneObject title;
    public MeshRenderer genelator;

    private int TutorialStage = 0;
    public Text tutorialMessageBox;
    private string[] tutorialMessage;
    private float sumTimes = 0f;
    private float alphaChangeTime;

    void Start () {
        alphaChangeTime = 0.2f;
        playerMesh = player.GetComponent<MeshRenderer>();
        playerCont = player.GetComponent<playerControll>();
        int circlePena = playerCont.getPenaltyOfCircle();
        tutorialMessage = new string[12] {  "このゲームのチュートリアルになります",
                                            "このゲームは赤い点をマウスで操作して円の中をぐるぐるするゲームです。",
                                            "ゲームが始まるとこの数字が減り始めます。" ,
                                            "数字が0になる前に時計回りに回っていきましょう。",
                                            "黒い線を超えられれば数字の分だけポイントがもらえます。",
                                            "ゲーム中は中心から赤い四角が出てきます。",
                                            "これに当たるとマイナス"+ playerCont.getPenaltyOfObject() +"ポイントです。",
                                            "逆方向に回るとマイナス"+ playerCont.getPenaltyOfLeft() +"ポイントです。また、中心を通過しようとするとマイナス"+ playerCont.getPenaltyOfGenerator() +"です。",
                                            "外側の円にぶつかると約0.3秒ごとに"+ circlePena +"、"+ circlePena * 2 +"、"+ circlePena * 3 +"....とポイントがマイナスされていきます。",
                                            "制限時間は60秒です。",
                                            "スコアをどんどん上げていきましょう。",
                                            "以上でチュートリアルは終わりです。"};
    }
	
	void Update () {
        sumTimes += Time.deltaTime;
        if (sumTimes >= alphaChangeTime * 2) sumTimes = 0;
        if (TutorialStage == 0) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaReset(playerMesh);
        } else if (TutorialStage == 1) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(playerMesh);
            alphaReset(time1);
        } else if (TutorialStage == 2) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(time1);
            alphaReset(playerMesh);
            alphaReset(rightSector);
        } else if (TutorialStage == 3) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(rightSector);
            alphaReset(time1);
            alphaReset(cross);
            alphaReset(scoreUp);
        } else if (TutorialStage == 4) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(cross);
            alphaZeroOne(scoreUp);
            alphaReset(rightSector);
            alphaReset(objectRenderer);
        } else if (TutorialStage == 5) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(objectRenderer);
            alphaReset(cross);
            alphaReset(scoreUp);
            alphaReset(scoreDown);
        } else if (TutorialStage == 6) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(scoreDown);
            alphaReset(objectRenderer);
            alphaReset(leftSector);
            alphaReset(genelator);
        } else if (TutorialStage == 7) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(leftSector);
            alphaZeroOne(genelator);
            alphaReset(scoreDown);
            alphaReset(circle);
        } else if (TutorialStage == 8) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(circle);
            alphaReset(genelator);
            alphaReset(leftSector);
            alphaReset(timeAll);
        } else if (TutorialStage == 9) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(timeAll);
            alphaReset(circle);
            alphaReset(score);
        } else if (TutorialStage == 10) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaZeroOne(score);
            alphaReset(timeAll);
        } else if (TutorialStage == 11) {
            tutorialMessageBox.text = tutorialMessage[TutorialStage];
            alphaReset(score);
        } else if (TutorialStage == 12) {
            SceneManager.LoadScene(title);
        }

        if (Input.GetMouseButtonDown(0))
            TutorialStage++;
        else if (Input.GetMouseButtonDown(1))
            TutorialStage--;
        if (objectRenderer == null && GameObject.FindGameObjectsWithTag("object") != null)
            objectRenderer = GameObject.Find("ObjectParent").transform.Find("object (1)").GetComponent<MeshRenderer>();
    }

    void alphaZeroOne(Text alphaText) {
        if (sumTimes <= alphaChangeTime)
            alphaText.color = new Color(alphaText.color.r, alphaText.color.g, alphaText.color.b, 1);
        else
            alphaText.color = new Color(alphaText.color.r, alphaText.color.g, alphaText.color.b, 0);
    }

    void alphaZeroOne(SpriteRenderer alphaText) {
        if (alphaText != leftSector && alphaText != rightSector) {
            if (sumTimes <= alphaChangeTime)
                alphaText.color = new Color(alphaText.color.r, alphaText.color.g, alphaText.color.b, 1);
            else
                alphaText.color = new Color(alphaText.color.r, alphaText.color.g, alphaText.color.b, 0);
        } else {
            if (sumTimes <= alphaChangeTime)
                alphaText.color = new Color(125, 0, 0, 1);
            else
                alphaText.color = new Color(0, 0, 0, 0);
        }
    }
    void alphaZeroOne(MeshRenderer alphaText) {
        if (alphaText != genelator) {
            if (sumTimes <= alphaChangeTime)
                alphaText.enabled = true;
            else
                alphaText.enabled = false;
        } else {
            if (sumTimes <= alphaChangeTime) {
                genelator.material.color = new Color(1, 0, 0, 1);
            } else {
                genelator.material.color = new Color(1, 0, 0, 0);
            }
        }
    }

    void alphaReset(Text alphaText) {
        alphaText.color = new Color(alphaText.color.r, alphaText.color.g, alphaText.color.b, 1);
    }

    void alphaReset(SpriteRenderer alphaText) {
        if (alphaText != leftSector && alphaText != rightSector) {
            alphaText.color = new Color(alphaText.color.r, alphaText.color.g, alphaText.color.b, 1);
        } else {
            alphaText.color = new Color(255, 255, 255, 1);
        }
    }

    void alphaReset(MeshRenderer alphaText) {
        if (alphaText != genelator.GetComponent<MeshRenderer>()) {
            alphaText.enabled = true;
        } else {
            genelator.material.color = new Color(1, 0, 0, 0);
        }
    }
}
