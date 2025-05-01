using UnityEngine;

// BGM 목록 enum (넣은 배열 순서대로)
public enum BGM_KYW
{

}

// SFX 목록 enum (넣은 배열 순서대로)
public enum SFX_KYW
{

}

public class SoundData_KYW : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}