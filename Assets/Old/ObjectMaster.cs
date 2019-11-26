using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectMaster : MonoBehaviour {

    [SerializeField, Range(0, 10)] int grassGuaranteeValue = 3;

    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject capsule;

    [SerializeField] List<Object> objectPool = new List<Object>();

    bool isGrassMode;
    int grassCounter=0;
    int spawnCounter = 0;

	void Start(){
		
        // 適当に20個配置
        for(int i=0; i<20; i++) {
            Create();
            spawnCounter++;
        }

    }

    private void Create() {

        var randSeed = UnityEngine.Random.Range(0, 3);
        GameObject respawn;
        Object.Type respawnType;
        GameObject reuseObj=null;

        if (isGrassMode) { randSeed = 2; }

        if (randSeed == 0) { respawnType = Object.Type.BOMB; }
        else if(randSeed == 1) { respawnType = Object.Type.ROCK; }
        else { respawnType = Object.Type.GRASS; }

        foreach(Object obj in objectPool) {
            if(!obj.isActive && obj.myType == respawnType) {
                reuseObj = obj.gameObject;
                break;
            }
        }

        if(reuseObj == null) {
            GameObject newObj;

            if (randSeed == 0) {
                newObj = Instantiate(capsule);
                newObj.transform.position = gameObject.transform.position + new Vector3(0, 0, spawnCounter*2);
                newObj.GetComponent<Object>().myType = Object.Type.BOMB;
            } else if (randSeed == 1) {
                newObj = Instantiate(sphere);
                newObj.transform.position = gameObject.transform.position + new Vector3(0, 0, spawnCounter * 2);
                newObj.GetComponent<Object>().myType = Object.Type.ROCK;
            } else {
                isGrassMode = true;
                grassCounter++;
                newObj = Instantiate(cube);
                newObj.transform.position = gameObject.transform.position + new Vector3(0, 0, spawnCounter * 2);
                newObj.GetComponent<Object>().myType = Object.Type.GRASS;
            }

            var newObjScript = newObj.GetComponent<Object>();
            newObjScript.OnDisable += () => Create();
            newObjScript.SetIsActive(true);
            objectPool.Add(newObjScript);

        } else {
            grassCounter++;
            reuseObj.GetComponent<Object>().SetIsActive(true);
            reuseObj.transform.position = gameObject.transform.position + new Vector3(0, 0, spawnCounter * 2);
        }


        if (isGrassMode && grassCounter >= grassGuaranteeValue) {
            grassCounter = 0;
            isGrassMode = false;
        }
    }

}
