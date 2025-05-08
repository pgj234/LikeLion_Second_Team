using UnityEngine;

// BGM 목록 enum (배열에 넣은 순서대로)
public enum BGM_KYW
{
    
}

// SFX 목록 enum (배열에 넣은 순서대로)
public enum SFX_KYW
{
    BreakMirror,
    DDok,
    KwangKwang,
    CandleLight,
    DDok2,
    DoorUnlock,
    ChildLaugh,
    GhostChoir,
    GhostChoir2
}

[CreateAssetMenu(fileName = "SoundData_KYW", menuName = "Scriptable Object/Sound Data KYW")]
public class SoundData_KYW : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}