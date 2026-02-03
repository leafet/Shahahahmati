using System.Collections;
using Systems.GameField;
using static Systems.Globals.Constants;
using UnityEngine;
using UnityEngine.Serialization;

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

    public enum FigureTeam
    {
        Team1,
        Team2
    }
    
    public class BaseFigure : MonoBehaviour
    {
        protected Cell Current_cell {get; private set;}
        public FigureType Type {get; private set;}

        public FigureTeam FigureTeam;
        
        public void Initialize(Cell current_cell, FigureType type, FigureTeam team)
        {
            Current_cell = current_cell;
            Type = type;
            scalePositionToFieldSize();

            FigureTeam = team;
            
            G.Instance.GameField.CellsGrid[current_cell.Grid_Coordinates.x][current_cell.Grid_Coordinates.y].Figure =
                this;
        }
        
        protected virtual bool ValidateMove(int x, int y)
        {
            Cell target_cell = G.Instance.GameField.CellsGrid[x][y];
            
            if(x < 0 || x >= GRID_SIZE || y < 0 || y >= GRID_SIZE)
                return false;

            BaseFigure possibleTarget = target_cell.Figure; 
            
            if(possibleTarget is not null)
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
                G.Instance.GameField.CellsGrid[Current_cell.Grid_Coordinates.x][Current_cell.Grid_Coordinates.y].Figure 
                    = null;
                
                Current_cell = G.Instance.GameField.CellsGrid[x][y];
                G.Instance.GameField.CellsGrid[x][y].Figure = this;
                
                scalePositionToFieldSize();
            }
        }

        private void scalePositionToFieldSize()
        {
            
            Vector3 newPos = 
                new Vector3(Current_cell.Grid_Coordinates.x * CELL_SIZE + CELL_SIZE / 2, 
                    0, 
                    Current_cell.Grid_Coordinates.y * CELL_SIZE + CELL_SIZE / 2);
            
            StartCoroutine(MoveOverTime(newPos, 0.5f));
        }
        
        IEnumerator MoveOverTime(Vector3 targetPosition, float duration)
        {
            Vector3 startPos = transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float linearT = elapsedTime / duration;
                
                float easedT = Mathf.SmoothStep(0f, 1f, linearT);
                
                transform.position = Vector3.Lerp(startPos, targetPosition, easedT);
                
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }
            
            transform.position = targetPosition;
        }
        
        
    }
}