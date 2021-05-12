using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    void OnCollisionEnter(Collision col) 
    {
        Debug.Log(name + "COLLIDED WITH" + col.gameObject.name);

        //Identification par name
        //col.gameObject.name.Equals("CUBE")
        //col.gameObject.name.Contains("CUBE")
        // if (col.gameObject.name.ToUpper().Contains("CUBE")) {
        //     MyTools.ChangeColorRandom(col.gameObject);
        // }

        //Identification par Tag
        // if(col.gameObject.CompareTag("ChangeColor")) {
        //     MyTools.ChangeColorRandom(col.gameObject);
        // }

        //Identification par Layer
        // if((layerMask.value & (1<<col.gameObject.layer)) > 0) {
        //     MyTools.ChangeColorRandom(col.gameObject);
        // }

        //Identification par Component
        // Colorized colorize = col.gameObject.GetComponent<Colorized>();
        // if (colorize) {
        //     colorize.ChangeColor();
        // }

        //Identification par Interface
        IColorized colorized = col.gameObject.GetComponent<IColorized>();
        if (colorized != null) {
            colorized.Colorized();
        }

    }

    void OnTriggerEnter(Collider col) {
        MyTools.SpawnRandomPrimitives(4,col.transform.position,4);
    }


}
