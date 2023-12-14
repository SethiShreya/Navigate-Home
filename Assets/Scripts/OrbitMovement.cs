using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float zoomSpeed = 2.0f;
    public float minZoomFOV = 20.0f;  // Set the minimum FOV value for zooming in
    public float maxZoomFOV = 60.0f;  // Set the maximum FOV value for zooming out

    private Vector3 lastMousePosition;

    void Update()
    {
        // Check for left mouse button drag to rotate
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            //float rotationX = mouseDelta.y * rotationSpeed;
            float rotationY = -mouseDelta.x * rotationSpeed;
            //transform.Rotate(Vector3.right, rotationX, Space.World);
            transform.Rotate(Vector3.up, rotationY, Space.World);
        }

        // Store the current mouse position for the next frame
        lastMousePosition = Input.mousePosition;

        // Zoom using the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scrollInput * zoomSpeed);
    }

    void ZoomCamera(float zoomAmount)
    {
        // Calculate the new FOV based on the zoom input
        float newFOV = Camera.main.fieldOfView - zoomAmount;

        // Clamp the new FOV within the specified range
        newFOV = Mathf.Clamp(newFOV, minZoomFOV, maxZoomFOV);

        // Apply the new FOV to the camera
        Camera.main.fieldOfView = newFOV;

        // Log or display the FOV value
        //Debug.Log("FOV (Zoom): " + newFOV);
    }
}
