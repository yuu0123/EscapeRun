using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    [SerializeField] Transform target;

	void Update() {
        if(target != null && gameObject.activeInHierarchy) {
            gameObject.transform.LookAt(target);
        }
	}

}
