using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public static int SELECT_LEVEL = 0;
    public const int SKIN_UNLOCK_DIAMOND = 100;

    [SerializeField] TextMeshProUGUI levelText;

    #region UI parts
    [SerializeField] GameObject resultBG;
    [SerializeField] GameObject newCharaUnlockableBG;
    [SerializeField] Image progressFill;
    [SerializeField] TextMeshProUGUI[] progressTexts;
    #endregion

    [SerializeField] ParticleSystem getEffectParticle;
    [SerializeField] RescueTargetOnGUI rescueTargetOnGUI;
    [SerializeField] ClearBall clearBallScript;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> stages = new List<GameObject>();
    LineRenderer myLineRenderer;

    bool isRescued;
    CinemachineDollyCart playerDollyCart;
    

    private void OnEnable() {
        //myLineRenderer = GetComponent<LineRenderer>();
        playerDollyCart = player.GetComponent<CinemachineDollyCart>();
    }

    private void Start() {
        SetResolution(1280); // 反映はフレーム更新後
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        //levelText.text = "LEVEL " + (SELECT_LEVEL + 1);

        progressTexts[0].text = (SELECT_LEVEL + 1).ToString();
        progressTexts[1].text = (SELECT_LEVEL + 2).ToString();
        progressFill.fillAmount = playerDollyCart.m_Position;

        // ステージすべて非表示
        foreach (GameObject obj in stages) {
            obj.SetActive(false);
        }



        // 該当ステージだけ表示
        if(SELECT_LEVEL >= stages.Count) { SELECT_LEVEL = stages.Count-1; }
        var currentStage = stages[SELECT_LEVEL];
        currentStage.SetActive(true);

        var stageSetting = currentStage.GetComponent<StageSetting>();
        var stagePath = stageSetting.GetPath();
        //DrawLine(stagePathWaypoint);

        player.GetComponent<CinemachineDollyCart>().m_Path = stagePath;
        player.GetComponent<PlayerController>().OnGoal += () => ClearGame();
        player.GetComponent<PlayerController>().OnDeathReload += () => Reload();
        player.GetComponent<PlayerController>().OnRescued += skinID => {
            rescueTargetOnGUI.ChangeMySkin(skinID);
            isRescued = true;
        };

        // 獲得演出
        var stageObj = GameObject.FindGameObjectsWithTag("StageItem");
        foreach (GameObject obj in stageObj) {
            obj.GetComponent<TakeCube>().OnGetItem += pos => {
                Debug.Log("hoge");
                getEffectParticle.gameObject.transform.position = pos;
                getEffectParticle.Play();
            };
        }
    }

    private void Update() {
        progressFill.fillAmount = playerDollyCart.m_Position;
    }

    public void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToHome() {
        SELECT_LEVEL = 0;
        Reload();
    }

    public void NextLevel() {
        SELECT_LEVEL++;
        Reload();
    }

    private void ClearGame() {
        if (isRescued) {
            newCharaUnlockableBG.SetActive(true);
        } else {
            ShowResult();
        }
    }

    private void ShowResult() {
        resultBG.SetActive(true);
        clearBallScript.PlayMove();
    }

    public void CloseNewChara() {
        newCharaUnlockableBG.SetActive(false);
        ShowResult();

    }

    void SetResolution(float baseResolution) {
        float screenRate = baseResolution / Screen.height;
        if (screenRate > 1) screenRate = 1;
        int width = (int)(Screen.width * screenRate);
        int height = (int)(Screen.height * screenRate);

        Screen.SetResolution(width, height, true);
    }
}
