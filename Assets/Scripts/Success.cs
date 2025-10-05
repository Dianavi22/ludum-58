using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Success : MonoBehaviour
{
    private SpriteRenderer _mySprite;
    public SuccessData SuccessDatas;

    [SerializeField] float targetWidth;
    [SerializeField] float targetHeight;
    [SerializeField] TextMeshPro _titleText;
    [SerializeField] TextMeshPro _descriptionText;
    [SerializeField] Sprite _unknownSprite;




    private void ShowSuccess(Sprite sprite, string name, string description)
    {
        _mySprite = GetComponentInChildren<SpriteRenderer>();
        _mySprite.sprite = sprite;

        //float spriteWidth = _mySprite.sprite.rect.width / _mySprite.sprite.pixelsPerUnit;
        //float spriteHeight = _mySprite.sprite.rect.height / _mySprite.sprite.pixelsPerUnit;

        //float scaleX = targetWidth / spriteWidth;
        //float scaleY = targetHeight / spriteHeight;

        //_mySprite.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        _titleText.text = name;
        _descriptionText.text = description;
    }

    public void ShowOnlyName()
    {
        ShowSuccess(_unknownSprite, SuccessDatas.successName, "?????");
    }

    public void ShowAllSuccess()
    {
        ShowSuccess(SuccessDatas.successSprite, SuccessDatas.successName, SuccessDatas.successDescription);
    }

    public void ShowOnlySprite()
    {
        ShowSuccess(SuccessDatas.successSprite, "?????", "?????");
    }
}
