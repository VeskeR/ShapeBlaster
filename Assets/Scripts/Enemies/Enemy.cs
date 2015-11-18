using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies.EnemiesBehaviours;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float Health;
        public float TimeUntilStart;
        public float CollisionDamage;
        public string Name;
        public int CoinsCost;

        public List<AudioClip> SpawnClips;
        public List<GameObject> ExplosionParticles; 

        protected SpriteRenderer _sprite;
        protected EnemyBehaviour[] _behaviours;
        protected float _startIn;
        protected GameObject _player;


        protected virtual void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();

            _player = GameObject.FindGameObjectWithTag("Player");

            // Get all behaviours that attached to the enemy
            _behaviours = GetComponents<EnemyBehaviour>();
        }

        protected virtual void Start()
        {
            StartCoroutine(Spawn());
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            // If collides with player ...
            if (other.tag == "Player")
            {
                // ... then player takes collision damage ...
                other.GetComponent<PlayerShip>().GetDamage(CollisionDamage);
                
                // ... and enemy takes infinite damage
                GetDamage(float.MaxValue);
            }
        }


        public void GetDamage(float damage)
        {
            // Decrease enemy health
            Health -= damage;

            // If health below 0 ...
            if (Health <= 0)
            {
                // ... destroy enemy
                DestroyEnemy();
            }
        }

        public void DestroyEnemy()
        {
            // Instantiate explosion particles
            if (ExplosionParticles.Count > 0)
                Instantiate(ExplosionParticles[Random.Range(0, ExplosionParticles.Count)], transform.position,
                    transform.rotation);

            Destroy(gameObject);
        }

        private IEnumerator Spawn()
        {
            // Deactivate collider while spawning
            GetComponent<Collider2D>().enabled = false;

            // Play spawn sound
            if (SpawnClips.Any())
                GetComponent<AudioSource>().PlayOneShot(SpawnClips[Random.Range(0, SpawnClips.Count)]);

            // Make enemy invisible
            _sprite.color = new Color(1, 1, 1, 0);

            // Get spawn time
            _startIn = TimeUntilStart;

            // Smoothly make enemy visible
            do
            {
                _startIn -= Time.deltaTime;

                _sprite.color = new Color(1, 1, 1, 1 - _startIn / TimeUntilStart);

                yield return null;
            } while (_startIn > 0);

            // Make sure sprite is visible
            _sprite.color = new Color(1, 1, 1, 1);
            
            // Activate collider
            GetComponent<Collider2D>().enabled = true;

            // Start enemy behaviours
            foreach (EnemyBehaviour b in _behaviours)
            {
                StartCoroutine(b.ApplyBehaviour());
            }
        }
    }
}