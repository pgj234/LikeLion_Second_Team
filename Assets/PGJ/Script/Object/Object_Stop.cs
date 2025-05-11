using UnityEngine;
using UnityEngine.SceneManagement;

public class Object_Stop : MonoBehaviour
{
    [SerializeField] Vector3 destinationVectorPos;

    bool isStop = false;

    void Update()
    {
        if (true == isStop)
        {
            return;
        }

        if (destinationVectorPos == transform.localPosition)
        {
            isStop = true;

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            if (0 == string.Compare(SceneManager.GetActiveScene().name, "Team9"))
            {
                SoundManager.instance.PlaySFX(SFX_PGJ.StoneFallImpact);
            }
        }
    }
}
