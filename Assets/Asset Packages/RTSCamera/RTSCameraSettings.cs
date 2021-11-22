using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSCamera
{
    [CreateAssetMenu(fileName = "RTS Camera Setting", menuName = "RTS Camera/RTS Camera Setting")]
    public class RTSCameraSettings : ScriptableObject
    {
        [Tooltip("Normal speed without holding shift")]
        public float unshiftSpeed;
        [Tooltip("Normal scroll speed without holding shift")]
        public float unshiftZoomSpeed;
        [Tooltip("How fast camera can rotate")]
        public float rotationSpeed;
        [Tooltip("Speed is multiplied by this when holding shift")]
        public int shiftMultiplier;
        [Tooltip("Minimum height for camera")]
        public float minHeight;
        [Tooltip("Maximum height for camera")]
        public float maxHeight;
    }
}
