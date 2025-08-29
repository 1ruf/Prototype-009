using System;
using System.Collections.Generic;
using UnityEngine;
using KoUtility.ObjectPool.Runtime;
using KoUtility.Unity.GameObjects.Pools;

namespace KoUtility.Unity.GameObjects
{
    public enum RemoveActions
    {
        None,
        Fall,
        Decrease
    }
    public struct ObjectBuildData
    {
        public float CreateSpeed;
        public float LifTime;
        public float RemoveTime;

        public RemoveActions RemoveAction;
    }
    public struct ObjectData
    {
        public Transform Transform;
        public Material Material;
        public Vector3 TargetScale;

        public Action Action;
    }

    public class ObjectBuilder
    {
        private ObjectBuildData _buildData;
        private ObjectData _objectData;

        private Transform _targetTransform;
        private Material _targetMaterial;

        private PoolManagerMono _poolManager;
        private PoolingItemSO _poolItem;

        private Stack<CreatedObject> _createdObject = new();

        public ObjectBuilder(PoolManagerMono poolM)
        {
            _poolManager = poolM;
        }

        public void Initialize(ObjectData objData, ObjectBuildData buidData, PoolingItemSO poolItem)
        {
            _objectData = objData;
            _buildData = buidData;
            _poolItem = poolItem;
        }

        public void AddObject()
        {
            _createdObject.Push(ObjectCreate());
        }

        public void RemoveObejct()
        {
            _createdObject.Pop();
        }

        public void Build()
        {
            if (_createdObject.Count == 0) return;
            foreach (var obj in _createdObject)
            {
                obj.Action();
            }
        }

        private CreatedObject ObjectCreate()
        {
            CreatedObject obj = _poolManager.Pop<CreatedObject>(_poolItem);
            obj.SetObject(_objectData);
            obj.Initialize(_objectData,_buildData, _poolItem);

            return obj;
        }
    }

}