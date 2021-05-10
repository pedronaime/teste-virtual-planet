using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    public class CreateLegMesh : MonoBehaviour
    {
        [SerializeField] private Material legMaterial;
        [SerializeField] private PhysicMaterial legPhysicMaterial;
        
        private MeshFilter _meshFilter;
        
        private MeshCollider _meshCollider;

        private DrawLine _drawLine;

        private void Awake()
        {
            _meshCollider = GetComponent<MeshCollider>();
            _drawLine = FindObjectOfType<DrawLine>();
            _drawLine.OnStoppedDrawing += CreateMesh;
        }

        private void CreateMesh()
        {
            if (!_meshFilter)
            {
                _meshFilter = gameObject.AddComponent<MeshFilter>();
                var meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshRenderer.material = legMaterial;
                _meshCollider.convex = true;
                _meshCollider.material = legPhysicMaterial;
            }
            
            _meshFilter.sharedMesh = _drawLine.LegMesh;
            _meshCollider.sharedMesh = _drawLine.LegMesh;
        }
    }
}
