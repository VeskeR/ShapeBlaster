using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Particles
{
    public class ParticlesAppear : MonoBehaviour
    {
        public List<AudioClip> AudioClips; 


        private void Start()
        {
            // Play particle sound
            if (AudioClips.Any())
                GetComponent<AudioSource>().PlayOneShot(AudioClips[Random.Range(0, AudioClips.Count)]);
        }
    }
}
