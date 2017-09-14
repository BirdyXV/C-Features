using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitWithPanAndZoom : MonoBehaviour
{
    public Transform target; // Target object to orbit around
    public float panSpeed = 5f; // Speed of panning
    public float sensitivity = 1f; // Sensitivity of mouse

    // Minimum & maxium zoom distance
    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private float distance = 0f; // Current distance between target and camera

    // Stored X & Y euler rotation
    private float x = 0.0f;
    private float y = 0.0f;

    // Create an enum to use for mouse input (just for readability)
    public enum MouseButton
    {
        LEFTMOUSE = 0,
        RIGHTMOUSE = 1,
        MIDDLEMOUSE = 2,

    }

    // Use this for initialization
    void Start()
    {
        // CALL target transform's SetParent(null)
        target.transform.SetParent(null); // Detaches the target from children

        // SET distance = Vector3.Distance(target's position, transform's position)
        distance = Vector3.Distance(target.transform.position, transform.position); // Calculates distance to target

        // LET angles = transform's eulerAngles
        Vector3 angles = transform.eulerAngles; // Records the current euler rotation
        // SET x = angles.x
        x = angles.x;
        // SET y = angles.y
        y = angles.y;
    }

    void Orbit()
    {
        // SET x = x + Input Axis "Mouse Y" x sensitivty
        x = x + Input.GetAxis("Mouse Y") * sensitivity;
        // SET y = y + Input Axis "Mouse X" x sensitivty
        y = y - Input.GetAxis("Mouse X") * sensitivity;
    }

    void Movement()
    {
        // IF target != null
        if (target != null)
        {
            // LET rotation = Quaternion Euler(x, x, 0)
            Quaternion rotation = Quaternion.Euler(x, y, 0);

            // LET desiredDist = distance - Input Axis "Mouse ScrollWheel"
            float desiredDist = distance - Input.GetAxis("Mouse ScrollWheel");

            // SET desiredDist = desiredDist x sensitivty
            desiredDist = desiredDist * sensitivity; // Amplifies desiredDist by sensitivty (Scroll Speed)

            // SET distance = Mathf Clamp (desiredDist, distanceMin, distanceMax)
            distance = Mathf.Clamp(desiredDist, distanceMin, distanceMax); // Clamps the result so that distance doesn't go outside of constraints

            // LET invDistanceZ = new Vector3(0, 0, -distance)
            Vector3 invDistanceZ = new Vector3(0, 0, -distance);

            // SET invDistanceZ = rotation x invDistanceZ
            invDistanceZ = rotation * invDistanceZ; // Rotates the direction of vector to be local to camera

            // LET position = target.position
            Vector3 position = target.position + invDistanceZ;

            // SET transform.rotation = rotation
            transform.rotation = rotation;
            // SET transform.position = position
            transform.position = position;
        }
    }

    void Pan()
    {
        // LET inputX = -Input GetAxis "Mouse X"
        float inputX = -Input.GetAxis("Mouse X");
        // LET inputY = -Input GetAxis "Mouse Y"
        float inputY = -Input.GetAxis("Mouse Y");

        // LET inputDir = new Vector3(inputX, inputY)
        Vector3 inputDir = new Vector3(inputX, inputY);

        // LET movement = transform.TransformDirecion(inputDir)
        Vector3 movement = transform.TransformDirection(inputDir);
        // SET target.transform.position += movement x panSpeed x deltaTime
        target.transform.position += movement * panSpeed * Time.deltaTime;
    }

    // Hides/Unhides the cursor
    void HideCursor(bool isHiding)
    {
        // IF isHiding
        if (isHiding)
        {
            // Lock the cursor  
            Cursor.lockState = CursorLockMode.Locked;
            // Hide the cursor
            Cursor.visible = false;

        }
        // ELSE
        else
        {
            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            // Unhide the cursor
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        // IF Input MouseButton Right
        if (Input.GetMouseButton((int)MouseButton.RIGHTMOUSE))
        {
            // CALL HideCursor(true)
            HideCursor(true); // Hides the cursor
            Orbit(); // Update orbit of the camera
        }
        // ELSE IF Input MouseButton Middle
        else if (Input.GetMouseButton((int)MouseButton.MIDDLEMOUSE))
        {
            HideCursor(true);
            Pan(); // Pans the camera
        }
        // ELSE
        else
        {
            HideCursor(false); // Unhides the cursor
            Movement(); // Always update movement regardless of any input
        }
    }
}
