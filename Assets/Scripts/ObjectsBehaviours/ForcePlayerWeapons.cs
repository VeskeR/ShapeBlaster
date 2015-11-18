using System.Collections;
using System.Linq;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class ForcePlayerWeapons : ObjectBehaviour
    {
        // Range from which force is being applied
        public float Range;
        // Force to apply to player's weapons. Positive values pull weapons towards this gameobject and negative once push them away
        public float Force;


        protected override IEnumerator Behave()
        {
            while (true)
            {
                // From all gameobjects with tag 'PlayerWeapon' in current scene select those that are closer than Range
                var weapons = from w in GameObject.FindGameObjectsWithTag("PlayerWeapon")
                              where (w.transform.position - transform.position).sqrMagnitude <= Range * Range
                              select w;

                // For each found weapon ...
                foreach (GameObject w in weapons)
                {
                    // ... get vector to that weapon ...
                    Vector3 toWeaponVect = w.transform.position - transform.position;
                    // ... and apply force to it
                    w.GetComponent<Rigidbody2D>().AddForce(-toWeaponVect.normalized * Force);
                }

                yield return null;
            }
        }
    }
}