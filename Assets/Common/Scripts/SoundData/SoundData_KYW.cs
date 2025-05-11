using UnityEngine;

// BGM 목록 enum (배열에 넣은 순서대로)
public enum BGM_KYW
{
    Title,
    Rain,
    FirstCave,
    LastStage
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
    GhostChoir2,
    TiltleClick,
    TiltleHover,
    GhostState,
    LightOn,
    Meteor,
    ChairShiver,
    LastRoom,
    FallingSound,
    TwinkleSound,
    FallingPlatform,
    LightFly,
    JumpPlatform,
    WaterSound,
    LightOff,
    MountainBomb,
    MagmaBombSound,
    MagmaBoilSound,
    GroundMove,
    GroundBreak,
    PortalSound,
    TwinkleSound2,
    JumpSound,
    WallJumpSound,
    DashSound,
    SwordSound





}

[CreateAssetMenu(fileName = "SoundData_KYW", menuName = "Scriptable Object/Sound Data KYW")]
public class SoundData_KYW : ScriptableObject
{
    [SerializeField] internal AudioClip[] bgmClip;
    [SerializeField] internal AudioClip[] sfxClip;
}