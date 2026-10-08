using UnityEngine;

/// <summary>
/// PlayerInputHandler decouples input detection from character physics logic.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool DashTriggered { get; private set; }

    [SerializeField] private bool useRawInput = true;

    private void Update()
    {
        ReadMovementInput();
        ReadActionInputs();
    }

    private void ReadMovementInput()
    {
        float x = useRawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float y = useRawInput ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");

        MovementInput = new Vector2(x, y).normalized;
    }

    private void ReadActionInputs()
    {
        IsSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        DashTriggered = Input.GetKeyDown(KeyCode.Space);
    }

    public void ResetTriggers()
    {
        DashTriggered = false;
    }
}