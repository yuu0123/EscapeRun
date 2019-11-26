using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSetting : MonoBehaviour {

    [SerializeField] CinemachinePathBase stagePath;

    public CinemachinePathBase GetPath() {
        return stagePath;
    }

}
