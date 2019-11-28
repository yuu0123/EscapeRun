using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathBlood : MonoBehaviour {

    [SerializeField] GameObject bloodStickerPrefab;
    [SerializeField] List<GameObject> objectPool = new List<GameObject>();
    private List<ParticleCollisionEvent> collisionEventList = new List<ParticleCollisionEvent>();
    private ParticleSystem particle;
    private int useBloodStickerID = 0;

    void Start(){
        particle = GetComponent<ParticleSystem>();

        // オブジェクトプールに作成
        for(int i=0; i<80; i++) {
            var hoge = Instantiate(bloodStickerPrefab);
            objectPool.Add(hoge);
        }
    }

    private void OnParticleCollision(GameObject other) {

        particle.GetCollisionEvents(other, collisionEventList);

        foreach (var collisionEvent in collisionEventList) {
            Vector3 pos = collisionEvent.intersection;

            objectPool[useBloodStickerID].transform.position = pos;
            objectPool[useBloodStickerID].transform.localScale = Vector3.one * Random.Range(0.5f, 1.1f);

            useBloodStickerID++;
            if (useBloodStickerID >= objectPool.Count) { useBloodStickerID = 0; }
        }
    }
}
