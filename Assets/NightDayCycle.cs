using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightDayCycle : MonoBehaviour
{
    [SerializeField] float timeCycle = 5f;
    [SerializeField] SpriteRenderer sr;

    float timer = 0f;
    float opacity = 1f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeCycle)
        {
            timer = 0f;
            StartCoroutine("Cycle");
        }
    }

    private IEnumerator Cycle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        sr.Color.a = Mathf.Lerp(opacity, timeCycle, waitTime);
    }

}
