using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

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

    public void PlayMove() {
        foreach(GameObject obj in balls) {

            obj.transform.DOMove(target.transform.position, Random.Range(1,2.0f))
                .OnComplete(()=> {
                    obj.SetActive(false);
                    target.transform.DOScale(Vector3.one * 1.3f, 0.15f);
                    target.transform.DOScale(Vector3.one, 0.15f);
                    myProgress++;
                    skinProgressText.text = (((float)myProgress / (float)GameDirector.SKIN_UNLOCK_DIAMOND) * 100) + "%";
                    targetFillImage.fillAmount = (float)myProgress / (float)GameDirector.SKIN_UNLOCK_DIAMOND;

                });

        }
    }

}
