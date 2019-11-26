using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueTargetOnGUI : MonoBehaviour {

    [SerializeField] GameObject[] mySkins;
    int mySkinID;

    public void ChangeMySkin(int id) {
        mySkinID = id;

        foreach (GameObject obj in mySkins) {
            obj.SetActive(false);
        }

        mySkins[id].SetActive(true);

    }
}
