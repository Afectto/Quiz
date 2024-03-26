using System;
using Grid;
using UnityEngine;

public class SelectedGridSlot : MonoBehaviour
{
    [SerializeField] private CreateGrid Grid;
    private Grid<SlotGridObject> _slotItemGrid;

    private bool IsOverlayActive;
    public static Action<string> OnClickSlotItem;
    
    private void Start()
    {
        Instantiate();
        ChangeLevel.onLevelChange += Instantiate;
        CreateGrid.onCompleteGame += OnChangeOverlayActive;
        RestartGame.OnStartNewGame += OnChangeOverlayActive;
    }

    public void Instantiate()
    {
        if (Grid)
        {
            _slotItemGrid = Grid.SlotItemGrid;
        }
    }

    private void OnChangeOverlayActive()
    {
        IsOverlayActive = !IsOverlayActive;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _slotItemGrid != null && !IsOverlayActive)
        {
            var gridObject = _slotItemGrid.GetGridObject(GetMouseWorldPosition(Input.mousePosition));
            if (gridObject != null)
            {
                OnClickSlotItem?.Invoke(gridObject.GetSlotIdentifier());
            }
        }
    }
    
    private Vector3 GetMouseWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    private void OnDestroy()
    {
        ChangeLevel.onLevelChange -= Instantiate;
        CreateGrid.onCompleteGame -= OnChangeOverlayActive;
        RestartGame.OnStartNewGame -= OnChangeOverlayActive;
    }
}
