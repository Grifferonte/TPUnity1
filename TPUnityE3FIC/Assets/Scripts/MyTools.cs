using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTools
{
    public static void Log (string msg) {
        Debug.Log(Time.frameCount + " - " + msg);
    }
    public static void ChangeColorRandom(GameObject go) {
        MeshRenderer mr = go.GetComponentInChildren<MeshRenderer>();
        if(mr) {
            mr.material.color = Random.ColorHSV();
        }
    }

    public static void SpawnRandomPrimitives(int nObjects, Vector3 refPos, float radius) {
        for (int i = 0; i < nObjects; i++) {
            GameObject newGO = GameObject.CreatePrimitive((PrimitiveType)Random.Range(0,5));
            newGO.transform.position = refPos + Random.insideUnitSphere * radius;
            newGO.transform.localScale *= .25f;

            MeshCollider mc = newGO.GetComponent<MeshCollider>();
            if (mc) {
                mc.convex = true;
            }
            newGO.AddComponent<Rigidbody>();

        }
    }
}
