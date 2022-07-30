using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{

    public class BulletEmitterController
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _transform;

        private int _index;
        private float _timeTillNexBull;
        private float _delay = 1;

        private float _startSpeed = 15;

        public BulletEmitterController(List<LevelObjectView> bulletViews, Transform EmitterTransform)
        {
            _transform = EmitterTransform;
            foreach(LevelObjectView BulletView in bulletViews)
            {
                _bulletControllers.Add(new BulletController(BulletView));
            }
        }

        public void Update()
        {
            if(_timeTillNexBull>0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNexBull -= Time.deltaTime;
            }
            else
            {
                _timeTillNexBull = _delay;
                _bulletControllers[_index].Throw(_transform.position, -_transform.up*_startSpeed);
                _index++;
                if (_index >= _bulletControllers.Count)
                    _index = 0;
            }
        }
    }

}
