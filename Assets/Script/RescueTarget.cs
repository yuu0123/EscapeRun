using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueTarget : MonoBehaviour {

    [SerializeField] GameObject[] mySkins;
    [SerializeField] GameObject myMesh;
    [SerializeField] Cage cage;
    int mySkinID;
    bool isRescue;

	void Start(){
        ChangeMySkin(10);
        myMesh.GetComponent<Animator>().SetBool("Idle", true);
        myMesh.GetComponent<Animator>().Play("Idle");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !isRescue) {
            isRescue = true;
            cage.Explode();
            other.gameObject.transform.root.GetComponent<PlayerController>().ChangeFriend(mySkinID);
            myMesh.SetActive(false);
        }
    }

    public void ChangeMySkin(int id) {
        mySkinID = id;

        foreach (GameObject obj in mySkins) {
            obj.SetActive(false);
        }

        mySkins[id].SetActive(true);

    }


}
