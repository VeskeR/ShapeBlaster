using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class ForceParticles : ObjectBehaviour
    {
        // Range from which force is being applied
        public float Range;
        // Range from which apply tangential force to particles
        public float ParticlesCircleRange;
        // Force to apply to particles. Positive values pull particles towards this gameobject and negative once push them away
        public float Force;


        protected override IEnumerator Behave()
        {
            while (true)
            {
                // For each found particle system in current scene ...
                foreach (ParticleSystem pS in FindObjectsOfType<ParticleSystem>())
                {
                    // ... get attached particles and their count
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pS.particleCount];
                    int ptCount = pS.GetParticles(particles);

                    // For each particle in current particle system ...
                    for (int i = 0; i < ptCount; i++)
                    {
                        // ... get vector to that particle ...
                        Vector3 toParticleVect = particles[i].position - transform.position;
                        // ... distance ...
                        float dist = toParticleVect.magnitude;
                        // ... and normalized vector
                        Vector3 n = toParticleVect.normalized;

                        particles[i].velocity = -n * Force / (dist * dist + Range / 10);

                        // If particle is in range for applying tangential acceleration to it ...
                        if (dist <= ParticlesCircleRange)
                        {
                            // ... then set velocity of that particle so it moves around this gameobject
                            particles[i].velocity = Force * new Vector3(n.y, -n.x, 0) / (dist + ParticlesCircleRange);
                        }
                    }

                    // Set new particles to current particle system
                    pS.SetParticles(particles, ptCount);
                }

                yield return null;
            }
        }
    }
}