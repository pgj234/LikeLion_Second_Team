using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public GameObject obj; // 반응할 오브젝트
    private bool isActive = true; //수신기

    public void Activate()
    {
        if(!isActive) return;
        // Debug.Log("수신기 작동됨!");
        // // 반응할거
        obj.SetActive(true);
        SoundManager.instance.PlaySFX(SFX_KYW.CandleLight);
        isActive = false;
    }
}
