using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof(MeshRenderer), typeof(MeshFilter))]
public class TransformationGrid : MonoBehaviour
{
    public Transform prefab;
    public int gridResolution = 10;
    Transform[] grid;
    List<Transformation> transformations;
    Matrix4x4 transformation;
    float time;
    ScaleTransformation _scaleTransRef;
    RotationTransformation _rotationTransRef;
    Quaternion _startingRotation;
    public Quaternion RotationDelta;
    [SerializeField] MouseInput _mouseInputRef;

    void Awake()
    {
        _scaleTransRef = GetComponent<ScaleTransformation>();
        _rotationTransRef = GetComponent<RotationTransformation>();

        grid = new Transform[gridResolution * gridResolution * gridResolution];
        
        for(int i=0, z =0; z < gridResolution; z++)
        {
            for(int y = 0; y < gridResolution; y++)
            {
                for(int x =0; x< gridResolution; x++, i++)
                {
                    grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }
        transformations = new List<Transformation>();
        _rotationTransRef.rotation = Quaternion.Euler(300, 0, 315);
        _startingRotation = _rotationTransRef.rotation;
    }


    private void Update()
    {
        
        UpdateTransformation();
        for (int i = 0, z = 0; z < gridResolution; z++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
                for (int x = 0; x < gridResolution; x++, i++)
                {
                    grid[i].localPosition = TransformPoint(x, y, z);
                    grid[i].gameObject.GetComponent<MeshRenderer>().material.color = UpdateCubeColour(x, y, z);
                }
            }
        }

        if(_mouseInputRef.IsMouseControl == false)
        {
            time += Time.deltaTime;
            _scaleTransRef.scale = new Vector3(1 + Mathf.Sin(time) * 0.5f + 0.5f, 1 + Mathf.Sin(time) * 0.5f + 0.5f, 1 + Mathf.Sin(time) * 0.5f + 0.5f);
            _rotationTransRef.rotation =  Quaternion.Euler(
                _rotationTransRef.rotation.eulerAngles.x,
                _rotationTransRef.rotation.eulerAngles.y +20*Time.deltaTime,
                _rotationTransRef.rotation.eulerAngles.z);
        }
    }

    private Transform CreateGridPoint(int x,int y,int z)
    {
        Transform cube = Instantiate(prefab);
        cube.localPosition = GetCoordinates(x, y, z);
        //cube.GetComponent<MeshRenderer>().material.color = new Color(
        //    (float)x / gridResolution, 
        //    (float)y / gridResolution, 
        //    (float)z / gridResolution);
        
        return cube;
    }
    private Color UpdateCubeColour(int x, int y, int z)
    {
        Color color = new Color(
            (float)((float) x / gridResolution + ((Mathf.Sin(time) * 0.5f + 0.5f)))%1,
            (float)y / gridResolution ,
            (float)z / gridResolution);
        return color;
    }
    Vector3 GetCoordinates(int x, int y, int z) // Make the centre of the grid to 0,0,0
    {
        return new Vector3(x - (gridResolution - 1) /2f, y - (gridResolution - 1) /2f,z - (gridResolution - 1) /2f);
    }

    private Vector3 TransformPoint(int x, int y, int z)
    {
        //Vector3 coordinates = GetCoordinates(x, y, z);
        //for(int i =0; i < transformations.Count; i++)
        //{
        //    coordinates = transformations[i].Apply(coordinates);
        //}
        //return coordinates;

        Vector3 coordinates = GetCoordinates(x, y, z);
        return transformation.MultiplyPoint(coordinates);
    }

    private void UpdateTransformation()
    {
        GetComponents<Transformation>(transformations); //This will fill this transfomation list with all components of type Transformation found 

        if (transformations.Count > 0)
        {
            transformation = transformations[0].Matrix;
            for (int i =1; i< transformations.Count; i++)
            {
                transformation = transformations[i].Matrix * transformation;
            }
        }
    }

}
