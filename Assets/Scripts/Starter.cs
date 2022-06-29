using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int animSpeed = 15;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private SpriteAnimatorController _playerAnimator;
        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StarAnimation(_playerView._spriteRenderer, AnimState.Run, true, animSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            _playerAnimator.Update();
        }
    }
}