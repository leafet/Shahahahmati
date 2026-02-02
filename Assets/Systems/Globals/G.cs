using System;
using Systems.Figures;
using Systems.GameField;
using Systems.Input;
using Systems.Interface;
using Systems.Movement;
using UnityEngine;
using static Systems.Globals.Constants;

public class G : MonoBehaviour
{
    public static G Instance {get; private set;}
    
    public Field GameField;
    public OnGameUI OnGameUI;
    public InputService InputService;
    public FigureInteractionService FigureInteractionService;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        initializeGameField();

        moveCameraToFieldCenter();

        initializeDebugFeatures();

        initializeInputService();

        initializeFigureMovementService();
        
        initializeOnGameUI();
    }

    private void initializeFigureMovementService()
    {
        FigureInteractionService figureInteractionService = gameObject.AddComponent<FigureInteractionService>();
        figureInteractionService.Initialize();
        
        FigureInteractionService = figureInteractionService;
    }

    private void initializeInputService()
    {
        InputService inputService = gameObject.AddComponent<InputService>();
        inputService.Initialize();
        
        InputService = inputService;
    }

    private void initializeOnGameUI()
    {
        OnGameUI onGameUI = gameObject.AddComponent<OnGameUI>();
        onGameUI.Initialize();
        
        OnGameUI = onGameUI;
    }

    private void moveCameraToFieldCenter()
    {
        if (Camera.main == null) return;
        
        if (Camera.main.fieldOfView < 15) return;
        
        float camera_y_pos = FIELD_SIZE / (2 * Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad / 2));

        Camera.main.transform.position = new Vector3(FIELD_SIZE / 2, camera_y_pos, FIELD_SIZE / 2);
    }

    private void initializeGameField()
    {
        GameObject gameField = new GameObject("Game Field");
        Field game_field = gameField.AddComponent<Field>();
        game_field.Initialize();
        GameField = game_field;
    }

    private void initializeDebugFeatures()
    {
        GameObject PawnGO = new GameObject("Pawn");
        Pawn pawn_logic = PawnGO.AddComponent<Pawn>();
        
        pawn_logic.Initialize(Instance.GameField.CellsGrid[0][0], FigureType.Pawn);
    }
}
