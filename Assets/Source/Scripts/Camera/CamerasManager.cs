using System;
using System.Collections;
using cakeslice;
using Cameras;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{
    public static CamerasManager Instance { get; private set; }

    [SerializeField] private LayerMask _triggersMask;

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    [Header("Cameras")][Space]
    [SerializeField] private OutlineCamera _defaultCamera;
    [Space]
    [SerializeField] private OutlineCamera _driverCamera;
    [Space]
    [SerializeField] private OutlineCamera _observerCamera;
    [Space]
    [SerializeField] private OutlineCamera _operatorCamera;

    [SerializeField][Space] private CamerasHolder _camerasHolder;

    private OutlineCamera _current;

    public OutlineCamera Current => _current;

    public enum CameraType
    {
        Default = 0,
        Driver,
        Operator,
        Observer
    }

    private void Start()
    {
        _driverCamera = _camerasHolder.DriverCamera;
        _observerCamera = _camerasHolder.ObserverCamera;
        _operatorCamera = _camerasHolder.OperatorCamera;
        _current = _defaultCamera;
        
        ChangeCamera(CameraType.Default);
    }
    
    public bool IsPointingAtOutlinedObject()
    {
        return Physics.Raycast(
            _current.Camera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, _current.Camera.nearClipPlane)), 
            _current.transform.forward,
            25,
            _triggersMask);
    }
    


    public void ChangeCamera(CameraType type)
    {
        OutlineCamera cam = null;
        switch (type)
        {
            case CameraType.Driver: cam = _driverCamera;
                break;
            case CameraType.Operator: cam = _operatorCamera;
                break;
            case CameraType.Observer: cam = _observerCamera;
                break;
            case CameraType.Default: cam = _defaultCamera;
                break;
            default:  cam =_defaultCamera;
                break;
                
        }
        
        Camera.SetupCurrent(cam.Camera);
        
        _current.enabled = false;
        _current = null;
        
        _current = cam;

        StartCoroutine(SetCurrentOutlineEffectInstance());
    }

    private IEnumerator SetCurrentOutlineEffectInstance()
    {
        yield return new WaitUntil(() => OutlineEffect.Instance == null);

        _current.enabled = true;

    }
}
