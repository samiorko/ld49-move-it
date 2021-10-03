using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [FormerlySerializedAs("CameraDistance")] [Range(0f,100f)]
    public float cameraDistance;
    
    [FormerlySerializedAs("CameraHeight")] [Range(0f,100f)]
    public float cameraHeight;


    public float cameraSpeed;
    
    public Transform cameraTarget;

    private Vector3 _cameraVelocity;

    // Update is called once per frame
    void Update()
    {
        var offset = -transform.forward * cameraDistance;
        offset += Vector3.up * cameraHeight;

        var targetPos = transform.position + transform.forward + offset;

        var cameraSmoothedTarget = Vector3.SmoothDamp(
            Camera.main.transform.position,
            targetPos,
            ref _cameraVelocity,
            cameraSpeed
        );
        
        Camera.main.transform.position = cameraSmoothedTarget;
        Camera.main.transform.LookAt(cameraTarget);
    }
}
