using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TakeCube : MonoBehaviour {

    public event Action<Vector3> OnGetItem;

    Sequence myAnimation;
    MeshRenderer myMeshRenderer;

	void Start(){
        myMeshRenderer = GetComponent<MeshRenderer>();
        myAnimation = DOTween.Sequence();

        myAnimation.Append(gameObject.transform.DORotate(new Vector3(0,+360,0),5f)).SetRelative()
            .SetLoops(-1,LoopType.Restart);

        myAnimation.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            myAnimation.Pause();
            myMeshRenderer.enabled = false;
            OnGetItem?.Invoke(gameObject.transform.position);
        }
    }

}
