using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    /// <summary>
    /// Creates a mesh for the leg that was drawn with the line renderer
    /// </summary>

    public class CreateLegMesh : MonoBehaviour
    {
        /// <summary>
        /// The material to be used to render the mesh
        /// </summary>
        [SerializeField] private Material legMaterial;
        
        private MeshFilter _meshFilter;
        
        private MeshCollider _meshCollider;

        private DrawLine _drawLine;

        private void Awake()
        {
            _meshCollider = GetComponent<MeshCollider>();
            _drawLine = FindObjectOfType<DrawLine>();
            _drawLine.OnStoppedDrawing += CreateMesh;
        }

        /// <summary>
        /// Creates the mesh when the player finishes drawing
        /// Gets Called by the DrawLine class event
        /// </summary>
        private void CreateMesh()
        {
            if (!_meshFilter)
            {
                _meshFilter = gameObject.AddComponent<MeshFilter>();
                var meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshRenderer.material = legMaterial;
                _meshCollider.convex = true;
            }
            
            _meshFilter.sharedMesh = _drawLine.LegMesh;
            _meshCollider.sharedMesh = _drawLine.LegMesh;
        }
    }
}
