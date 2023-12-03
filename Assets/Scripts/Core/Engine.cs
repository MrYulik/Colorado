using System.Linq;
using UnityEngine;

namespace Colorado.Core
{
    public class Engine : MonoBehaviour
    {
        private BaseSystem[] _systems;

        private void Start()
        {
            Application.targetFrameRate = 60;  

            _systems = FindObjectsByType<BaseSystem>(FindObjectsSortMode.None);
            _systems.ToList().ForEach(x => x.StartGame());
        }

        private void Update()
        {
            if (_systems == null)
                return;

            _systems.ToList().ForEach(x => x.UpdateGame());
        }
    }
}