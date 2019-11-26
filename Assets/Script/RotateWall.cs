using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateWall : Enemy {

    [SerializeField] Type moveType;

    enum Type {
        NOMAL,
        STOPandMOVE,
        Xaxis
    }

    private void Start() {
        var myAnimation = DOTween.Sequence();

        switch (moveType) {
            case Type.STOPandMOVE:
                myAnimation
                    .Append(gameObject.transform.DORotate(new Vector3(0, +180, 0), 1f))
                    .AppendInterval(1f)
                    .Append(gameObject.transform.DORotate(new Vector3(0, +180, 0), 1f))
                    .SetLoops(-1);
                break;
            case Type.NOMAL:
                myAnimation
                    .Append(gameObject.transform.DORotate(new Vector3(0, -360, 0), 6f).SetRelative().SetEase(Ease.Linear))
                    .SetLoops(-1);
                break;
            case Type.Xaxis:
                myAnimation
                    .Append(gameObject.transform.DORotate(new Vector3(+360, 0, 0), 6f).SetRelative().SetEase(Ease.Linear))
                    .SetLoops(-1);
                break;

        }

        myAnimation.Play();
    }

}
