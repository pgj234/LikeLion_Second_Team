using UnityEngine;
using UnityEngine.UI;

public class PlayerHit_WhiteCover_Controller : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float alphaValue;

    Image img;

    void Awake()
    {
        img = GameObject.Find("Canvas_PGJ/IMG_WhiteCover").GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            img.color = new Color(1, 1, 1, alphaValue);
        }
    }
}
