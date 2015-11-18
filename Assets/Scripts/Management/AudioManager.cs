using UnityEngine;

namespace Assets.Scripts.Management
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource MusicSource;
        public AudioSource SfxSource;

        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get { return _instance; }
        }


        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (this != _instance)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }


        public void PlayOnce(AudioClip clip)
        {
            SfxSource.clip = clip;
            SfxSource.Play();
        }
    }
}