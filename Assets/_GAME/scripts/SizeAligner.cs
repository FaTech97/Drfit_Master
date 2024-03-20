using System;
using UnityEngine;

namespace _GAME.scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class SizeAligner: MonoBehaviour
    {
        private BoxCollider _collider;

        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
        }

        public void AlignColliderSizeToObjectSize(GameObject newModel)
        {
            MeshFilter mf = newModel.GetComponent<MeshFilter>();
            if (mf == null)
            {
                mf = newModel.GetComponentInChildren<MeshFilter>();
            }

            if (_collider == null)
            {
                _collider = GetComponent<BoxCollider>();
            }

            Bounds bounds = mf.sharedMesh.bounds;
            _collider.center = bounds.center;
            _collider.size = bounds.size;
        }

        public void AlignObjectSizeToColliderSize(GameObject newModel)
        {
            if (_collider == null)
            {
                _collider = GetComponent<BoxCollider>();
            }
            Transform transform = newModel.transform;
            transform.localPosition = _collider.center;
            float sizeComparedScale = _collider.size.magnitude / transform.localScale.magnitude;
            transform.localScale *= sizeComparedScale;
        }
    }
}