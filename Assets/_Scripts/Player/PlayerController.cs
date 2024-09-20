using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;

    [Range(0f, 30f)]
    public float speed = 5f;

    private float gravity;
    private Vector2 direction;

    public LayerMask testLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        gravity = Physics.gravity.y;
        InputManager.Instance.controller = this;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //grab camera forward and camera right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        //remove yaw rotation
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 desiredMoveDirection = cameraForward * -direction.y + cameraRight * direction.x;

        if (desiredMoveDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 5f * Time.deltaTime);
        }

        desiredMoveDirection *= speed * Time.deltaTime;

        float YVel = (!cc.isGrounded) ? gravity * Time.deltaTime : 0;

        desiredMoveDirection.y = YVel;

        cc.Move(desiredMoveDirection);

        Vector3 startingPos = transform.position;
        startingPos.y += 0.5f;

        Ray ray = new Ray(startingPos, transform.forward);
        RaycastHit hit;

        Debug.DrawLine(startingPos, startingPos + (transform.forward * 10), Color.magenta);

        if (Physics.Raycast(ray, out hit, 10f, testLayerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

   public void MoveStarted(InputAction.CallbackContext ctx)
{
    Vector2 inputDirection = ctx.ReadValue<Vector2>();
    direction = new Vector2(inputDirection.x, -inputDirection.y); // Invert y-axis
}


    public void MoveCanceled(InputAction.CallbackContext ctx)
    {
        direction = Vector2.zero;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}