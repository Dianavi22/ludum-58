using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SuccessData", menuName = "Scriptable Objects/SuccessData")]
public class SuccessData : ScriptableObject
{
    public bool isSuccess;
    public string successName;
    public string successDescription;
    public Sprite successSprite;
    public string successKey;
    public bool showText;
    public bool showImage;


}
