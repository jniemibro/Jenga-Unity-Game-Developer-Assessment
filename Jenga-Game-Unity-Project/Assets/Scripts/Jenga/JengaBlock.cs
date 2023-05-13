namespace JengaGame
{
    using UnityEngine;
    using UnityEngine.Events;

    public class JengaBlock : MonoBehaviour
    {
        [SerializeField] MasteryType masteryType = MasteryType.Glass;
        JengaBlockData data;
        BoxCollider _boxCollider;
        MeshRenderer _meshRenderer;
        Rigidbody _rigidbody;
        Material _sharedMaterial;

        const float PADDING = 0.1f;
        private static readonly Color SELECTED_COLOR = new Color(1, 0.5f, 0);

        void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _sharedMaterial = _meshRenderer.sharedMaterial;
            _boxCollider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        void OnDestroy()
        {
            
        }

        public void SetData(JengaBlockData newData)
        {
            data = newData;
        }

        public void SetHighlighted(bool isHighlighted)
        {
            if (isHighlighted)
                _meshRenderer.material.color = SELECTED_COLOR;
            else
                _meshRenderer.material = _sharedMaterial;
        }

        public void SetEnabled(bool shouldEnable)
        {
            _boxCollider.enabled = shouldEnable;
            _meshRenderer.enabled = shouldEnable;
            _rigidbody.isKinematic = !shouldEnable;
        }

        public float GetHeight()
        {
            if (!_meshRenderer)
                _meshRenderer = GetComponent<MeshRenderer>();
            return _meshRenderer.bounds.extents.y * 2;
        }

        public float GetWidth()
        {
            if (!_meshRenderer)
                _meshRenderer = GetComponent<MeshRenderer>();
            return (_meshRenderer.bounds.extents.z * 2) + PADDING;
        }

        public JengaBlockData GetData()
        {
            return data;
        }

        public MasteryType GetMasteryType()
        {
            return masteryType;
        }
    }

}