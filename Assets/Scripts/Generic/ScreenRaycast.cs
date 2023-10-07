using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenRaycast : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _collisionLayerMask;
    private Vector3 raycasCollisionCordinates;
    [SerializeField] private UnityEvent<GameObject> _onRaycastCollision;
    [SerializeField] private UnityEvent<GameObject> _onRaycastCollisionFail;


    public void ThrowRayScreenToWorld(Vector3 screenPosition)
    {
        Ray worldPoint = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(worldPoint, out RaycastHit raycastHit, 1000, _collisionLayerMask))
        {
            raycasCollisionCordinates = raycastHit.point;
            _onRaycastCollision?.Invoke(raycastHit.collider.gameObject);
        }
        else
            _onRaycastCollisionFail?.Invoke(null);
    }
}
