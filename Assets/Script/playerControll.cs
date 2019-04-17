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
    private bool peneltied = false;
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

        /*
        touchWorldPosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        touchWorldPosition = new Vector3(touchWorldPosition.x, touchWorldPosition.y, 0);
        transform.position = touchWorldPosition;
        if (Vector3.Distance(transform.position, Genelator.transform.position) >= 4f) {
            transform.position = (transform.position - Genelator.transform.position).normalized * 4f;
            if (SceneManager.GetActiveScene().name == "mainGame" || SceneManager.GetActiveScene().name == "Tutorial") {
                if (penaltyTimeCount % penaltyTimeCountDistance == 0) {
                    peneltied = false;
                }
                if (!peneltied) {
                    cameraSys.penalty(getPenaltyOfCircle() * (penaltyTimeCount / penaltyTimeCountDistance + 1));
                    peneltied = true;
                }
            }
            penaltyTimeCount++;
        } else {
            penaltyTimeCount = 0;
            peneltied = false;
        }
        */
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "object" && !isTutorial) {
            cameraSys.penalty(getPenaltyOfObject());
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Genelator") {
            cameraSys.penalty(getPenaltyOfGenerator());
        }
    }

    public int getPenaltyOfObject() {
        return penaltyOfObject;
    }

    public int getPenaltyOfGenerator() {
        return penaltyOfGenerator;
    }

    public int getPenaltyOfCircle() {
        return penaltyOfCircle;
    }

    public int getPenaltyOfLeft() {
        return penaltyOfLeft;
    }
}
