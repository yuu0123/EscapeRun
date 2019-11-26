using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour {

    [SerializeField] GameObject[] myParts;

    public void Explode() {
        foreach(GameObject obj in myParts) {

            var myRB = obj.GetComponent<Rigidbody>();

            obj.GetComponent<CapsuleCollider>().enabled = true;
            myRB.isKinematic = false;
            myRB.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(2, 10), Random.Range(-5, 5)), ForceMode.Impulse);
            myRB.AddTorque(new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), ForceMode.Impulse);
        }
    }
    

}
