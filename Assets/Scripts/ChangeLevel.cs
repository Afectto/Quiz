using System;
using Grid;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    private int _currentLevel = 0;
    [SerializeField] private CreateGrid Grid;
    [SerializeField] private QuestGameObserver QuestGameObserver;

    public static Action onLevelChange;

    private void Start()
    {
        if (Grid && QuestGameObserver)
        {
            GoNextLevel();
        }

        DOTweenAnimator.OnNeedChangeLevel += GoNextLevel;
        RestartGame.OnStartNewGame += RestartLevelGame;
    }

    private void GoNextLevel()
    {
        Grid.GenerateLevel(_currentLevel);
        onLevelChange?.Invoke();
            
        QuestGameObserver.CreateQuest();
        _currentLevel++;
    }

    private void RestartLevelGame()
    {
        _currentLevel = 0;
        GoNextLevel();
    }

    private void OnDestroy()
    {
        DOTweenAnimator.OnNeedChangeLevel -= GoNextLevel;
    }
}
