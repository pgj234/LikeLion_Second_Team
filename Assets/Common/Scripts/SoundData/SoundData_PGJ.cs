using UnityEngine;

// BGM 목록 enum (넣은 배열 순서대로)
public enum BGM_PGJ
{

}

// SFX 목록 enum (넣은 배열 순서대로)
public enum SFX_PGJ
{

}

public class SoundData_PGJ : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}