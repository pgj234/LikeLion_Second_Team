using UnityEngine;

public class LightTrigger : MonoBehaviour
{

    public string playerTag = "Player"; // 플레이어 태그
    public Light[] lightsToTurnOn; // 켜질 라이트들 (인스펙터에서 설정)

    private void Awake()
    {
        Debug.Log($"LightTrigger on {gameObject.name} initialized. IsTrigger: {GetComponent<Collider2D>().isTrigger}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            foreach (Light light in lightsToTurnOn)
            {
                if (light != null)
                {
                    light.gameObject.SetActive(true); // 라이트 활성화
                    var lightScript = light.GetComponent<Light>();
                    if (lightScript != null)
                    {
                        lightScript.TurnOnLight();
                    }
                    
                }
                
            }
        }
    }
}
