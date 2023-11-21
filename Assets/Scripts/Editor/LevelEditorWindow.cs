// LevelEditorWindow.cs

using System;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using CardData = DefaultNamespace.Data.CardData;

public class LevelEditorWindow : EditorWindow
{
    private LevelData levelData;
    private int levelNumber;
    private int rows;
    private int itemsNumber;
    private int matchCount; // in most game it is 2
    private int left;
    private int right;
    private int top;
    private int bottom;
    private int spacingX;
    private int spacingY;
    private int width;
    private int height;
    private Sprite cardsBack;
    private Sprite[] cardsFront;
    private Vector2 scrollPosition;
    private GameObject cardPrefab;
    private string levelIndexInput = "1"; // Default to level 1


    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("Level Editor", EditorStyles.boldLabel);
        itemsNumber = EditorGUILayout.IntField("itemsNumber", itemsNumber);
        //levelData = EditorGUILayout.ObjectField("Level Data", levelData, typeof(LevelData), false) as LevelData;
        levelData = LoadScriptableObject();
        cardPrefab = EditorGUILayout.ObjectField("cardPrefab", cardPrefab, typeof(GameObject), false) as GameObject;
        EditorGUILayout.LabelField("cardFront Image", EditorStyles.boldLabel);

        // Check if the array needs resizing
        if (cardsFront == null || cardsFront.Length != itemsNumber)
        {
            cardsFront = new Sprite[itemsNumber];
        }

        if (cardsFront != null)
        {
            for (int i = 0; i < cardsFront.Length; i++)
            {
                // Use ObjectField for each element in the array
                cardsFront[i] =
                    EditorGUILayout.ObjectField("cardFront " + i, cardsFront[i], typeof(Sprite), true) as Sprite;
            }
        }

        EditorGUILayout.LabelField("cardsBack Image", EditorStyles.boldLabel);
        cardsBack = EditorGUILayout.ObjectField("cardsBack ", cardsBack, typeof(Sprite), true) as Sprite;

        // Select or create a LevelDataScriptableObject

        // Input fields for rows and columns
        rows = EditorGUILayout.IntField("Rows", rows);
        matchCount = EditorGUILayout.IntField("matchCount", matchCount);
        levelNumber = EditorGUILayout.IntField("levelNumber", levelNumber);
        EditorGUILayout.LabelField("Padding", EditorStyles.boldLabel);

        left = EditorGUILayout.IntField("Left", left);
        right = EditorGUILayout.IntField("Right", right);
        top = EditorGUILayout.IntField("Top", top);
        bottom = EditorGUILayout.IntField("Bottom", bottom);

        EditorGUILayout.LabelField("Spacing", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();

        spacingX = EditorGUILayout.IntField("X", spacingX);
        spacingY = EditorGUILayout.IntField("Y", spacingY);

        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Card Size", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        GUILayout.EndHorizontal();

        // Button to create a new level
        if (GUILayout.Button("Create New Level"))
        {
            CreateNewLevel();
        }

        // Input field for the desired level index
        levelIndexInput = EditorGUILayout.TextField("Desired Level Index", levelIndexInput);

        // Button to test the current level
        if (GUILayout.Button("Test Level"))
        {
            // Parse the input string to get the desired level index
            if (int.TryParse(levelIndexInput, out int desiredLevelIndex))
            {
                // Load the level from Resources/Levels
                //LevelData levelToTest = LoadLevel(desiredLevelIndex);

                // Check if the level is successfully loaded
                //if (levelToTest != null)
                //{
                    // Test the loaded level
                    TestLevel(desiredLevelIndex);
               // }
               // else
              //  {
              //      Debug.LogWarning("Failed to load the specified level.");
              //  }
            }
            else
            {
                Debug.LogError("Invalid level index. Please enter a valid integer.");
            }
        }
        // Button to clear tested level
        if (GUILayout.Button("CLear Scene"))
        {
              CLearStage();
        }

        GUILayout.EndScrollView();

    }

    private void CLearStage()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            // Create a list to store the children
            List<Transform> childrenToDestroy = new List<Transform>();

            // Add all children to the list
            foreach (Transform child in levelManager.spawnPoint.transform)
            {
                childrenToDestroy.Add(child);
            }

            // Iterate over the list and destroy the children
            foreach (Transform child in childrenToDestroy)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    private void CreateNewLevel()
    {
        // Create a new instance of LevelDataScriptableObject
        levelData = CreateInstance<LevelData>();
        levelData.desiredRows = rows;
        levelData.itemsNumber = itemsNumber;
        levelData.paddingLeft = left;
        levelData.paddingRight = right;
        levelData.paddingTop = top;
        levelData.paddingBottom = bottom;
        levelData.spacingX = spacingX;
        levelData.spacingY = spacingY;
        levelData.width = width;
        levelData.height = height;
        levelData.matchCount = matchCount;
        levelData.card = cardPrefab;
        levelData.cards = new List<CardData>();
        // Loop through the cardFront array and add new CardData instances to the cards list
        for (int i = 0; i < levelData.matchCount; i++)
        {
            for (int j = 0; j < cardsFront.Length; j++)
            {
                CardData newCardData = new CardData();
                newCardData.cardFrontImage = cardsFront[i];
                newCardData.cardBackImage = cardsBack;
                newCardData.index = j;
                levelData.cards.Add(newCardData);
            }
        }

        string path = "Assets/Resources/Levels";

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/Level " + levelNumber + ".asset");
        AssetDatabase.CreateAsset(levelData, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        // Focus the project window
        Selection.activeObject = levelData;

        Debug.Log("Created new level: " + levelData.name);
    }

    LevelData LoadScriptableObject()
    {
        // Specify the path to your Scriptable Object asset
        string assetPath = "Assets/ScriptableObjects/LevelData.asset"; // Change this to your actual path

        // Load the Scriptable Object
        LevelData level = AssetDatabase.LoadAssetAtPath<LevelData>(assetPath);
        return level;
    }

    private LevelData LoadLevel(int levelIndex)
    {
        // Load the level from Resources/Levels
        string levelPath = "Levels/Level " + levelIndex;
        Debug.Log(levelIndex);
        LevelData level = Resources.Load<LevelData>(levelPath);
        Debug.Log(level.name);
        return level;
    }

    private void TestLevel(int level)
    {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.GenerateCards(level);
            }
            else
            {
                Debug.LogWarning("LevelManager not found in the scene.");
            }
        
     
    }
}
