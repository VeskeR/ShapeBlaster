using UnityEngine;

namespace Assets.Scripts.ObjectsBehaviours
{
    public class Rotating : MonoBehaviour
    {
        public float RotatingSpeed;


        private void Update()
        {
            // Rotate object around z-axis
            transform.Rotate(Vector3.forward*RotatingSpeed);
        }
    }
}