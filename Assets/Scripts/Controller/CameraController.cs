using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class CameraController
    {
        private LevelObjectView _playerView;
        private Transform _playerT;
        private Transform _cameraT;

        private float _camSpeed = 2f;

        private float X;
        private float Y;

        private float offsetX;
        private float offsetY;

        private float _xAxisInput;
        private float _yAxisInput;

        private float _treshold;

        public CameraController(LevelObjectView playerView, Transform cameraT)
        {
            _playerView = playerView;
            _cameraT = cameraT;
            _playerT = playerView._transform;
            _treshold = 0.2f;
        }

        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = _playerView._rb.velocity.y;

            X = _playerT.position.x;
            Y = _playerT.position.y;

            if(_xAxisInput > _treshold)
            {
                offsetX = 4;
            }
            else if(_xAxisInput < -_treshold)
            {
                offsetX = -4;
            }
            else
            {
                offsetX = 0;
            }


            if(_yAxisInput > _treshold)
            {
                offsetY = 4;
            }
            else if (_yAxisInput < -_treshold)
            {
                offsetY = -4;
            }
            else
            {
                offsetY = 0;
            }

            _cameraT.position = Vector3.Lerp(_cameraT.position, new Vector3(X+offsetX, Y+offsetY, _cameraT.position.z), Time.deltaTime*_camSpeed);
        }
    }
}