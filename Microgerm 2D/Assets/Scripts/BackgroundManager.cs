using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    
    public static BackgroundManager Instance { get; private set; }

    [SerializeField] private GameObject background1, background2, background3;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FindGameObjects();
        DefaultSetting();
    }

    public void ChangeBackground(int level)
    {
        Debug.Log("Change background");
        DefaultSetting();

        switch (level)
        {
            case 1:
                background1.SetActive(true);
                break;
            case 2:
                background2.SetActive(true);
                break;
            case 3:
                background3.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void FindGameObjects()
    {
        background1 = GameObject.Find("Background1");
        background2 = GameObject.Find("Background2");
        background3 = GameObject.Find("Background3");
    }

    public void BackgroundEnabled()
    {
        background1.SetActive(true);
        background2.SetActive(true);
        background3.SetActive(true);
    }
    private void DefaultSetting()
    {
        background1.SetActive(false);
        background2.SetActive(false);
        background3.SetActive(false);
    }


}
