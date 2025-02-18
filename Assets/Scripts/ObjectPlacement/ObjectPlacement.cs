using System;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    [Header("MVC components")]
    [SerializeField]
    private ObjectPlacementView _view;
    [SerializeField] 
    private ObjectPlacementModel _model;
    
    [SerializeField]
    private Transform _cameraTransform;

    public bool IsStarted { get; private set; } = false;
    private bool _objectPlaced = false;

    [Header("Spawn Settings")]
    //[SerializeField] private Vector3 spawnOffset = Vector3.zero;
    private Transform _placedObject; // —сылка на уже созданный лабиринт


    public static event Action<Transform> OnObjectPlaced;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        if (IsStarted) return;
        IsStarted = true;

        _view.onDropDownChange += value => _model.SetCurrentTransform(value);
        
        TouchDetector.PlaneTouchEvent.AddListener(OnTouch);
    }

    public static void RaiseOnObjectPlaced(Transform placedTransform)
    {
        OnObjectPlaced?.Invoke(placedTransform);
    }

    private void OnTouch(Vector3 touchPos)
    {
        if (!_objectPlaced)
        {
            if (!_model.currentGObj.activeSelf)
                _model.currentGObj.SetActive(true);

            _model.currentTransform.position = touchPos;// + spawnOffset;
            //_model.currentTransform.rotation = Quaternion.identity;

            Vector3 directionToCamera = _cameraTransform.position - _model.currentTransform.position;
            directionToCamera.y = 0; // только горизонтальное направление

            _model.currentTransform.rotation = Quaternion.LookRotation(directionToCamera);


            // Ќе мен€ем поворот, если это лабиринт
            _placedObject = _model.currentTransform;
            _objectPlaced = true;
            //OnObjectPlaced?.Invoke(_placedObject);

            TouchDetector.PlaneTouchEvent.RemoveListener(OnTouch);

        }
        else
        {
            _placedObject.position = touchPos;// + spawnOffset;
        }
    }

    public void CreateMazeByButton()
    {
        if (!_objectPlaced)
        {

            Vector3 spawnPosition = _cameraTransform.position + _cameraTransform.forward * 2.0f;// + spawnOffset;
            if (!_model.currentGObj.activeSelf)
                _model.currentGObj.SetActive(true);

            _model.currentTransform.position = spawnPosition;


            Vector3 directionToCamera = _cameraTransform.position - _model.currentTransform.position;
            directionToCamera.y = 0; // только горизонтальное направление

            _model.currentTransform.rotation = Quaternion.LookRotation(directionToCamera);


            _placedObject = _model.currentTransform;
            _objectPlaced = true;

            // ќтписываемс€ от касаний
            TouchDetector.PlaneTouchEvent.RemoveListener(OnTouch);
        }
    }
}
