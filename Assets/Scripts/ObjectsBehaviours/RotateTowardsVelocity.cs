using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class RotateTowardsVelocity : MonoBehaviour
    {
        private void FixedUpdate()
        {
            // Get velocity of object
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

            // Calculate angle to rotate
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            // Set z-rotation of object equal to angle
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}