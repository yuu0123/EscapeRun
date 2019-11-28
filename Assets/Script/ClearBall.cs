using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class ClearBall : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] Image targetFillImage;
    [SerializeField] GameObject[] balls;
    [SerializeField] TextMeshProUGUI skinProgressText;
    int myProgress = 10;
	void Start(){

        skinProgressText.text = (((float)myProgress / (float)GameDirector.SKIN_UNLOCK_DIAMOND) * 100) + "%";
        targetFillImage.fillAmount = (float)myProgress / (float)GameDirector.SKIN_UNLOCK_DIAMOND;
        
    }

    public void PlayMove(int count, Action callback) {

        var valPerBall = count / balls.Length;
        int counter=0;

        foreach (GameObject obj in balls) {

            var newAni = DOTween.Sequence();
            newAni.Append(obj.transform.DOMove(target.transform.position, UnityEngine.Random.Range(0.5f, 1.25f)))
                .OnComplete(()=> {
                    obj.SetActive(false);
                    target.transform.DOScale(Vector3.one * 1.3f, 0.15f);
                    target.transform.DOScale(Vector3.one, 0.15f);
                    myProgress+=valPerBall;
                    counter++;
                    skinProgressText.text = (((float)myProgress / (float)GameDirector.SKIN_UNLOCK_DIAMOND) * 100) + "%";
                    targetFillImage.fillAmount = (float)myProgress / (float)GameDirector.SKIN_UNLOCK_DIAMOND;

                    if(counter >= balls.Length) {
                        callback?.Invoke();
                    }

                });
            newAni.Play();

        }
    }

}
