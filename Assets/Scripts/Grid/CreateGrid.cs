using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grid
{
    public class CreateGrid : MonoBehaviour
    {
        [SerializeField] private Slot SlotPrefab;
        [SerializeField] private Transform Background;
        [SerializeField] private CardBundleData[] CardDataArray;
        [SerializeField] private LevelBundleData[] LevelDataArray;

        private Grid<SlotGridObject> _slotItemGrid;
        private int _rowCount;
        private int _columnCount;
        private float _cellSize = 1f;
        private int _currentLevel;

        public int RowCount => _rowCount;
        public int ColumnCount => _columnCount;
        public Grid<SlotGridObject> SlotItemGrid => _slotItemGrid;

        public static Action<Transform> onStartFirstLevelSlotAnimation;
        public static Action onStartFirstLevel;
        public static Action onCompleteGame;

        public void GenerateLevel(int levelNum)
        {
            if (LevelDataArray.Length <= 0) return;
            if (levelNum >= LevelDataArray.Length)
            {
                onCompleteGame?.Invoke();
                return;
            }

            ClearGrid();
            
            _currentLevel = levelNum;
            
            FillLevelData();
            UpdateBackgroundScale();

            _slotItemGrid = new Grid<SlotGridObject>(_columnCount, _rowCount, _cellSize,
                new Vector3(-_columnCount / 2f * _cellSize, -_rowCount / 2f * _cellSize),
                (grid, x, y) => new SlotGridObject(grid, x, y));

            if (CardDataArray.Length > 0)
            {
                var randomDataIndex = Random.Range(0, CardDataArray.Length);
                FillingGrid(CardDataArray[randomDataIndex]);
            }

            StartSlotAnimationIfNeed();
        }

        private void FillLevelData()
        {
            var levelData = LevelDataArray[_currentLevel].LevelData;
            _rowCount = levelData.RowCount;
            _columnCount = levelData.ColumnCount;
            _cellSize = levelData.CellSize;
        }

        private void UpdateBackgroundScale()
        {
            var scale = Background.localScale * _cellSize;
            scale.x = _columnCount * _cellSize + 0.1f * _cellSize;
            scale.y = _rowCount * _cellSize + 0.1f * _cellSize;
            Background.localScale = scale;
        }

        private void FillingGrid(CardBundleData Data)
        {
            List<int> usedIndexes = new List<int>();

            for (int i = 0; i < _columnCount; i++)
            {
                for (int j = 0; j < _rowCount; j++)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = Random.Range(0, Data.CardData.Length);
                    } while (usedIndexes.Contains(randomIndex));

                    usedIndexes.Add(randomIndex);

                    var cardData = Data.CardData[randomIndex];

                    var newSlot = CreateNewSlot(cardData, i, j);
                    _slotItemGrid.GetGridObject(i, j).SetSlotItem(newSlot);
                }
            }
        }

        private Slot CreateNewSlot(CardData cardData, int x, int y)
        {
            var worldPosition = _slotItemGrid.GetWorldPosition(x, y);
            worldPosition.x += 0.5f * _cellSize;
            worldPosition.y += 0.5f * _cellSize;

            Color randomColor = new Color(Random.value, Random.value, Random.value);

            var newSlot = Instantiate(SlotPrefab, worldPosition, Quaternion.Euler(cardData.RotateSprite));
            newSlot.ChangeItemSkin(cardData.Sprite);
            newSlot.ChangeBackgroundColor(randomColor);
            newSlot.ChangIdentifier(cardData.Identifier);
            newSlot.transform.localScale = new Vector3(_cellSize - 0.1f, _cellSize - 0.1f);

            return newSlot;
        }

        private void ClearGrid()
        {
            for (int i = 0; i < _columnCount; i++)
            {
                for (int j = 0; j < _rowCount; j++)
                {
                    _slotItemGrid.GetGridObject(i, j).Destroy();
                }
            }
        }

        private void StartSlotAnimationIfNeed()
        {
            if (_currentLevel != 0) return;
            onStartFirstLevel?.Invoke();
            for (int i = 0; i < _columnCount; i++)
            {
                for (int j = 0; j < _rowCount; j++)
                {
                    var transformSlot = _slotItemGrid.GetGridObject(i, j).GetSlot().transform;
                    onStartFirstLevelSlotAnimation?.Invoke(transformSlot);
                }
            }
        }
    }
}