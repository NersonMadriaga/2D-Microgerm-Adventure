using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int level;

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        level = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
