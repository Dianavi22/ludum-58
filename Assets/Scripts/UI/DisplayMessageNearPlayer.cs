using TMPro;
using UnityEngine;

public class DisplayMessageNearPlayer : MonoBehaviour
{   
    /// <summary>
    /// The text to be displayed when the player is nearby.
    /// </summary>
    [SerializeField, Tooltip("The text assigned to the TextMeshPro text.")] private string _text;

    /// <summary>
    /// Reference to child TMP object which will be shown when the player is nearby.
    /// </summary>
    private TextMeshPro _tmp;

    private void Awake()
    {
        _tmp = GetComponentInChildren<TextMeshPro>();
        _tmp.text = _text;
        _tmp.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            _tmp.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            _tmp.gameObject.SetActive(false);
        }
    }
}
