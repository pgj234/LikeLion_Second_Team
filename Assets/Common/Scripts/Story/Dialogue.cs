using UnityEngine;

public enum ImageEffect
{
    None,
    Darken,
    RotateY,
    BounceY
}

[System.Serializable]
public class Dialogue
{
    public Sprite leftImage;  // 왼쪽 이미지
    public Sprite rightImage; // 오른쪽 이미지
    [Header("이미지 효과")]
    public ImageEffect leftImageEffect = ImageEffect.None;  // 왼쪽 이미지 효과
    public ImageEffect rightImageEffect = ImageEffect.None; // 오른쪽 이미지 효과
    [TextArea(3, 10)]        // 텍스트 영역을 더 크게 설정
    public string text;       // 대화 텍스트
} 