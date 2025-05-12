using UnityEngine;

public class DescriptionTrigger : MonoBehaviour
{
    public string description = "이 바위는 오래된 유물입니다."; // 표시할 설명
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger Enter on {gameObject.name}, Collider: {other.name}, Tag: {other.tag}");
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowDescription(description);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Trigger Exit on {gameObject.name}, Collider: {other.name}, Tag: {other.tag}");
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideDescription();
        }
    }
}