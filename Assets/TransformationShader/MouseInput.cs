using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private Vector3 _mouseTouchStart;
    public Vector2 _inputTouchStart;
    [SerializeField] private RotationTransformation _rotationTransRef;
    [SerializeField] private ScaleTransformation _scaleTransRef;
    [SerializeField] private TransformationGrid _TransRef;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float scaleSpeed = 0.2f;
    public Vector3 _scaleDifference = new Vector3(0,0,0);
    private Vector3 _originalScale;
    public bool IsMouseControl = false;
    private bool _isInputScaling = false;

    private void Start()
    {
        _originalScale = _scaleTransRef.scale;
    }

    void Update()
    {
        //mouse input
        if (Input.GetMouseButtonDown(0))
        {
            IsMouseControl = true;
            _mouseTouchStart = Input.mousePosition;
            
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseDelta = Input.mousePosition - _mouseTouchStart; 
            _mouseTouchStart = Input.mousePosition; 

            float rotationX = mouseDelta.y * rotationSpeed; // Vertical movement rotates around X-axis
            float rotationY = -mouseDelta.x * rotationSpeed;  // Horizontal movement rotates around Y-axis

            // Create a quaternion for the world-space rotation
            Quaternion rotationDelta = Quaternion.Euler(rotationX, rotationY, 0);

            // Convert the world-space rotation to local rotation relative to the object's current rotation
            _rotationTransRef.rotation = rotationDelta * _rotationTransRef.rotation; // Apply new rotation (world rotation) first, then apply object's existing rotation
        }
        if(Input.GetMouseButtonUp(0))
        {
            IsMouseControl = false;
        }

        //Touch Input
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            IsMouseControl = true;
            _isInputScaling = false;
            _inputTouchStart = Input.GetTouch(0).position;
        }
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 InputDelta = Input.GetTouch(0).position - _inputTouchStart;
            //_touchStart = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);


            float rotationSpeed = 0.01f;
            float rotationX = InputDelta.y * rotationSpeed; // Vertical movement rotates around X-axis
            float rotationY = -InputDelta.x * rotationSpeed;  // Horizontal movement rotates around Y-axis

            // Create a quaternion for the world-space rotation
            Quaternion rotationDelta = Quaternion.Euler(rotationX, rotationY, 0);

            // Convert the world-space rotation to local rotation relative to the object's current rotation
            _rotationTransRef.rotation = rotationDelta * _rotationTransRef.rotation; // Apply new rotation (world rotation) first, then apply object's existing rotation
        }
        if (Input.touchCount == 2)
        {
            _isInputScaling = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;

            float preMagnitude = (touchOnePrePos - touchZeroPrePos).magnitude;
            float currentMagnitude = (touchOne.position - touchZero.position).magnitude;

            float difference = (currentMagnitude - preMagnitude) * scaleSpeed;
            _scaleTransRef.scale = _scaleTransRef.scale + new Vector3(difference, difference, difference);
        }
        if(Input.touchCount >=1 && _isInputScaling && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _scaleDifference = _scaleTransRef.scale - _originalScale;
            _originalScale = _scaleTransRef.scale;
            IsMouseControl = false;
            _isInputScaling = false;
        }
    }
}
