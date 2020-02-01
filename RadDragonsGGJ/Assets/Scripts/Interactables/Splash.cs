using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public float existTime = 3.0f;

    private IEnumerator existence;

    // Start is called before the first frame update
    void Awake()
    {
        existence = Exist(existTime);
        StartCoroutine(existence);
    }

    private IEnumerator Exist(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyImmediate(this.gameObject);
    }


}
