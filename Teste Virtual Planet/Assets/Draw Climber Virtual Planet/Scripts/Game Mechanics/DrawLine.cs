using System;
using System.Collections.Generic;
using Draw_Climber_Virtual_Planet.Scripts.UI;
using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    
    /// <summary>
    /// Uses a line renderer to draw the line that will become the players leg
    /// </summary>
    
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLine : MonoBehaviour
    {
        /// <summary>
        /// Used to determine the distance required to create a new vertex
        /// </summary>
        [SerializeField] private float resolution;

        /// <summary>
        /// The camera used to create the mesh
        /// </summary>
        [SerializeField] private Camera lineCamera; 

        public event Action OnStoppedDrawing;

        private LineRenderer _lineRenderer;

        private readonly List<Vector3> _lineVertices = new List<Vector3>();
        public Mesh LegMesh { get; private set; }

        private void Awake()
        {
            LegMesh = new Mesh();
            _lineRenderer = GetComponent<LineRenderer>();
        }
        private void Update()
        {
            if (HandleDrawArea.CanDraw)
            {
                Draw();
            }
            else if (_lineVertices.Count > 0)
            {
                PrepareForBaking();
                
                CreateMesh();
                
                ClearLineRendererAndVertices();
            } 
        }

        /// <summary>
        /// Draws the line based on the player input
        /// </summary>
        private void Draw()
        {
            if (_lineVertices.Count < 1)
            {
                _lineVertices.Add(GetMousePosition());
            }
            else if(GetRelativeDistance(ReturnLastLineVertex(),GetMousePosition()) > resolution)
            {
                _lineVertices.Add(GetMousePosition());
            }
                
            PopulateLineRenderer();
        }

        /// <summary>
        /// Prepares the line renderer to be baked in a mesh
        /// </summary>
        private void PrepareForBaking()
        {
            _lineVertices.Add(_lineVertices[_lineVertices.Count -1] + Vector3.forward * 0.3f);
            
            PopulateLineRenderer();
             
            var first = _lineRenderer.GetPosition(0);
            var difference = Vector3.zero - first;

            for (var i = 0; i < _lineRenderer.positionCount; i++)
            {
                var newPosition = _lineRenderer.GetPosition(i) + difference;
                _lineRenderer.SetPosition(i,newPosition);
            }
        }

        /// <summary>
        /// Creates a mesh through the line renderer
        /// </summary>
        private void CreateMesh()
        {
            _lineRenderer.BakeMesh(LegMesh,lineCamera, true);
            OnStoppedDrawing?.Invoke();
        }

        /// <summary>
        /// Clears the list and the line renderer making them ready for the next drawing
        /// </summary>
        private void ClearLineRendererAndVertices()
        {
            _lineRenderer.positionCount = 0;
            _lineVertices.Clear();
        }

        /// <summary>
        /// Gets the points of the list to populate the line renderer
        /// </summary>
        private void PopulateLineRenderer()
        {
            _lineRenderer.positionCount = _lineVertices.Count;
            
            _lineRenderer.SetPositions(_lineVertices.ToArray());
        }

        /// <summary>
        /// Returns the value of the last member of the list
        /// </summary>
        /// <returns></returns>
        private Vector3 ReturnLastLineVertex()
        {
            return _lineVertices[_lineVertices.Count - 1];
        }

        /// <summary>
        /// Gets the mouse position
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMousePosition()
        {
            var pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,1);
            return lineCamera.ScreenToWorldPoint(pos);
        }

        /// <summary>
        /// Gets the distance between two Vector3 divided by 100
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        private float GetRelativeDistance(Vector3 first, Vector3 last)
        {
            return Vector3.Distance(first, last) / 100;
        }
    }
}
