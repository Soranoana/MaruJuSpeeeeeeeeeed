using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class button : MonoBehaviour {

    public SceneObject m_mainGame;
    public SceneObject m_title;
    public SceneObject m_Tutorial;

    public void OnNewGame() {
        SceneManager.LoadScene(m_mainGame);
    }

    public void OnGoTitle() {
        SceneManager.LoadScene(m_title);
    }

    public void SetHighScoreZero() {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }

    public void OnGoTutorial() {
        SceneManager.LoadScene(m_Tutorial);
    }
}
