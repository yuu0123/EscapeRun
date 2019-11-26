using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestChild : MonoBehaviour {

    public event Action OnDisable;
    public bool isActive;

	void Start(){
        isActive = true;
    }

	void Update() {
        if (isActive) {
            gameObject.transform.Translate(0, 0, -0.05f);

            if(gameObject.transform.position.z < -5) {
                isActive = false;
                OnDisable?.Invoke();
            }
        }
	}

}
