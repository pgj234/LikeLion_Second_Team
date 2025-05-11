using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoveCreditPlayer_Trigger : MonoBehaviour
{
    private InputManager input;

    [SerializeField] public string triggerTag = "Player";

    [SerializeField] public UnityEvent<float> floatFunction1;
    [SerializeField] private float value1;
    [SerializeField] private float delay1 = 0;
    [SerializeField] public UnityEvent<float> floatFunction2;
    [SerializeField] private float value2;
    [SerializeField] private float delay2 = 0;
    [SerializeField] public UnityEvent<bool> boolFunction1;
    [SerializeField] private bool boolValue1;
    [SerializeField] private float delay3 = 0;
    [SerializeField] public UnityEvent<bool> boolFunction2;
    [SerializeField] private bool boolValue2;
    [SerializeField] private float delay4 = 0;

    void Start()
    {
        input = InputManager.instance;
    }

    void Update()
    {
    }

    void Triggered()
    {
        StartCoroutine(TriggerFunction1(delay1));
        StartCoroutine(TriggerFunction2(delay2));
        StartCoroutine(TriggerFunction3(delay3));
        StartCoroutine(TriggerFunction4(delay4));
    }

    IEnumerator TriggerFunction1(float time)
    {
        yield return new WaitForSeconds(time);
        floatFunction1?.Invoke(value1);
    }
    IEnumerator TriggerFunction2(float time)
    {
        yield return new WaitForSeconds(time);
        floatFunction2?.Invoke(value2);
    }
    IEnumerator TriggerFunction3(float time)
    {
        yield return new WaitForSeconds(time);
        boolFunction1?.Invoke(boolValue1);
    }
    IEnumerator TriggerFunction4(float time)
    {
        yield return new WaitForSeconds(time);
        boolFunction2?.Invoke(boolValue2);
    }

    public void ChangeXInput(float x)
    {
        input.SetXInput(x);
    }
    public void ChangeYInput(float y)
    {
        input.SetYInput(y);
    }
    public void ChangeFInput(bool f)
    {
        input.SetFInput(f);
    }
    public void ChangeFInputRelease(bool f)
    {
        input.SetFReleaseInput(f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            Triggered();
        }
    }
}
