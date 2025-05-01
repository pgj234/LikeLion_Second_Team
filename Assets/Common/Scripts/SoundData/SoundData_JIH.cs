using UnityEngine;

// BGM 목록 enum (넣은 배열 순서대로)
public enum BGM_JIH
{

}

// SFX 목록 enum (넣은 배열 순서대로)
public enum SFX_JIH
{

}

public class SoundData_JIH : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}