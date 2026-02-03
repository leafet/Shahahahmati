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
            
        }

        private void FixedUpdate()
        {
            foreach (List<Cell> Row in G.Instance.GameField.CellsGrid)
            {
                foreach (Cell cell in Row)
                {
                    Debug.Log(cell.Figure?.Type);
                }
            }
        }
    }
}