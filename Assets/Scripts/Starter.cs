using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonAimController _aimController;
        [SerializeField] private CannonView _cannonView;

        void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _aimController = new CannonAimController(_cannonView.muzzleTransform, _playerView._transform);
        }

        // Update is called once per frame
        void Update()
        {
            _playerController.Update();
            _aimController.Update();
        }
    }
}