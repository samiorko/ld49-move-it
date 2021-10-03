using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public Transform target;

    public float rotationSpeed;
    public float jumpingSpeed;
    public float jumpingDistance;


    private Vector3 _startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position += Vector3.up * target.localScale.y - Vector3.up + Vector3.up * jumpingDistance;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _startPos + (Vector3.up * (Mathf.Sin(Time.time * jumpingSpeed) * jumpingDistance));
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
