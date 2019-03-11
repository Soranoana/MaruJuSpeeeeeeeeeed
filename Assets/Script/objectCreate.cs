using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class objectCreate : MonoBehaviour {

    public GameObject Object;   //生成オブジェクト（プレファブ）
    private GameObject[] GenelatedObject=new GameObject[100];   //生成済みオブジェクト一覧
    private int geneNum;        //生成済みオブジェクト一覧の空きナンバー
    private float createTime = 3f;//生成間隔
    private float[] geneTime=new float[4] {0,0,0,0 };  //前回生成からの時間
    private float deltaTime;

    private bool isTutorial;

	void Start () {
        for (int i = 0; i<4; i++) {
            geneTime[i]=createTime;
        }
	}
	
	void Update () {
        deltaTime=Time.deltaTime;
        if (!isTutorial) {
            for (int i = 0; i<4; i++) {
                if (geneTime[i]>=3f) {
                    if (Random.Range(0f, 100f)>98f) {
                        geneNum=genelatorEmptyNum();
                        if (i==0) {
                            GenelatedObject[geneNum]=Instantiate(Object, ( transform.position+transform.up+transform.right ).normalized*0.01f, Object.transform.rotation);
                        } else if (i==1) {
                            GenelatedObject[geneNum]=Instantiate(Object, ( transform.position-transform.up+transform.right ).normalized*0.01f, Object.transform.rotation);
                        } else if (i==2) {
                            GenelatedObject[geneNum]=Instantiate(Object, ( transform.position-transform.up-transform.right ).normalized*0.01f, Object.transform.rotation);
                        } else if (i==3) {
                            GenelatedObject[geneNum]=Instantiate(Object, ( transform.position+transform.up-transform.right ).normalized*0.01f, Object.transform.rotation);
                        }
                        geneTime[i]=0;
                    }
                }
            }
        } else {
            if (GenelatedObject[0]==null) {
                Debug.Log("ok");
                GenelatedObject[0]=Instantiate(Object, ( transform.position+transform.up+transform.right ).normalized*0.01f, Object.transform.rotation);
                GenelatedObject[0].name="ObjectParent";
            }
            
        }
        for (int i = 0; i<4; i++) {
            geneTime[i]+=deltaTime;
        }
	}

    private int genelatorEmptyNum() {
        for (int i = 0; i<100; i++) {
            if (GenelatedObject[i]==null) {
                return i;
            }
        }
        return -1;
    }

    public float getDeltaTime() {
        return deltaTime;
    }

    public bool isTutorialScene() {
        return isTutorial;
    }

    public void setIsTutorial(bool flg) {
        isTutorial=flg;
    }
}
