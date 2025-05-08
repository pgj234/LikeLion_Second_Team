using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class CameraSet
{
    public CinemachineCamera camera;

    [Tooltip("카메라가 연출 하는 총 시간")]
    public float lookTime;
    public int priority;
}

public class CameraProduction : MonoBehaviour
{
    [Header("연출용 카메라")]
    [SerializeField] private CameraSet[] cameraSet;

    [Space, Header("연출 설정")]
    [SerializeField] private bool Loop = false;
    private bool alreadyPlayed = false;

    public void StartProduction()
    {
        StartCoroutine(TriggerProduction());
    }

    IEnumerator TriggerProduction()
    {
        if (alreadyPlayed == true && Loop == false) yield break;

        alreadyPlayed = true;
        InputManager.instance?.InputStop();

        foreach (CameraSet set in cameraSet)
        {
            set.camera.Priority = set.priority;
            set.camera.gameObject.SetActive(true);

            yield return new WaitForSeconds(set.lookTime);

            set.camera.Priority = 0;
            set.camera.gameObject.SetActive(false);
        }

        InputManager.instance?.InputStart();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TriggerProduction());
        }
    }
}
