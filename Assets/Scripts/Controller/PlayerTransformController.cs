using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class PlayerController
    {
        private float _speed = 150f;
        private float _jumpForce = 8f;
        private float _jumpTreshold = 1f;
        private float _moveTreshold = 0.1f;
        private float _g = -9.8f;
        private float _yVelocity;
        private float _xVelocity;
        private float _xAxisInput;
        private bool _jump;
        private bool _move;

        private float groundLevel = 0.1f;

        private Vector3 _leftScale = new Vector3(-1,1,1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private Transform _transform;
        private Rigidbody2D _rb;

        private int animSpeed = 20;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorConfig  _playerConfig;
        private ContactPooler _contactPooler;

        public PlayerController(LevelObjectView view)
        {
            _playerView = view;

            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StarAnimation(_playerView._spriteRenderer, AnimState.Idle, true, animSpeed);
            _contactPooler = new ContactPooler(_playerView._collider);

            _transform = _playerView._transform;
            _rb = _playerView._rb;
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);

            _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            _transform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public void Update()
        {
            _contactPooler.Update();
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _jump = Input.GetAxis("Vertical") > 0;
            _yVelocity = _rb.velocity.y;

            _move = Mathf.Abs(_xAxisInput) > _moveTreshold;

            if (_move)
                MoveTowards();

            if (_contactPooler.IsGrounded)
            {
                _playerAnimator.StarAnimation(_playerView._spriteRenderer, _move ? AnimState.Run : AnimState.Idle, true, animSpeed);
                if (_jump && _yVelocity <= _jumpTreshold)
                {
                    _rb.AddForce(Vector2.up*_jumpForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                if(Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _playerAnimator.StarAnimation(_playerView._spriteRenderer, AnimState.Jump, true, animSpeed);
                }
            }
        }
    }
}
