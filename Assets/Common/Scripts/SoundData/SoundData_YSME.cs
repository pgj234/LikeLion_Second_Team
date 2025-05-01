using UnityEngine;

// BGM 목록 enum (넣은 배열 순서대로)
public enum BGM_YSME
{

}

// SFX 목록 enum (넣은 배열 순서대로)
public enum SFX_YSME
{

}

public class SoundData_YSME : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}