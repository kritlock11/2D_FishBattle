
using UnityEngine;
namespace FishBattle
{
    public abstract class BaseSceneObj : MonoBehaviour
    {
        [HideInInspector] public Rigidbody2D Rigidbody { get; private set; }
        [HideInInspector] public Transform Transform { get; private set; }
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }
        private int _layer;
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                AskLayer(Transform, _layer);
            }
        }
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Transform = transform;
        }
        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }
    }
}
