﻿using UnityEngine;

[CreateAssetMenu(fileName = "New LevelBundleData", menuName = "Level Bundle Data", order = 10)]
public class LevelBundleData : ScriptableObject
{
    [SerializeField] private LevelData _levelData;

    public LevelData LevelData => _levelData;
}