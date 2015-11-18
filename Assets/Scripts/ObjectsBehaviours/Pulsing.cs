using System.Collections;
using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class Pulsing : ObjectBehaviour
    {
        public float MaxAddedScale;
        public float PulsingRate;


        protected override IEnumerator Behave()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            float time = 0f;

            while (true)
            {
                // Calculate current scale
                float scale = 1 + MaxAddedScale * Mathf.Sin(time * PulsingRate);
                // Set new scale to object
                transform.localScale = new Vector3(scale, scale, scale);

                time += Time.deltaTime;

                yield return null;
            }
        }
    }
}