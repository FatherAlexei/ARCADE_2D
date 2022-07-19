using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class PlayerTransformController
    {
        private float _speed = 4f;
        private float _groundLevel = 0.1f;
        private float _jumpForce = 8f;
        private float _jumpTreshold = 1f;
        private float _moveTreshold = 0.1f;
        private float _g = -9.8f;
        private float _yVelocity;
        private float _xAxisInput;
        private bool _jump;
        private bool _move;

        private float groundLevel = 0.1f;

        private Vector3 _leftScale = new Vector3(-1,1,1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private Transform _transform;

        private int animSpeed = 15;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorConfig  _playerConfig;

        public PlayerTransformController(LevelObjectView view)
        {
            _playerView = view;

            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StarAnimation(_playerView._spriteRenderer, AnimState.Idle, true, animSpeed);

            _transform = _playerView._transform;
        }

        private void MoveTowards()
        {
            _transform.position += Vector3.right * (Time.deltaTime * _speed * (_xAxisInput < 0 ? -1 : 1));
            _transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public bool IsGround()
        {
            return _transform.position.y <= groundLevel&&_yVelocity<= 0;
        }

        public void Update()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _jump = Input.GetAxis("Vertical") > 0;

            _move = Mathf.Abs(_xAxisInput) > _moveTreshold;

            if (_move)
                MoveTowards();

            if (IsGround())
            {
                _playerAnimator.StarAnimation(_playerView._spriteRenderer, _move ? AnimState.Run : AnimState.Idle, true, animSpeed);
                if (_jump && _yVelocity == 0)
                {
                    _yVelocity = _jumpForce;
                }
                else if(_yVelocity < 0)
                {

                    _yVelocity = 0;
                    _transform.position = new Vector3(_transform.position.x, _groundLevel, _transform.position.z);
                }
            }
            else
            {
                if(Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _playerAnimator.StarAnimation(_playerView._spriteRenderer, AnimState.Jump, true, animSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
        }
    }
}
