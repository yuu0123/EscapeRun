using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour {

    [SerializeField] CinemachineDollyCart myDollyCart;
    [SerializeField] Animator myAnimator;
    [SerializeField] GameObject rootBone;
    [SerializeField] BoxCollider rootCollider;
    [SerializeField] Camera deathCamera;
    [SerializeField] GameObject myFriend;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject deathParticle;
    public bool isDead;
    bool isActive;
    bool isGoal;

    public event Action OnGoal;
    public event Action OnDeathReload;
    public event Action<int> OnRescued;

	void Start(){
        SwitchRagdoll(false);
    }

	void Update() {
        if (isGoal || isActive) { return; }
        if (isDead) { myDollyCart.m_Speed = 0;  }

        if (Input.GetMouseButton(0)) {
            if (isDead) { return; }
            myAnimator.SetBool("Run", true);
            myFriend.GetComponent<Animator>().SetBool("Run", true);
            myDollyCart.m_Speed = 0.3f;
        } else {
            myAnimator.SetBool("Run", false);
            myFriend.GetComponent<Animator>().SetBool("Run", false);
            myDollyCart.m_Speed = 0;
        }

        // ゴールチェック
        if(myDollyCart.m_Position == 1) {
            isGoal = true;
            deathCamera.gameObject.SetActive(true);
            particle.SetActive(true);
            myAnimator.Play("Win");
            myFriend.GetComponent<Animator>().Play("Win");
            StartCoroutine(DeathCameraDispose());

        }
    }

    public void SwitchRagdoll(bool newVal) {

        myAnimator.enabled = !newVal;

        var child = GetAllChildren.GetAll(rootBone);
        foreach(GameObject obj in child) {
            if (obj.GetComponent<Rigidbody>()) { obj.GetComponent<Rigidbody>().isKinematic = !newVal; }
            if (obj.GetComponent<BoxCollider>()) { obj.GetComponent<BoxCollider>().enabled = newVal; }
            if (obj.GetComponent<CapsuleCollider>()) { obj.GetComponent<CapsuleCollider>().enabled = newVal; }
            if (obj.GetComponent<SphereCollider>()) { obj.GetComponent<SphereCollider>().enabled = newVal; }
        }
    }

    public void DeathPlayer() {
        if (isDead) { return; }
        rootCollider.enabled = false;
        isDead = true;
        SwitchRagdoll(true);
        deathParticle.SetActive(true);
        deathCamera.gameObject.SetActive(true);
        StartCoroutine(DeathCamera());
    }

    public void ChangeActive(bool newVal) {
        isActive = newVal;
    }

    public void ChangeFriend(int id) {
        OnRescued?.Invoke(id);
        myFriend.GetComponent<Friend>().ChangeFriendSkin(id);
    }

    IEnumerator DeathCamera() {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        deathCamera.gameObject.SetActive(false);
        OnDeathReload?.Invoke();
    }


    IEnumerator DeathCameraDispose() {
        yield return new WaitForSeconds(2f);
        deathCamera.gameObject.SetActive(false);
        OnGoal?.Invoke();

    }
}
