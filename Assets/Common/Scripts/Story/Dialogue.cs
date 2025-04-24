using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sprite leftImage;  // 왼쪽 이미지
    public Sprite rightImage; // 오른쪽 이미지
    [TextArea(3, 10)]        // 텍스트 영역을 더 크게 설정
    public string text;       // 대화 텍스트
} 