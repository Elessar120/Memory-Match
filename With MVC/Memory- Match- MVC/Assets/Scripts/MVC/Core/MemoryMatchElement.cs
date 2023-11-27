using UnityEngine;

namespace MVC.Core
{
    // Base class for all elements in this application.
    public class MemoryMatchElement : MonoBehaviour
    {
        // Gives access to the application and all instances.
        public MemoryMatchApplication App => FindObjectOfType<MemoryMatchApplication>();
    }
}