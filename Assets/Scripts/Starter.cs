using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private PlayerTransformController _playerController;
        [SerializeField] private LevelObjectView _playerView;

        void Awake()
        {
            _playerController = new PlayerTransformController(_playerView);
        }

        // Update is called once per frame
        void Update()
        {
            _playerController.Update();
        }
    }
}