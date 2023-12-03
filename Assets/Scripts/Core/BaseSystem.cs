using UnityEngine;

namespace Colorado.Core
{
    public abstract class BaseSystem : MonoBehaviour, IInitialize
    {
        public virtual void StartGame() { }

        public virtual void UpdateGame() { }
    }
}