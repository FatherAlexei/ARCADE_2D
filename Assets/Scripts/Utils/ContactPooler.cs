using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public class ContactPooler
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private int _contactCount;
        private Collider2D _collider;
        private float _treshold = 0.2f;

        public bool IsGrounded { get; private set; }
        public bool LeftContact { get; private set; }
        public bool RightContact { get; private set; }

        public ContactPooler(Collider2D collider)
        {
            _collider = collider;
        }

        // Update is called once per frame
        public void Update()
        {
            IsGrounded = false;
            LeftContact = false;
            RightContact = false;

            _contactCount = _collider.GetContacts(_contacts);

            for (int i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > _treshold) IsGrounded = true;

                if (_contacts[i].normal.x > _treshold) LeftContact = true;

                if (_contacts[i].normal.x < -_treshold) RightContact = true;
            }

        }
    }
}