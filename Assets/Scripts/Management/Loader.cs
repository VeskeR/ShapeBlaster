using UnityEngine;

namespace Assets.Scripts.Management
{
    public class Loader : MonoBehaviour
    {
        public GameManager GameManager;
        public AudioManager AudioManager;


        private void Awake()
        {
            //if (GameManager.Instance == null)
            //    Instantiate(GameManager);
            if (AudioManager.Instance == null)
                Instantiate(AudioManager);
        }
    }
}