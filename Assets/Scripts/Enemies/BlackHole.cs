using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    [Obsolete("Use instead ObjectBehaviours derived classes")]
    public class BlackHole : Enemy
    {
        public float PlayerForce;
        public float PlayerWeaponsForce;
        public float ParticlesForce;
        public float GravityRange;
        public float ParticlesCircleRange;


        protected void FixedUpdate()
        {
            //if (IsActive)
            //{
            //    ApplyForce();
            //}
        }

        protected void ApplyForce()
        {
            PullPlayer();
            PushPlayerWeapons();

            //PullParticles not working correctly
            //PullParticles();
        }

        protected void PullPlayer()
        {
            //float sqrDist = ToPlayerVector.sqrMagnitude;

            //if (sqrDist <= GravityRange * GravityRange)
            //    _player.GetComponent<Rigidbody>().AddForce(-ToPlayerVector.normalized * PlayerForce / (sqrDist + 0.5f));
        }

        protected void PushPlayerWeapons()
        {
            var weapons = from w in FindObjectsOfType<PlayerWeapon>().Select(w => w.gameObject)
                where (w.transform.position - transform.position).sqrMagnitude <= GravityRange*GravityRange
                select w;

            foreach (GameObject w in weapons)
            {
                Vector3 vect = w.transform.position - transform.position;

                w.GetComponent<Rigidbody>().AddForce(vect.normalized * PlayerWeaponsForce);
            }
        }

        protected void PullParticles()
        {
            foreach (ParticleSystem pS in FindObjectsOfType<ParticleSystem>())
            {
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pS.particleCount];
                int ptCount = pS.GetParticles(particles);

                for (int i = 0; i < ptCount; i++)
                {
                    Vector3 vect = transform.position - particles[i].position;
                    float dist = vect.magnitude;
                    Vector3 n = vect.normalized;

                    particles[i].velocity = ParticlesForce * n / (dist * dist + 4);

                    if (dist <= ParticlesCircleRange)
                    {
                        particles[i].velocity = ParticlesForce * new Vector3(n.z, 0, -n.x) / (dist + ParticlesCircleRange);
                    }
                }

                pS.SetParticles(particles, ptCount);
            }
        }
    }
}