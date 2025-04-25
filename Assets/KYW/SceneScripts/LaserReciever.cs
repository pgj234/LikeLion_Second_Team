using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public GameObject obj; // 반응할 오브젝트

    public void Activate()
    {
        Debug.Log("수신기 작동됨!");
        // 반응할거
        obj.SetActive(true);
    }
}
