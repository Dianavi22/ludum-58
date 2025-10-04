using TMPro;
using UnityEngine;

public class Success : MonoBehaviour
{
    private SpriteRenderer _mySprite;
    [SerializeField] private SuccessData _successDatas;

    public bool canBeShowed;
    public bool canBeOnlyImageView;
    public bool canBeOnlyTextView;

    [SerializeField] float targetWidth;
    [SerializeField] float targetHeight;
    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] TextMeshProUGUI _descriptionText;
    [SerializeField] Sprite _unknownSprite;

    private string _name;
    private string _description;
    private Sprite _sprite;

    void Awake()
    {
        canBeShowed = _successDatas.isSuccess;
        canBeOnlyImageView = _successDatas.showImage;
        canBeOnlyTextView = _successDatas.showText;
        _name = _successDatas.successName;
        _description = _successDatas.successDescription;
        _sprite = _successDatas.successSprite;
    }


    private void ShowSuccess(Sprite sprite, string name, string description)
    {
        _mySprite = GetComponentInChildren<SpriteRenderer>();
        _mySprite.sprite = sprite;

        float spriteWidth = _mySprite.sprite.rect.width / _mySprite.sprite.pixelsPerUnit;
        float spriteHeight = _mySprite.sprite.rect.height / _mySprite.sprite.pixelsPerUnit;

        float scaleX = targetWidth / spriteWidth;
        float scaleY = targetHeight / spriteHeight;

        _mySprite.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        _titleText.text = name;
        _descriptionText.text = description;
    }

    public void ShowOnlyName()
    {
        ShowSuccess(_unknownSprite, _name, "????????");
    }

    public void ShowAllSuccess()
    {
        ShowSuccess(_sprite, _name, _description);
    }

    public void ShowOnlySprite()
    {
        ShowSuccess(_sprite, "????????", "????????");
    }
}
