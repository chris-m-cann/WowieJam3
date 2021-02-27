using System;
using UnityEngine;

namespace Wowie
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float minDistance;

        private Rigidbody2D _rigidbody;
        private Camera _cam;
        private Vector2 targetDir = Vector2.zero;
        private Vector3 mousePoint = Vector3.zero;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _cam = Camera.main;
        }

        private void Update()
        {
            mousePoint = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.z = transform.position.z;

            targetDir = (mousePoint - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, mousePoint) > minDistance)
            {
                var y = Vector3.SignedAngle(Vector3.up, targetDir, Vector3.forward);
                transform.eulerAngles = Vector3.forward * (y + 90);

                _rigidbody.velocity = transform.right * speed;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }
}