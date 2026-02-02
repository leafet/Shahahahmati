using JetBrains.Annotations;
using Systems.Figures;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.GameField
{
    public class Cell : MonoBehaviour
    {
        [CanBeNull] public BaseFigure Figure;
        
        public Vector2Int Grid_Coordinates;
        private SpriteRenderer _cell_renderer;
        
        public void Initialize(Vector2Int cell_coordinates,Vector3 cell_position, Sprite cell_sprite, BaseFigure figure)
        {
            _cell_renderer = gameObject.AddComponent<SpriteRenderer>();
            _cell_renderer.sprite = cell_sprite;
            
            Grid_Coordinates = cell_coordinates;
            
            Figure = figure;
            
            transform.position = cell_position;
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}