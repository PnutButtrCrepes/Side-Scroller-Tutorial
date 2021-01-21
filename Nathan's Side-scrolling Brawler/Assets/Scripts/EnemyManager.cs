using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxEnemies;
    public int maxRushingEnemies;

    public static int surroundingEnemies;
    public static List<bool> isSurroundPositionOccupied;

    // Start is called before the first frame update
    void Start()
    {
        surroundingEnemies = 0;
        isSurroundPositionOccupied = new List<bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeNumberOfSurroundingEnemies(int a)
    {
        surroundingEnemies += a;

        isSurroundPositionOccupied.Clear();
        for(int i = 0; i < surroundingEnemies; i++)
        {
            isSurroundPositionOccupied.Add(false);
        }
    }
}
