using System;
using System.Collections.Generic;
using Systems.Figures;
using Systems.GameField;
using UnityEngine;

namespace Systems.Interface
{
    public class OnGameUI : MonoBehaviour
    {
        private GameObject PawnVisual;
        
        public void Initialize()
        {
            PawnVisual = new GameObject("Pawn Visual");
            PawnVisual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            PawnVisual.transform.localScale = new Vector3(12, 12, 12);
        }

        private void FixedUpdate()
        {
            foreach (List<Cell> Row in G.Instance.GameField.CellsGrid)
            {
                foreach (Cell cell in Row)
                {
                    if (cell.Figure is null) return;
                    
                    switch (cell.Figure.Type)
                    {
                        case FigureType.Pawn:
                            PawnVisual.transform.position = cell.Figure.transform.position;
                            break;
                    }
                }
            }
        }
    }
}