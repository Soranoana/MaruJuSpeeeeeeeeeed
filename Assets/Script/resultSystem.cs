using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultSystem : MonoBehaviour {

    public Text resultText;
    public Text highScoreText;

	void Start () {
        if (PlayerPrefs.GetInt("score")>PlayerPrefs.GetInt("highscore")) {
            PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
            PlayerPrefs.Save();
        }
    }
	
	void Update () {
        resultText.text=PlayerPrefs.GetInt("score").ToString();
        highScoreText.text=PlayerPrefs.GetInt("highscore").ToString();
	}
}
