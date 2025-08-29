using System.Collections;
using UnityEngine;
using KoUtility.ObjectPool.Runtime;
using KoUtility.Unity.Common;

namespace KoUtility.Unity.GameObjects.Pools
{
    [RequireComponent(typeof(MeshRenderer))]
    public class CreatedObject : MonoBehaviour, IPoolable
    {
        [SerializeField] private PoolingItemSO _poolType;
        [SerializeField] private float destroyYPosition = -50f;

        private ObjectBuildData _buildData;
        private ObjectData _objectData;

        private Material _material;
        private MeshRenderer _meshRenderer;
        private Pool _currentPool;

        private Vector3 _targetScale;
        private Vector3 _currentScale;

        private float _currentBuildSpeed;
        private bool _canRemove;
        public PoolingItemSO PoolingType => _poolType;

        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(ObjectData objData,ObjectBuildData buildData, PoolingItemSO poolType)
        {
            _poolType = poolType;
            _objectData = objData;
            _buildData = buildData;
        }

        public void Action()
        {
            //EndAction도 추가해야함
            StartCoroutine(Flow());
        }

        public void SetObject(ObjectData objData)
        {
            transform.position = objData.Transform.position;
            transform.rotation = objData.Transform.rotation;
            _material = objData.Material;
            _targetScale = objData.TargetScale;

            ResetVisual();
        }

        public void ResetItem()
        {
            ResetVisual();
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        public void SetUpPool(Pool pool)
        {
            _currentPool = pool;
        }

        private IEnumerator Flow()
        {
            SetScale(_targetScale);
            _currentBuildSpeed = _buildData.CreateSpeed;
            yield return new WaitForSeconds(_currentBuildSpeed);

            _objectData.Action?.Invoke();
            yield return new WaitForSeconds(_buildData.LifTime);

            Remove();
        }

        private void Remove()
        {
            switch (_buildData.RemoveAction)
            {
                case RemoveActions.Fall:
                    gameObject.AddComponent(typeof(Rigidbody));
                    break;
                case RemoveActions.Decrease:
                    _currentScale = Vector3.zero;
                    break;
                default:
                    PushObject();
                    break;
            }
        }

        private void SetScale(Vector3 scale)
        {
            _currentScale = scale;
        }

        private void ResetVisual()
        {
            transform.localScale = Vector3.zero;
            _meshRenderer.material = _material;
        }

        private void Update()
        {
            transform.localScale = Interpolater.Lerp(transform.localScale, _currentScale, _currentBuildSpeed);

            if (_canRemove && (transform.localScale.x < 0.05f || transform.position.y < destroyYPosition))
                PushObject();
        }

        private void PushObject()
        {
            _currentPool.Push(this);
        }
    }
}
