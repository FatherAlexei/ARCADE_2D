using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class Starter : MonoBehaviour
    {
        private PlayerController _playerController;
        private CannonAimController _aimController;
        private BulletEmitterController _bulletEmitterController;
        private CameraController _cameraController;
        private CoinsController _coinsController;


        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coinsView;

        void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _aimController = new CannonAimController(_cannonView.muzzleTransform, _playerView._transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView.bullets, _cannonView.emitter);
            _cameraController = new CameraController(_playerView, Camera.main.transform);
            _coinsController = new CoinsController(_playerView, _coinsView);
        }

        // Update is called once per frame
        void Update()
        {
            _playerController.Update();
            _aimController.Update();
            _bulletEmitterController.Update();
            _cameraController.Update();
            _coinsController.Update();
        }
    }
}