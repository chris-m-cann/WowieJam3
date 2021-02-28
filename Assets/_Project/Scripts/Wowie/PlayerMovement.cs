using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerTail))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float minDistanceToMouse;
        [SerializeField] private float minDistanceBetweenTailBlocks;

        private Rigidbody2D _rigidbody;
        private PlayerTail _tail;
        private Camera _cam;


        private Vector2 targetDir = Vector2.zero;
        private Vector3 mousePoint = Vector3.zero;
        private bool _acceptingInput = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _tail = GetComponent<PlayerTail>();
            _cam = Camera.main;
        }

        private void Update()
        {
            if (!_acceptingInput) return;

            mousePoint = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.z = transform.position.z;

            targetDir = (mousePoint - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, mousePoint) > minDistanceToMouse)
            {
                var y = Vector3.SignedAngle(Vector3.up, targetDir, Vector3.forward);
                transform.eulerAngles = Vector3.forward * (y + 90);

                _rigidbody.velocity = transform.right * speed;

                Transform connection = transform;

                foreach (TailBlock block in _tail.Tail)
                {
                    block.Move(connection, _tail.Spacing, minDistanceBetweenTailBlocks);
                    connection = block.transform;
                }
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }

        public void StopAcceptingInput()
        {
            _acceptingInput = false;
        }
    }
}