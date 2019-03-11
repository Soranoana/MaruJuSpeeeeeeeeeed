using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toTitle : MonoBehaviour
{
    private int count;
    private GameObject rogo;
    private Animation rogoAni;
    private float deltaTime;
    private int waitFrame;
    private float waitTime;

    public float m_WaitTimeSafeMargin;
    /*アニメーション
     *実フレーム数（LoadSceneするまで）　：　約580フレーム
     *実時間（LoadSceneするまで）　：　約4.53ｓ
     *Unity のフレームレート：100フレーム
     */
    //次のシーン
    public SceneObject m_nextScene;

    void Start() {
        rogo = GameObject.Find("soranoanaROGO2");
        rogoAni = rogo.GetComponent<Animation>();
        rogoAni.Play();
        waitTime=6f;    //動かない時の待ち時間
        waitFrame =(int)waitTime*100;
        deltaTime=0f;
        count = 0;
    }

    private void Update() {
        /* 以下遷移方法の違いについてのスクリプト */

        /* フレーム数で遷移 */
        if (count >= waitFrame+(int)m_WaitTimeSafeMargin*100) {
            Debug.Log("count over");
            goTitle();
        }

        /* 経過時間で遷移 */
        if (deltaTime>=waitTime+m_WaitTimeSafeMargin) {
            Debug.Log("time over");
            goTitle();
        }
        deltaTime+=Time.deltaTime;

        /* アニメーション終了で遷移 */
        if (!rogoAni.isPlaying && count>m_WaitTimeSafeMargin*100) {
            Debug.Log("animetion finish");
            goTitle();
        }
        
        /* スマホ用 */
        if (Input.touchCount>0) {
            Debug.Log("touch event");
            goTitle();
        }

        count++;
    }
    /* PC用 */
    public void OnMouseDown() {
        Debug.Log("click event");
        goTitle();
    }

    private void goTitle() {
        SceneManager.LoadScene(m_nextScene);
    }

    
}