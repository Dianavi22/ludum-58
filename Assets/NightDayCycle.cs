using System.Collections;
using UnityEngine;

public class NightDayCycle : MonoBehaviour
{
    [SerializeField] float timeCycle = 5f;
    [SerializeField] float switchCycle = 3f;
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
            if(opacity == 1)
            {
                StartCoroutine("NightCycle");
            }
            else
            {
                StartCoroutine("DayCycle");
            }
        }
    }

    private IEnumerator NightCycle()
    {
        while (timer < switchCycle)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(opacity, 0, (timer / switchCycle)));
            yield return null;
        }
        opacity = 0;
    }

    private IEnumerator DayCycle()
    {
        while (timer < switchCycle)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(opacity, 1, (timer / switchCycle)));
            yield return null;
        }
        opacity = 1;
    }




}
