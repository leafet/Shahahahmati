using Systems.Figures;
using UnityEditor;
using UnityEngine;

namespace Systems.Interface.CustomEditors
{
    [CustomEditor(typeof(G))]
    public class GlobalScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Move piece"))
            {
                BaseFigure current_figure = G.Instance.GameField.CellsGrid[0][0].Figure;
                
                if (current_figure is not null)
                {
                    int x = current_figure.GetGridCoordinates().x;
                    current_figure.MoveOnGrid(x + 3, 0);
                }
                    
            }
        }
    }
}