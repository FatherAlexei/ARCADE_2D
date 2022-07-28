using UnityEngine;

namespace Arcade
{
    public class BulletController
    {
        private LevelObjectView _view;
        private Vector3 _velocity;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);

        }

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
        }

        public void Throw(Vector3 position, Vector3 velocity)
        {
            Active(true);

            _view._transform.position = position;
            _view._rb.velocity = Vector3.zero;
            _view._rb.angularVelocity = 0;

            _view._rb.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}
