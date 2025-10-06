using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestAppear : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            StartCoroutine("Appear");
        }
    }

    private IEnumerator Appear()
    {
        float appearanceTime = 2f;
        float timer = 0f;

        while (timer < appearanceTime)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(0, 1, (timer / appearanceTime)));
            yield return null;
        }

        if (timer > appearanceTime)
        {
            this.gameObject.SetActive(false);
        }
    }

}
