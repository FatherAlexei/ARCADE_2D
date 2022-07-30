using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Arcade
{
    public class CoinsController : IDisposable
    {
        private float _animation = 10;
        private SpriteAnimatorController _coinAnimator;
        private SpriteAnimatorConfig _coinConfig; 
        private LevelObjectView _playerView;
        private List<LevelObjectView> _coinsView;


        public CoinsController(LevelObjectView playerView, List<LevelObjectView> coins)
        {
            _playerView = playerView;
            _coinsView = coins;
            _coinConfig = Resources.Load<SpriteAnimatorConfig>("CoinAnimCfg");
            _coinAnimator = new SpriteAnimatorController(_coinConfig);

            _playerView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (LevelObjectView coin in _coinsView)
            {
                _coinAnimator.StarAnimation(coin._spriteRenderer, AnimState.Run, true, _animation);
            }
        }

        private void OnLevelObjectContact(LevelObjectView contactView)
        {
            if(_coinsView.Contains(contactView))
            {
                _coinAnimator.StopAnimation(contactView._spriteRenderer);
                GameObject.Destroy(contactView.gameObject);
                _coinsView.Remove(contactView);
            }
        }

        public void Update()
        {
            _coinAnimator.Update();
        }

        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }

}