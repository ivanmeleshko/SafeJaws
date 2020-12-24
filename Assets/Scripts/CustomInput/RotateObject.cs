
using System;
using UnityEngine;

namespace CustomInput
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private int _speed = 12;
        [SerializeField] private float _friction = 0.5f;
        [SerializeField] private float _lerpSpeed = 1.5f;
        [SerializeField] private float _lerpZoomSpeed = 1.5f;
        [SerializeField] private float _zoomDelta = 10;


        [SerializeField] private float _minZoom = 320;
        [SerializeField] private float _maxZoom = 430;

        private float _positionZ = 429;
        private bool _allowInertialDrag = false;
        private Vector3 _toPosition;
        private Quaternion _fromRotation;
        [SerializeField] private Quaternion _toRotation;
        private float _xDeg;
        private float _yDeg;
        private bool _executeRotation = false;
        private bool _executeScale = false;

        public void OnEnable()
        {
            DragControlls.OnDragAction += OnDragAction;
            DragControlls.OnPointerAction += OnPointerAction;
        }

        private void OnDisable()
        {
            DragControlls.OnDragAction -= OnDragAction;
            DragControlls.OnPointerAction -= OnPointerAction;
        }

        private void OnDragAction(bool executeRotation)
        {
            _executeRotation = executeRotation;
        }

        private void OnPointerAction(bool allow)
        {
            _executeScale = allow;
        }

        private void Update()
        {
            RotateModel(Time.deltaTime);
            ZoomModel(Time.deltaTime, Input.mouseScrollDelta.y);
        }

        private bool _firstClick = false;

        private void RotateModel(float deltaTime)
        {
            if (Input.GetMouseButton(0) || _allowInertialDrag)
            {
                if (Input.GetMouseButton(0) && _executeRotation)
                                            //&& Math.Abs(Input.mousePosition.x) < 1000)
                {

                    _xDeg += -Mathf.Clamp(Input.GetAxis("Mouse X"), -5, 5) * _speed * _friction;
                    _yDeg += Mathf.Clamp(Input.GetAxis("Mouse Y"), -5, 5) * _speed * _friction;
                    _firstClick = true;
                }

                if (_firstClick)
                {

                    _fromRotation = transform.rotation;
                    _toRotation = Quaternion.Euler(_yDeg, _xDeg, 0);
                    transform.rotation = Quaternion.Lerp(_fromRotation, _toRotation, Time.deltaTime * _lerpSpeed);

                    if (transform.rotation == _toRotation)
                        _allowInertialDrag = false;
                    else
                        _allowInertialDrag = true;
                }
            }
        }

        private void ZoomModel(float deltaTime, float scroolPosition)
        {
            if (_executeScale)
            {
                if (scroolPosition >= 1)
                {
                    _positionZ = Mathf.Clamp(_positionZ - _zoomDelta, _minZoom, _maxZoom);
                }
                else if (scroolPosition <= -1)
                {
                    _positionZ = Mathf.Clamp(_positionZ + _zoomDelta, _minZoom, _maxZoom);
                }

                _toPosition = transform.localPosition;
                _toPosition.z = _positionZ;
                transform.localPosition =
                    Vector3.Lerp(transform.localPosition, _toPosition, deltaTime * _lerpZoomSpeed);

            }
        }
    }
}