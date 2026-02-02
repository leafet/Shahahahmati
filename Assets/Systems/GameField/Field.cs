using System.Collections.Generic;
using System.Linq;
using Systems.Figures;
using UnityEngine;

namespace Systems.GameField
{
    public class Field : MonoBehaviour
    {
        public readonly List<List<Cell>> CellsGrid = new();

        private int _grid_size;

        private int _cell_size;
        
        public void Initialize(int grid_size, int cell_size)
        {
            _grid_size = grid_size;
            _cell_size = cell_size;
            
            GenerateCells();
            
            GameObject PawnGO = new GameObject("Pawn");
            Pawn pawn_logic = PawnGO.AddComponent<Pawn>();
        
            pawn_logic.Initialize(CellsGrid[0][0], FigureType.Pawn);
        
            CellsGrid[0][0].Figure = pawn_logic;
        }

        private void GenerateCells()
        {
            for (int i = 0; i < _grid_size; i++)
            {
                List<Cell> cells_row = new List<Cell>();
                for (int j = 0; j < _grid_size; j++)
                {
                    
                    Texture2D current_cell_tex = new Texture2D(_cell_size, _cell_size);
                    
                    if ((i + j) % 2 == 0)
                    {
                        Color[] current_cell_colors = new Color[_cell_size * _cell_size];

                        for (int k = 0; k < current_cell_colors.Length; k++)
                        {
                            current_cell_colors[k] = Color.white;
                        }
                        
                        current_cell_tex.SetPixels(current_cell_colors);
                        current_cell_tex.Apply();
                    }
                    else
                    {
                        Color[] current_cell_colors = new Color[_cell_size * _cell_size];

                        for (int k = 0; k < current_cell_colors.Length; k++)
                        {
                            current_cell_colors[k] = Color.black;
                        }
                        
                        current_cell_tex.SetPixels(current_cell_colors);
                        current_cell_tex.Apply();
                    }
                    
                    Vector3 cell_pos = new Vector3(i * _cell_size, 0, j * _cell_size);
                    
                    Sprite cell_sprite = 
                        Sprite.Create(current_cell_tex, 
                            new Rect(0, 0, _cell_size, _cell_size), 
                            new Vector2(0, 0), 1.0f);
                    
                    GameObject cell_go = new GameObject($"Cell {i} {j}");
                    
                    Cell cell_logic = cell_go.AddComponent<Cell>();
                    
                    cell_logic.Initialize(new Vector2Int(i, j) ,cell_pos, cell_sprite, null);
                    
                    cells_row.Add(cell_logic);
                }
                CellsGrid.Add(cells_row);
            }
        }
    }
}