using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private void Awake() {
        var thisMatrix = transform.localToWorldMatrix;
        var vertices = GetComponent<MeshFilter>().mesh.vertices;
        foreach (var vertex in vertices) {
            Debug.Log("mesh1 vertex at " + thisMatrix.MultiplyPoint3x4(vertex));
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one * 0.2f;
            cube.transform.position = thisMatrix.MultiplyPoint3x4(vertex);
        }

    }

}
