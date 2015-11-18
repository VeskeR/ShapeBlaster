using UnityEngine;

namespace Assets.Scripts.Other
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}