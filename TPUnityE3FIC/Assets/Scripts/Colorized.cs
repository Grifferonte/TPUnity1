using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorized : MonoBehaviour
{
    public void ChangeColor() {
        MyTools.ChangeColorRandom(gameObject);
    }
}
