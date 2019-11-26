using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObject : Enemy {

    [SerializeField] Type moveType;

    enum Type {
        Yaxis_DOWN
    }

    void Start(){
        var myAnimation = DOTween.Sequence();

        switch (moveType) {
            case Type.Yaxis_DOWN:
                myAnimation.Append(gameObject.transform.DOLocalMoveY(-1, 1).SetEase(Ease.Linear))
                    .SetLoops(-1,LoopType.Yoyo);
                break;
        }
        myAnimation.Play();
    }


}
