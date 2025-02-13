using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TouchDetector : MonoBehaviour
{
    [Header("AutoFilling")]
    [SerializeField]
    private ARRaycastManager _raycastManager;

    private static List<ARRaycastHit> s_hits = new List<ARRaycastHit>();

    public static UnityEvent<Vector3> PlaneTouchEvent = new UnityEvent<Vector3>();

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    private void OnValidate() 
    {
        _raycastManager ??= GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        CheckTouchPlane();
    }

    private void CheckTouchPlane()
    {
        if (!TryGetTouchOnScreen(out Vector2 touchPos) ||
            !_raycastManager.Raycast(touchPos, s_hits, TrackableType.Planes)) 
            return;

        print("hit");
        Pose hitPose = s_hits[0].pose;

        PlaneTouchEvent.Invoke(hitPose.position);
    }
    private bool TryGetTouchOnScreen(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }
}
