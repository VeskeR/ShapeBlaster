using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class ForcePlayer : ObjectBehaviour
    {
        // Range from which force is being applied
        public float Range;
        // Force to apply to player. Positive values pull player towards this gameobject and negative once push player away
        public float Force;


        protected override IEnumerator Behave()
        {
            // Get player's gameobject and rigidbody2D
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Rigidbody2D playerRb2D = player.GetComponent<Rigidbody2D>();

            while (true)
            {
                // Get vector from this object to player
                Vector3 toPlayerVect = player.transform.position - transform.position;
                float sqrDist = toPlayerVect.sqrMagnitude;

                // If squared distance less than squared Range ...
                if (sqrDist <= Range * Range)
                    // ... then apply force to player
                    playerRb2D.AddForce(-toPlayerVect.normalized * Force / (sqrDist + Range / 10));

                yield return null;
            }
        }
    }
}