using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Kryz.Tweening;
using Random = UnityEngine.Random;
public class TestCoroutines : MonoBehaviour
{
    delegate float EasingFunctionDelegate(float t);
    delegate void SimpleAction(); //Action

    IEnumerator m_MyCoroutineRedf;
    //Start is called before the first frame update
    void Start() {
        //StartCoroutine(MyFirstCoroutine(1.5f));
        //StartCoroutine(InfiniteRescaleCoroutine());
        //StartCoroutine(InfiniteTranslateCoroutine());
        //StartCoroutine(TranslateFromAToBCoroutine(transform.position, transform.position+Random.onUnitSphere*4, 2.5f));
        m_MyCoroutineRedf = MultipleTranslateCoroutine(10);

        StartCoroutine(m_MyCoroutineRedf);
    }

    private void Update() {
        //MyTools.Log("Update");
    }

    IEnumerator MyFirstCoroutine (float duration) {
        //MyTools.Log("Start MyFirstCoroutine");
        //yield return new WaitForSeconds(duration);
        yield return null;
        //MyTools.Log("End MyFirstCoroutine");
    }

    IEnumerator InfiniteRescaleCoroutine() {
        while(true) {
            transform.localScale = Vector3.one * (1 + .25f * Mathf.Sin(10*Time.time));
            //MyTools.Log("InfiniteRescaleCoroutine");
            yield return null;
        }
    }

    IEnumerator InfiniteTranslateCoroutine() {
        while(true) {
            transform.position += Vector3.one * Time.deltaTime;
            //MyTools.Log("InfiniteTranslateCoroutine");
            yield return null;
        }
    }

    IEnumerator MultipleTranslateCoroutine(int nTranslations) {
        int cpt = 0;
        while (cpt < nTranslations) {
            yield return StartCoroutine(TranslateFromAToBCoroutine(transform.position, transform.position+Random.onUnitSphere*4, 0.5f, Kryz.Tweening.EasingFunctions.InOutBounce, () => MyTools.ChangeColorRandom(gameObject), () => MyTools.Log("End Action")));
            cpt++;
        }
    }

    IEnumerator TranslateFromAToBCoroutine(Vector3 startPos, Vector3 endPos, float duration, EasingFunctionDelegate easingFunctionDelegaten, Action startAction=null, Action endAction=null) {
        if (startAction != null) {
            startAction();
        }
        
        float elapsedTime = 0;
        while(elapsedTime < duration) {
            float k = elapsedTime/duration;
            //MyTools.Log(k.ToString());
            //transform.position = Vector3.Lerp(startPos,endPos,k);
            transform.position = Vector3.Lerp(startPos, endPos, Kryz.Tweening.EasingFunctions.InOutBounce(k));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        //StartCoroutine(TranslateFromAToBCoroutine(transform.position, transform.position+Random.onUnitSphere*4, 2.5f));
        if (endAction != null) {
            endAction();
        }
    }

    private void OnGUI() {
        if(GUI.Button(new Rect(10,10,150,50), "Stop Coroutine")){
            //StopAllCoroutines();
            StopCoroutine(m_MyCoroutineRedf);
            m_MyCoroutineRedf = null;
        }    
    }
}
