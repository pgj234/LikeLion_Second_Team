using UnityEngine;

// BGM 목록 enum (배열에 넣은 순서대로)
public enum BGM_NSH
{
    
}

// SFX 목록 enum (배열에 넣은 순서대로)
public enum SFX_NSH
{

}

[CreateAssetMenu(fileName = "SoundData_NSH", menuName = "Scriptable Object/Sound Data NSH")]
public class SoundData_NSH : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}