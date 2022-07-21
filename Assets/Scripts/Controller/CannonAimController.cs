using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arcade
{

    public class CannonAimController : MonoBehaviour
    {

        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _dir;
        private Vector3 _axis;
        private float _angle;


        public CannonAimController(Transform muzzleTransform, Transform targetTransform)
        {
            _muzzleTransform = muzzleTransform;
            _targetTransform = targetTransform;
        }

        // Update is called once per frame
        public void Update()
        {
            _dir = _targetTransform.position - _muzzleTransform.position;
            _angle = Vector3.Angle(Vector3.down, _dir);
            _axis = Vector3.Cross(Vector3.down, _dir);

            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}