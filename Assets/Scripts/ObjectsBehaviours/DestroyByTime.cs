using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class DestroyByTime : MonoBehaviour
    {
        public float TimeBeforeDestoy;


        private void Update()
        {
            if ((TimeBeforeDestoy -= Time.deltaTime) <= 0)
                Destroy(gameObject);
        }
    }
}
