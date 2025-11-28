using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    Rigidbody RB;
    InputAction moveAction;
     [SerializeField] Transform cam; 
  
    void Start()
    {
          moveAction = InputSystem.actions.FindAction("Move");
          RB = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        move(); 
    }

     private void move()
    {

        Vector3 moveValue = moveAction.ReadValue<Vector3>() * Time.deltaTime;
        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Create movement direction relative to camera
        Vector3 moveDirection = (cameraForward * moveValue.z + cameraRight * moveValue.x);

        // Apply force
        RB.AddForce(moveDirection * 4000f * Time.deltaTime, ForceMode.VelocityChange);

    }
}
