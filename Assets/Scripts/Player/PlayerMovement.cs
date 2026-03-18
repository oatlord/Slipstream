using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    [Header("Public Object References")]
    public GameObject hockeyStick;
    public GameObject puck;

    [Header("Control Settings")]
    [Tooltip("Rotation speed of the hockey stick around the puck.")]
    public float rotationSpeed = 100f;
    public float pushForce = 10f;
    public float slowDownSpeed = 5f;
    public float pushActionCooldown = 0.5f; // Cooldown duration in seconds

    // Private References
    private Rigidbody puckRigidbody;
    private bool isSlowDownPerformed = false;
    private bool isPushPerformed = false;
    private bool isPushOnCooldown = false;

    void Awake()
    {
        inputActions = new InputSystem_Actions();

        puckRigidbody = puck.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.MoveStick.performed += ctx => RotateAroundPuck();

        inputActions.Player.PushPuck.performed += ctx => isPushPerformed = true;
        inputActions.Player.PushPuck.canceled += ctx => isPushPerformed = false;

        inputActions.Player.StopPuck.performed += ctx => isSlowDownPerformed = true;
        inputActions.Player.StopPuck.canceled += ctx => isSlowDownPerformed = false;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();

        inputActions.Player.MoveStick.performed -= ctx => RotateAroundPuck();

        inputActions.Player.PushPuck.performed -= ctx => isPushPerformed = true;
        inputActions.Player.PushPuck.canceled -= ctx => isPushPerformed = false;

        inputActions.Player.StopPuck.performed -= ctx => isSlowDownPerformed = true;
        inputActions.Player.StopPuck.canceled -= ctx => isSlowDownPerformed = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(hockeyStick.transform.position, hockeyStick.transform.forward * pushForce, Color.red);

        Debug.Log(puckRigidbody.velocity);
        Debug.Log(isSlowDownPerformed);

        if (isSlowDownPerformed)
        {
            StopPuck();
        }

        if (isPushPerformed && !isPushOnCooldown)
        {
            PushPuck();
            StartCoroutine(PushCooldown());
        }
    }

    void RotateAroundPuck()
    {
        Vector2 moveInput = inputActions.Player.MoveStick.ReadValue<Vector2>();
        hockeyStick.transform.RotateAround(puck.transform.position, Vector3.up, moveInput.x * Time.deltaTime * rotationSpeed);
    }

    void PushPuck()
    {
        Debug.Log("Pushing puck!");
        puckRigidbody.AddForceAtPosition(hockeyStick.transform.forward * pushForce, hockeyStick.transform.position, ForceMode.Impulse);
    }

    void StopPuck()
    {
        puckRigidbody.velocity = Vector3.LerpUnclamped(puckRigidbody.velocity, Vector3.zero, Time.deltaTime * slowDownSpeed);
    }

    IEnumerator PushCooldown()
    {
        isPushOnCooldown = true;
        yield return new WaitForSeconds(pushActionCooldown);
        isPushOnCooldown = false;
    }
}
