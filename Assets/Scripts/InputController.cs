using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField] private UnityEvent<Vector3> _onOneFingerTouch, _onTwoFingersMove;
    [SerializeField] private UnityEvent<Vector3> OnOneFingerMoves;
    [SerializeField] private UnityEvent _onTwoFingersMoveOut, _onTwoFingersMoveIn;
    [SerializeField] private UnityEvent<float> _onHorizontalFingerDisplacement, _onVerticalFingerDisplacement;
    private Vector3[] _previousFingerPosition = new Vector3[2] { Vector3.zero, Vector3.zero };
    private Vector3[] _normalizedMoveDirection = new Vector3[2] { Vector3.zero, Vector3.zero };
    private float _currentFingersDistance, _previousFingerDistance;
    private int _touchCount;
    public bool touchingScreen;

    void Update()
    {
        if (GameManager.Instance.CurrentSection == GameSection.MainMenu) return;
            _touchCount = Input.touchCount;
        if (_touchCount > 0)
        {
            touchingScreen = true;
            if (_touchCount > 2) return;

            if (_touchCount == 1)
            {
                ProcessOneFinger();
            }

            if (_touchCount == 2)
            {
                ProcessTwoFingers();
            }
        }
        else
        {
            touchingScreen = false;
        }
    }

    void ProcessTwoFingers()
    {
        if (Input.GetTouch(1).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _previousFingerPosition[1] = Input.GetTouch(1).position;
            _previousFingerPosition[0] = Input.GetTouch(0).position;
            _previousFingerDistance = Vector3.Distance(Input.GetTouch(1).position, Input.GetTouch(0).position);
        }

        CalculateFingersDirection();

        if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            if (_normalizedMoveDirection[0] == _normalizedMoveDirection[1])
                FingersMoveSameDirection();
            if (_normalizedMoveDirection[0] != _normalizedMoveDirection[1])
                FingersInOut();
        }

        if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(1).phase == TouchPhase.Stationary)
        {
            _currentFingersDistance = Vector3.Distance(Input.GetTouch(1).position, Input.GetTouch(0).position);
        }
    }

    void ProcessOneFinger()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OneFingerTouchs();
        }
        OneFingerMoves();
    }

    

    void CalculateFingersDirection()
    {
        Vector3 direction = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y) - _previousFingerPosition[0] ;
        _normalizedMoveDirection[0] = new Vector3(Mathf.RoundToInt(direction.normalized.x), Mathf.RoundToInt(direction.normalized.y));
        if (Input.touchCount > 1)
        {
            direction = new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y) - _previousFingerPosition[1];
            _normalizedMoveDirection[1] = new Vector3(Mathf.RoundToInt(direction.normalized.x), Mathf.RoundToInt(direction.normalized.y));            
        }
        else
        {
            _onHorizontalFingerDisplacement?.Invoke(Mathf.Sign(direction.x));
            _onVerticalFingerDisplacement?.Invoke(Mathf.Sign(direction.y));
        }
    }

    void FingersMoveSameDirection()
    {
        _previousFingerPosition[0] = Input.GetTouch(0).position;
        _onTwoFingersMove?.Invoke(_normalizedMoveDirection[1]);
    }

    void FingersInOut()
    {
        if (_previousFingerDistance != Vector3.Distance(Input.GetTouch(1).position, Input.GetTouch(0).position))
        {
            _currentFingersDistance = Vector3.Distance(Input.GetTouch(1).position, Input.GetTouch(0).position);

            if (_currentFingersDistance > _previousFingerDistance)
            {
                _onTwoFingersMoveOut?.Invoke();
            }
            if (_currentFingersDistance < _previousFingerDistance)
            {
                _onTwoFingersMoveIn?.Invoke();
            }
            _previousFingerDistance = Vector3.Distance(Input.GetTouch(1).position, Input.GetTouch(0).position);
        }
    }

    void OneFingerTouchs()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _onOneFingerTouch?.Invoke(Input.GetTouch(0).position);
            _previousFingerPosition[0] = Input.GetTouch(0).position;
        }
    }

    void OneFingerMoves()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            CalculateFingersDirection();
            OnOneFingerMoves?.Invoke(Input.GetTouch(0).position);
            _previousFingerPosition[0] = Input.GetTouch(0).position;
        }
    }
}