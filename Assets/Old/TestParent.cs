using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParent : MonoBehaviour {

    [SerializeField] GameObject[] test;

	void Start(){

        foreach(GameObject obj in test) {
            obj.GetComponent<TestChild>().OnDisable += () => {
                obj.GetComponent<TestChild>().isActive = true;
                obj.transform.position = new Vector3(0, 0.3f, 10);
            };
        }

    }

}
