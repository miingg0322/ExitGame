using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    PlayerInput input;

    [SerializeField] private float mouseSensitivity = 10f;

    Vector2 mouseLook;
    float xRotation;
    public Transform player;

    private void Awake()
    {
        player = transform.parent;
        input = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look()
    {
        mouseLook = input.PlayerAction.Look.ReadValue<Vector2>();
        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        Quaternion rot = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rot, Time.deltaTime * 10f);
        player.Rotate(Vector3.up * mouseX);
    }
}
