﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.transform.root.gameObject.GetComponent<PlayerController>().DeathPlayer();
        }
    }

}
