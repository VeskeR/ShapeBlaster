using System.Collections;
using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class Blinking : ObjectBehaviour
    {
        public float BlinkingSpeed;
        public float TimeBeingVisible;


        protected override IEnumerator Behave()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            float time = 0f;

            // Start blinking
            while (true)
            {
                // Change object's visibleness
                sprite.color = new Color(1, 1, 1, Mathf.Cos(time * BlinkingSpeed) + TimeBeingVisible);

                time += Time.deltaTime;

                yield return null;
            }
        }
    }
}