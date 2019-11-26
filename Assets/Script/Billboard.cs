using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    GameObject target;

    private void Start() {
        target = Camera.main.gameObject;
    }

    void Update() {
        if(target !=null && target.activeInHierarchy) {
            gameObject.transform.LookAt(target.transform);
        }
    }

}
