using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Management;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public float Velocity = 1f;
        public float Damage = 1f;
        public List<AudioClip> ShotsSound;
        public List<GameObject> HitParticles;


        private void Start()
        {
            // Set velocity of weapon
            GetComponent<Rigidbody2D>().velocity = transform.right*Velocity;

            // Play weapon sound
            if (ShotsSound.Count > 0)
                GetComponent<AudioSource>().PlayOneShot(ShotsSound[Random.Range(0, ShotsSound.Count)]);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // If collides with enemy ...
            if (other.tag == "Enemy")
            {
                // ... then enemy gets damage ...
                other.GetComponent<Enemy>().GetDamage(Damage);

                // ... instantiate hit particles ...
                if (HitParticles.Any())
                    Instantiate(HitParticles[Random.Range(0, HitParticles.Count)], transform.position,
                        transform.rotation);

                // ... and destroy this object
                Destroy(gameObject);
            }
        }
    }
}