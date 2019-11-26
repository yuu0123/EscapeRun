using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Object : MonoBehaviour {

    public event Action OnDisable;

    MeshRenderer myMesh;
    public Type myType;
    public bool isActive;

    public enum Type {
        GRASS,
        ROCK,
        BOMB
    }

    private void Awake() {
        myMesh = GetComponent<MeshRenderer>();
    }

    public void SetIsActive(bool newVal) {
        isActive = newVal;
        myMesh.enabled = newVal;
    }

    private void Update() {
        if (isActive) {
            gameObject.transform.Translate(0, 0, -0.1f);

            if(gameObject.transform.position.z < -5) {
                SetIsActive(false);
                OnDisable?.Invoke();
            }

        }  
    }

}
