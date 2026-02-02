using Systems.GameField;
using static Systems.Globals.Constants;
using UnityEngine;

namespace Systems.Figures
{
    public enum FigureType
    {
        Pawn,
        Rook,
        Bishop,
        Knight,
        Queen,
        King
    }
    
    public class BaseFigure : MonoBehaviour
    {
        protected Cell Current_cell {get; private set;}
        
        public FigureType Type {get; private set;}

        protected virtual bool ValidateMove(int x, int y)
        {
            if(x < 0 || x >= GRID_SIZE || y < 0 || y >= GRID_SIZE)
                return false;
            return true;
        }

        public Vector2Int GetGridCoordinates()
        {
            return Current_cell.Grid_Coordinates;
        }
        
        public void MoveOnGrid(int x, int y)
        {
            if (ValidateMove(x, y))
            {
                Current_cell = G.Instance.GameField.CellsGrid[x][y];
                Debug.Log($"{Current_cell.Grid_Coordinates}");
                scalePositionToFieldSize();
            }
        }

        private void scalePositionToFieldSize()
        {
            gameObject.transform.position = 
                new Vector3(Current_cell.Grid_Coordinates.x * CELL_SIZE + CELL_SIZE / 2, 
                    0, 
                    Current_cell.Grid_Coordinates.y * CELL_SIZE + CELL_SIZE / 2);
        }
        
        public void Initialize(Cell current_cell, FigureType type)
        {
            //Debug.Log($"{Current_cell.Grid_Coordinates}");
            
            Current_cell = current_cell;
            Type = type;
            scalePositionToFieldSize();
        }
        
    }
}