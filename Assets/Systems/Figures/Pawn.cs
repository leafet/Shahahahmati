using Systems.GameField;
using UnityEngine;
using static Systems.Globals.Constants;

namespace Systems.Figures
{
    public class Pawn : BaseFigure
    {
        protected override bool ValidateMove(int x, int y)
        {
            bool allowMove = true;
            
            Cell target_cell = G.Instance.GameField.CellsGrid[x][y];
            BaseFigure possibleTarget = target_cell.Figure; 
            
            if(Mathf.Abs(Current_cell.Grid_Coordinates.x - x) > 0 || Mathf.Abs(Current_cell.Grid_Coordinates.y - y) > 3)
                allowMove = false;

            if (!base.ValidateMove(x, y))
                allowMove = false;

            if (possibleTarget is not null)
            {
                if (possibleTarget.FigureTeam != FigureTeam)
                {
                    if (Mathf.Abs(Current_cell.Grid_Coordinates.x - x) == 1 &&
                        Mathf.Abs(Current_cell.Grid_Coordinates.y - y) == 1)
                    {
                        Destroy(possibleTarget.gameObject);
                        allowMove = true;
                    }
                        
                }
            }

            return allowMove;
        }
    }
}