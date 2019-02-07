
using UnityEngine;

namespace vicos.unity
{
    public class Vicos : MonoBehaviour, IVicos
    {
        private static Vicos Instance;

        [SerializeField] GameObject Root;
        
        void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
            Manager.Create( this );
        }

        private void Update()
        {
            Manager.RenderChanges();
        }

        private void OnDestroy()
        {
            Manager.Destroy();
        }
    }
}