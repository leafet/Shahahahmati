using UnityEngine;
using static Systems.Globals.Constants;

namespace Systems.Figures
{
    public class Pawn : BaseFigure
    {
        protected override bool ValidateMove(int x, int y)
        {
            if (!base.ValidateMove(x, y)) return false;
            return Mathf.Abs(Current_cell.Grid_Coordinates.x - x) <= 5;
        }
    }
}