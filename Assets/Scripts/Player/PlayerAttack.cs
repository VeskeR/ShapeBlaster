using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject Shot;
        public Transform ShotSpawn;
        public float FireDelay;

        private float _nextTime = 0f;
        private PlayerShip _player;


        private void Awake()
        {
            _player = GetComponent<PlayerShip>();
        }

        private void Update()
        {
            // If player is alive, user trying to fire and weapon is reloaded then fire
            if (_player.IsAlive && Input.GetButton("Fire1") && Time.time > _nextTime)
            {
                // Add delay before next fire
                _nextTime = Time.time + FireDelay;

                // Create shot
                Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation);
            }
        }
    }
}
