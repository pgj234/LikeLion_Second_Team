using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public GameObject door; // 문이나 어떤 반응할 오브젝트

    public void Activate()
    {
        Debug.Log("수신기 작동됨!");
        door.SetActive(false); // 문 열리게 한다든가
    }
}
