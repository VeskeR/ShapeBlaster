using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public abstract class ObjectBehaviour : MonoBehaviour
    {
        public float Delay;


        protected virtual void Start()
        {
            StartCoroutine(WaitDelay());
        }


        private IEnumerator WaitDelay()
        {
            // Wait delay before start behaviour
            yield return new WaitForSeconds(Delay);
            // Start behaviour
            StartCoroutine(Behave());
        }

        protected abstract IEnumerator Behave();
    }
}