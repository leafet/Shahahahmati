using System;
using Systems.Figures;
using Systems.GameField;
using Systems.Interface;
using UnityEngine;
using static Systems.Globals.Constants;

public class G : MonoBehaviour
{
    public static G Instance {get; private set;}
    
    public Field GameField;
    public OnGameUI OnGameUI;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        InitializeGameField();

        MoveCameraToFieldCenter();

        InitializeOnGameUI();
    }

    private void InitializeOnGameUI()
    {
        OnGameUI onGameUIgo = gameObject.AddComponent<OnGameUI>();
        onGameUIgo.Initialize();
        
        OnGameUI = onGameUIgo;
    }

    private void MoveCameraToFieldCenter()
    {
        if (Camera.main == null) return;
        
        if (Camera.main.fieldOfView < 15) return;
        
        float camera_y_pos = FIELD_SIZE / (2 * Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2));

        Camera.main.transform.position = new Vector3(FIELD_SIZE / 2, camera_y_pos, FIELD_SIZE / 2);
    }

    private void InitializeGameField()
    {
        GameObject gameField = new GameObject("Game Field");
        Field game_field = gameField.AddComponent<Field>();
        game_field.Initialize(GRID_SIZE, CELL_SIZE);
        GameField = game_field;
        
        
    }
}
