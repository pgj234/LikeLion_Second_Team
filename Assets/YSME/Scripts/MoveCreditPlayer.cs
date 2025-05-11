using UnityEngine;

public class MoveCreditPlayer : MonoBehaviour
{
    private InputManager input;

    void Start()
    {
        input = InputManager.instance;
        input.InputStop();
    }

    void Update()
    {
        input.SetXInput(1);
    }
}
