using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Management;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerShip : MonoBehaviour
    {
        public int Lives;
        public float TimeToRespawn;
        public float SpawnTime;

        public Text LivesText;

        public List<GameObject> ExplosionParticles; 

        private SpriteRenderer _sprite;
        private bool _isLive;
        private float _timeToSpawn;

        public bool IsAlive
        {
            get { return _isLive; }
        }


        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            // Show player's lives
            UpdateTexts();
            StartCoroutine(Spawn());
        }

        private void Update()
        {
            // If player is dead and player still have lives and it's time to spawn ...
            if (!_isLive && Lives > 0 && (_timeToSpawn -= Time.deltaTime) <= 0)
            {
                // ... then spawn
                StartCoroutine(Spawn());
            }
        }


        public void GetDamage(float damage)
        {
            // Decrease player's lives
            Lives--;

            UpdateTexts();

            // Explode player's ship
            Die();

            // If there is no more lives then game over
            if (Lives <= 0)
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }

        private void UpdateTexts()
        {
            LivesText.text = "Lives left: " + Lives;
        }

        private void Die()
        {
            // Stop player's moving
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            // Make player dead so it can't moving
            _isLive = false;
            // Set time before spawn
            _timeToSpawn = TimeToRespawn;
            // Make sprite invisible
            _sprite.color = new Color(1, 1, 1, 0);

            // Destroy every enemy on the screen
            foreach (Enemy e in FindObjectsOfType<Enemy>())
            {
                e.DestroyEnemy();
            }

            // Instantiate explosion particles
            if (ExplosionParticles.Any())
                Instantiate(ExplosionParticles[Random.Range(0, ExplosionParticles.Count)], transform.position,
                    transform.rotation);
        }

        private IEnumerator Spawn()
        {
            // Get spawn time
            float spawnIn = SpawnTime;

            // Set ship position to center of a screen
            transform.position = new Vector3(0, 0, 0);

            // Smoothly make player visible
            do
            {
                spawnIn -= Time.deltaTime;

                _sprite.color = new Color(1, 1, 1, 1 - spawnIn/SpawnTime);

                yield return null;
            } while (spawnIn > 0);

            // Make sure sprite is visible
            _sprite.color = new Color(1, 1, 1, 1);

            // Make player live
            _isLive = true;
        }
    }
}
