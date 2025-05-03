using UnityEngine;

// BGM 목록 enum (배열에 넣은 순서대로)
public enum BGM_JIH
{
    
}

// SFX 목록 enum (배열에 넣은 순서대로)
public enum SFX_JIH
{

}

[CreateAssetMenu(fileName = "SoundData_JIH", menuName = "Scriptable Object/Sound Data JIH")]
public class SoundData_JIH : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}