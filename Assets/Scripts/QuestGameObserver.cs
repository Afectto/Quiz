using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestGameObserver : MonoBehaviour
{
    [SerializeField] private Text TextQuest;
    private string _textQuest;

    private List<string> usedTextQuest = new List<string>();
    
    [SerializeField] private CreateGrid Grid;
    private Grid<SlotGridObject> _slotItemGrid;

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
        do
        {
            var randomX = Random.Range(0, Grid.ColumnCount);
            var randomY = Random.Range(0, Grid.RowCount);
            textQuest = _slotItemGrid.GetGridObject(randomX, randomY).GetSlotIdentifier();
        } while (usedTextQuest.Contains(textQuest));

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
