using System;
using System.Collections.Generic;
using Draw_Climber_Virtual_Planet.Scripts.Player_Inputs;
using Draw_Climber_Virtual_Planet.Scripts.UI;
using UnityEngine;

namespace Draw_Climber_Virtual_Planet.Scripts.Game_Mechanics
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawLine : MonoBehaviour
    {

        [SerializeField] private float resolution;

        [SerializeField] private Camera lineCamera;

        public event Action OnStoppedDrawing;

        private LineRenderer _lineRenderer;
        
        private PlayerInput _playerInput;

        private List<Vector3> _lineVertices = new List<Vector3>();
        public Mesh LegMesh { get; private set; }

        private void Awake()
        {
            LegMesh = new Mesh();
            _lineRenderer = GetComponent<LineRenderer>();
            _playerInput = new PlayerInput();
        }
        private void Update()
        {
            if (HandleDrawArea.CanDraw)
            {
                if (_lineVertices.Count < 1)
                {
                    _lineVertices.Add(GetMousePosition());
                }
                else if(GetDistance(ReturnLast(),GetMousePosition()) > resolution)
                {
                    _lineVertices.Add(GetMousePosition());
                }
                
                PopulateLineRenderer();
            }
            else if (_lineVertices.Count > 0)
            {
                PrepareForBaking();
                _lineRenderer.BakeMesh(LegMesh, lineCamera, true);
                OnStoppedDrawing?.Invoke();
                _lineRenderer.positionCount = 0;
                _lineVertices.Clear();
            } 
        }

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

        private void PopulateLineRenderer()
        {
            _lineRenderer.positionCount = _lineVertices.Count;
            
            _lineRenderer.SetPositions(_lineVertices.ToArray());
        }

        private Vector3 ReturnLast()
        {
            return _lineVertices[_lineVertices.Count - 1];
        }

        private Vector3 GetMousePosition()
        {
            var pos = new Vector3(PlayerInput.MousePosition.x,PlayerInput.MousePosition.y,1);
            return lineCamera.ScreenToWorldPoint(pos);
        }

        private float GetDistance(Vector3 lastVertex, Vector3 mousePosition)
        {
            return Vector3.Distance(lastVertex, mousePosition) / 100;
        }
    }
}
