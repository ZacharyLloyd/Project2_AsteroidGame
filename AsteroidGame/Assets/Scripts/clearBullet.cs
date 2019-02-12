using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearBullet : MonoBehaviour
{

    private int time = 0;
    private IEnumerator coroutine;

    // before the first frame
    void Start()
    {
        coroutine = TimeDestoryObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            time++;
            Debug.Log("Seconds passed: " + time);
            if (time > 60)
                Destroy(gameObject);
            StartCoroutine(coroutine);
        }
    }
    IEnumerator TimeDestoryObject()
    {
        yield return new WaitForSeconds(1);
    }
}
