using System;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    [Serializable]
    public class SheepStats
    {
        [SerializeField] private float _speedMove;
        [SerializeField] private float _speedRotation;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _drag;
        [SerializeField] private float _velocity;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private Vector3 _position;

        public event Action<float> EventChangeVelocity;
        public event Action<Vector3> EventChangeRotation;
        public event Action<Vector3> EventChangePosition;

        public float SpeedMove => _speedMove;
        public float SpeedRotation => _speedRotation;
        public float Drag => _drag;

        public float Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
                EventChangeVelocity?.Invoke(_velocity);
            }
        }

        public float Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }

        public Vector3 Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                EventChangeRotation?.Invoke(_rotation);
            }
        }

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                EventChangePosition?.Invoke(_position);
            }
        }
    }
}
