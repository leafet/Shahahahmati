using System;
using Systems.Figures;
using Systems.Input;
using Unity.VisualScripting;
using UnityEngine;
using static Systems.Globals.Constants;

namespace Systems.Movement
{
    public class FigureInteractionService : MonoBehaviour
    {
        private InputService _inputService;
        
        private BaseFigure _selectedFigure;
        
        private Vector2 _mouse_position;
        
        public void Initialize()
        {
            _inputService = G.Instance.InputService;
            
            _inputService.OnMouseMove += (sender, vector2) => {_mouse_position = vector2;}; 
            _inputService.OnLeftClick += OnLeftClick;
            _inputService.OnLeftRelease += OnLeftRelease;
        }

        private void OnLeftRelease(object sender, EventArgs e)
        {
            if (_selectedFigure == null) return;

            Vector3? endPos = GetEndPosition(_mouse_position);
            
            endPos ??= Vector3.zero;
            
            int casted_x_pos = Mathf.FloorToInt(endPos.Value.x / CELL_SIZE);
            int casted_y_pos = Mathf.FloorToInt(endPos.Value.z / CELL_SIZE);
            
            _selectedFigure.MoveOnGrid(casted_x_pos, casted_y_pos);
        }

        private void OnLeftClick(object sender, EventArgs e)
        {
            _selectedFigure = GetFigureAtMousePos(_mouse_position);
        }

        private Vector3? GetEndPosition(Vector2 mousePos)
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            
            return null;
        }
        
        private BaseFigure GetFigureAtMousePos(Vector2 mousePos)
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                
                if(hitObject == null) return null;

                if (hitObject.GetComponentInParent<BaseFigure>() != null)
                {
                    return hitObject.GetComponentInParent<BaseFigure>();
                }
                
            }
            
            return null;
        }
    }
}