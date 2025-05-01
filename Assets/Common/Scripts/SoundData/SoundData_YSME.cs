using UnityEngine;

// BGM 목록 enum (배열에 넣은 순서대로)
public enum BGM_YSME
{
    
}

// SFX 목록 enum (배열에 넣은 순서대로)
public enum SFX_YSME
{

}

[CreateAssetMenu(fileName = "SoundData_YSME", menuName = "Scriptable Object/Sound Data YSME")]
public class SoundData_YSME : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}