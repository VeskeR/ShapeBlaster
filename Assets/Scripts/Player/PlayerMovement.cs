using Assets.Scripts.Management;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed = 8;

        private float _radius;
        private PlayerShip _player;


        private void Awake()
        {
            // Get object size
            _radius = GetComponent<CircleCollider2D>().radius;
            _player = GetComponent<PlayerShip>();
        }

        private void FixedUpdate()
        {
            if (_player.IsAlive)
                MovePlayer();
        }


        private void MovePlayer()
        {
            // Get movement direction according to user input
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Set object velocity
            GetComponent<Rigidbody2D>().velocity = dir*Speed;

            // Clamp object position to game boundary
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, GameManager.XMin + _radius*2, GameManager.XMax - _radius*2),
                Mathf.Clamp(transform.position.y, GameManager.YMin + _radius*2, GameManager.YMax - _radius*2),
                .0f
            );
        }
    }
}
