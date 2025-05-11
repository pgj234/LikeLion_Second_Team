using System.Collections;
using UnityEngine;

public class MoveCreditPlayer : MonoBehaviour
{
    private InputManager input;

    private float time = 2;
    private float curTime = 0;

    void Start()
    {
        input = InputManager.instance;
        input.InputStop();

    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (time > curTime)
        {
            input.SetXInput(1);
        }
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1);
        input.SetXInput(1);
        input.SetYInput(1);
        Debug.Log(input.xInput);
        Debug.Log(input.yInput);
        yield return new WaitForSeconds(1);
        Debug.Log(input.xInput);
        Debug.Log(input.yInput);
    }
}
