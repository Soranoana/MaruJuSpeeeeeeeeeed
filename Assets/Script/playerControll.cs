using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControll : MonoBehaviour {

    Camera gameCamera;
    Vector3 touchWorldPosition;
    [SerializeField]
    private GameObject Genelator;
    public system cameraSys;
    private int penaltyTimeCount;
    private int penaltyTimeCountDistance;
    private int penaltyOfObject;
    private int penaltyOfGenerator;
    private int penaltyOfCircle = 500;
    private int penaltyOfLeft;

    //移動用
    private Vector3 wasVector;  //1フレーム前の座標
    private bool isTapping = false; //タップ中である
    private Vector3 moveVector; //加算する座標値
    private bool isCollisionCircle = false;
    private float edgeOfCircle = 2.35f; //半径　マジックナンバー

    private bool isTutorial;

    private void Awake() {
        penaltyOfObject = 500;
        penaltyOfGenerator = 3000;
        penaltyOfCircle = 500;
        penaltyOfLeft = 1000;

        penaltyTimeCount = 0;
        penaltyTimeCountDistance = 30;
    }

    void Start () {
        isTutorial = cameraSys.isTutorialScene();
        gameCamera = Camera.main;
        wasVector = transform.position;
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            //タップスタート
            isTapping = true;
        } else if (Input.GetMouseButtonUp(0)) {
            //タップエンド
            isTapping = false;
        }
        //移動処理
        moveVector = gameCamera.ScreenToWorldPoint(Input.mousePosition) - wasVector;
        moveVector = new Vector3(moveVector.x, moveVector.y, 0);
        //実際に移動するのはタップ中のみ
        if (isTapping) {
            transform.position += moveVector;
        }
        //過去座標更新
        wasVector = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        //境界処理とペナルティ処理
        if (Vector3.Distance(transform.position, Genelator.transform.position) >= edgeOfCircle) {
            //境界処理
            transform.position = (transform.position - Genelator.transform.position).normalized * edgeOfCircle;
            //30フレームごとにペナルティ
            if (penaltyTimeCount % penaltyTimeCountDistance == 0) {
                cameraSys.penalty(getPenaltyOfCircle() * (penaltyTimeCount / penaltyTimeCountDistance + 1));
            }
            penaltyTimeCount++;
        } else {
            penaltyTimeCount = 0;
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "object") {
            cameraSys.penalty(getPenaltyOfObject());
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Genelator") {
            cameraSys.penalty(getPenaltyOfGenerator());
        }
    }

    public void OnCollisionStay(Collision collision) {
        if (collision.gameObject.name == "outsideCircle") {
            isCollisionCircle = true;
        }
    }

    public void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == "outsideCircle") {
            isCollisionCircle = false;
        }
    }
    //オブジェクトペナルティ
    public int getPenaltyOfObject() {
        return penaltyOfObject;
    }
    //中心接触ペナルティ
    public int getPenaltyOfGenerator() {
        return penaltyOfGenerator;
    }
    //外円ペナルティ
    public int getPenaltyOfCircle() {
        return penaltyOfCircle;
    }
    //逆走ペナルティ
    public int getPenaltyOfLeft() {
        return penaltyOfLeft;
    }
}
