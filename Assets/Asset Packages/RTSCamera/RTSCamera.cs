using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSCamera
{
    // Parent the main camera to an empty gameobject and name it CameraRig
    // Only move the CameraRig's position to move the main camera, but do not rotate the CameraRig. CameraRig Rotation should be at (0, 0, 0) i.e. should be looking straight.
    // Rotate the main camera along x and z axis to whatever angle best fits your scene view. Don't rotate main camera along y-axis or you'll get weird lop-sided game view.

    // WASD to move around
    // Hold MMB to rotate view
    // Scroll to change height

    public class RTSCamera : MonoBehaviour
    {
        public RTSCameraSettings cameraSettings;

        float speed;
        float zoomSpeed;

        // Positions of mouse during rotation
        Vector2 p1;
        Vector2 p2;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = cameraSettings.shiftMultiplier* cameraSettings.unshiftSpeed;
                zoomSpeed = cameraSettings.shiftMultiplier *cameraSettings.unshiftZoomSpeed;
            }
            else
            {
                speed = cameraSettings.unshiftSpeed;
                zoomSpeed = cameraSettings.unshiftZoomSpeed;
            }

            float horizontalSpeed = transform.position.y * speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            float verticalSpeed = transform.position.y * speed * Input.GetAxis("Vertical") * Time.deltaTime;
            float scrollSpeed = transform.position.y * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;

            if ((transform.position.y >= cameraSettings.maxHeight) && (scrollSpeed > 0))
            {
                scrollSpeed = 0;
            }
            else if ((transform.position.y <= cameraSettings.minHeight) && (scrollSpeed < 0))
            {
                scrollSpeed = 0;
            }

            if ((transform.position.y + scrollSpeed) > cameraSettings.maxHeight)
            {
                scrollSpeed = (cameraSettings.maxHeight - transform.position.y) * Time.deltaTime;
            }
            else if ((transform.position.y + scrollSpeed) < cameraSettings.minHeight)
            {
                scrollSpeed = (cameraSettings.minHeight - transform.position.y) * Time.deltaTime;
            }

            Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
            Vector3 lateralMove = horizontalSpeed * transform.right;
            Vector3 forwardMove = transform.forward;
            forwardMove.y = 0;
            forwardMove.Normalize();
            forwardMove *= verticalSpeed;

            Vector3 move = verticalMove + lateralMove + forwardMove;

            transform.position += move;

            GetCameraRotation();
        }

        void GetCameraRotation()
        {
            if (Input.GetMouseButtonDown(2)) // Check if middle mouse is pressed
            {
                //Debug.Log("middle mouse pressed");
                p1 = Input.mousePosition;
            }

            if (Input.GetMouseButton(2)) // Check if the middle mouse is being held down
            {
                p2 = Input.mousePosition;

                float dx = (p2 - p1).x * cameraSettings.rotationSpeed * Time.deltaTime;
                float dy = (p2 - p1).y * cameraSettings.rotationSpeed * Time.deltaTime;

                transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0)); // Y rotation
                transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

                p1 = p2;
            }
        }
    }
}
