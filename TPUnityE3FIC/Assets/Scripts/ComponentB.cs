using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentB : MonoBehaviour
{
    void Awake() {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - Awake");
    }
    void OnEnable() {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - OnEnable");
    }

    void OnDisable() {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - OnDisable");
    }
    // Start is called before the first frame update
    void Start()
    {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - Start");
    }

    // Update is called once per frame
    void Update()
    {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - Update");
    }
    void FixedUpdate() {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - FixedUpdate");
    }
    void LateUpdate() {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - LateUpdate");
    }

    void OnDestroy() {
        MyTools.Log(name + " - " + this.GetType().ToString() + " - OnDestroy");
    }
}
