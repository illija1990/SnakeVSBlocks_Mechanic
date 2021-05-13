using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private float _defaultWidth;
    private Camera _camera = Camera.main;

    private void Start()
    {
        _defaultWidth = _camera.orthographicSize * _camera.aspect; //( aspect = w/h )
    }

    private void Update()
    {
        _camera.orthographicSize = _defaultWidth / _camera.aspect;
    }
}
