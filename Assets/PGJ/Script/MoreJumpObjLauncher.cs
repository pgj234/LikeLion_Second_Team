using UnityEngine;

public class MoreJumpObjLauncher : MonoBehaviour
{
    [SerializeField] GameObject shotPrefab;
    [SerializeField] float shotSpeed;
    [SerializeField] float shotDelay;

    float timer;

    void Awake()
    {
        timer = shotDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = shotDelay;

            GameObject obj = Instantiate(shotPrefab);
            obj.transform.position = transform.position;
            obj.GetComponent<ProjectileMove>().Init(transform.up, shotSpeed);
        }
    }
}
