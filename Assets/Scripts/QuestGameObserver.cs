using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestGameObserver : MonoBehaviour
{
    [SerializeField] private Text TextQuest;
    private string _textQuest;
    
    [SerializeField] private CreateGrid Grid;
    private Grid<SlotGridObject> _slotItemGrid;

    private List<string> usedTextQuest = new List<string>();
    
    public static Action<string, bool> OnCheckAnswer;

    private void Awake()
    {
        ChangeLevel.onLevelChange += Instantiate;
        SelectedGridSlot.OnClickSlotItem += CheckAnswer;
    }

    public void Instantiate()
    {
        if (Grid)
        {
            _slotItemGrid = Grid.SlotItemGrid;
        }
    }

    public void CreateQuest()
    {
        string textQuest;
        var countTryRollTextQuest = Grid.ColumnCount * Grid.RowCount;
        do
        {
            var randomX = Random.Range(0, Grid.ColumnCount);
            var randomY = Random.Range(0, Grid.RowCount);
            textQuest = _slotItemGrid.GetGridObject(randomX, randomY).GetSlotIdentifier();
            countTryRollTextQuest--;
        } while (usedTextQuest.Contains(textQuest) && countTryRollTextQuest > 0);
        
        _textQuest = textQuest;
        TextQuest.text = "Find: " + textQuest;
        usedTextQuest.Add(textQuest);
    }

    private void CheckAnswer(string text)
    {
        OnCheckAnswer?.Invoke(text, text == _textQuest);
    }

    private void OnDestroy()
    {
        ChangeLevel.onLevelChange -= Instantiate;
        SelectedGridSlot.OnClickSlotItem -= CheckAnswer;
    }
}
