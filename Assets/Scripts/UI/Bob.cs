using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
 
    [SerializeField] private float amplitude = 10f;
    [SerializeField] private float period = 5f;
    private Vector3 startPos;
 
    protected void Start() 
    {
        startPos = transform.position;
    }
 
    protected void Update() 
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.up * distance;
    }
}
