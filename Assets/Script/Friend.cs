using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour {

    [SerializeField] GameObject[] mySkin;

    private void Start() {
        foreach(GameObject obj in mySkin) {
            obj.SetActive(false);
        }
    }

    public void ChangeFriendSkin(int id) {
        if (id > mySkin.Length) { return; }
        mySkin[id].SetActive(true);
    }

}
