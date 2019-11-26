using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelfRotateAnimation : MonoBehaviour {

	void Start(){
        gameObject.transform.DORotate(new Vector3(0, 0, +360), 7).SetRelative().SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
    }

}
