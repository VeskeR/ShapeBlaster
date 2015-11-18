using UnityEngine;

namespace Assets.Scripts.Player
{
    public class RotateToMouse : MonoBehaviour
    {
        private Camera cam;


        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            // Get look vector to mouse position
            Vector3 diff = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            // Get angle to rotate object
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            // Set z-rotation of object equal to angle
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}