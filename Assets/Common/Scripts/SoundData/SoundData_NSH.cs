using UnityEngine;

// BGM 목록 enum (넣은 배열 순서대로)
public enum BGM_NSH
{

}

// SFX 목록 enum (넣은 배열 순서대로)
public enum SFX_NSH
{

}

public class SoundData_NSH : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}