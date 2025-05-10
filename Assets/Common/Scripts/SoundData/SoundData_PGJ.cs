using UnityEngine;

// BGM 목록 enum (배열에 넣은 순서대로)
public enum BGM_PGJ
{
    Bgm
}

// SFX 목록 enum (배열에 넣은 순서대로)
public enum SFX_PGJ
{
    Wind,
    StoneCrash,
    StoneMove,
    StoneFallImpact
}

[CreateAssetMenu(fileName = "SoundData_PGJ", menuName = "Scriptable Object/Sound Data PGJ")] 
public class SoundData_PGJ : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}