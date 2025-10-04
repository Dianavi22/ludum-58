using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconSwapper : MonoBehaviour
{
    /// <summary>
    /// The default sprite to use for this image.
    /// </summary>
    [SerializeField, Tooltip("Default sprite to use for this image.")] Sprite _defaultSprite;

    /// <summary>
    /// The secondary sprite to use for this image.
    /// </summary>
    [SerializeField, Tooltip("Secondary sprite to use for this image.")] Sprite _secondarySprite;

    /// <summary>
    /// Image component.
    /// </summary>
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _defaultSprite; // TODO: change w/ default setting value if any
    }

    /// <summary>
    /// Swaps between [_defaultSprite] and [_secondarySprite].
    /// </summary>
    public void Swap()
    {
        if (_image.sprite == _defaultSprite)
        {
            _image.sprite = _secondarySprite;
        }
        else
        {
            _image.sprite = _defaultSprite;
        }
    }

    /// <summary>
    /// Swaps to the default sprite depending on [default].
    /// </summary>
    /// <param name="default">Whether to swap to the default sprite or not.</param>
    public void SwapTo(bool @default)
    {
        if (@default && _image.sprite != _defaultSprite)
        {
            _image.sprite = _defaultSprite;
        }
        else if (!@default && _image.sprite != _secondarySprite)
        {
            _image.sprite = _secondarySprite;
        }
    }
}
