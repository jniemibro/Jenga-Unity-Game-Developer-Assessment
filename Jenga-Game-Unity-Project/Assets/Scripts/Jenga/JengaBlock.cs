namespace JengaGame
{
    using UnityEngine;

    public class JengaBlock : MonoBehaviour
    {
        [SerializeField] MasteryType masteryType = MasteryType.Glass;
        JengaBlockData data;
        BoxCollider _boxCollider;
        MeshRenderer _meshRenderer;
        Rigidbody _rigidbody;

        void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _boxCollider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetData(JengaBlockData newData)
        {
            data = newData;
        }

        void OnMouseOver()
        {
            if (Input.GetMouseButton(1))
            {
                Debug.Log(data.id);
                // TODO: ui display showing info about this block
            }
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
            return _meshRenderer.bounds.extents.x * 2;
        }

        public MasteryType GetMasteryType()
        {
            return masteryType;
        }
    }

}