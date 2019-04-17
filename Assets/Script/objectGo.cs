using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGo : MonoBehaviour {

    public GameObject generator;
    private objectCreate createSystem;
    private Vector3 targetForVector;
    private float myPase = 1f;
    private float deltaTime;

	void Start () {
        generator = GameObject.FindWithTag("Genelator");
        createSystem = generator.GetComponent<objectCreate>();
        targetForVector = (transform.position - generator.transform.position).normalized;
    }
	
	void Update () {
        deltaTime = createSystem.getDeltaTime();
        //if (!createSystem.isTutorialScene()) {
        transform.Translate(targetForVector * myPase * deltaTime);
        if (!createSystem.isTutorialScene()) {
            if (Vector3.Distance(transform.position, generator.transform.position) >= 3.8f) {
                Destroy(gameObject);
            }
        } else {
            if (Vector3.Distance(transform.position, generator.transform.position) >= 3.8f) {
                transform.position = generator.transform.position;
            }
        }
        //}
	}
}
