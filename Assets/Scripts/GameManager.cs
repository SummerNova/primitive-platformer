using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    [SerializeField] MinimapManager minimapManager;

    public static GameManager instance { get; private set; }

    public Action<int> CollectedPoint = (s) => { };
    public Action<int> DamageTaken = (s) => { };
    public Action<int> UpdateScore = (s) => { };

    private int score = 0;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        CollectedPoint += AddScore;
        DamageTaken += RemoveScore;
    }

    private void AddScore(int modifier)
    {
        score += modifier;
        UpdateScore.Invoke(score);
    }

    private void RemoveScore(int modifier)
    {
        score -= modifier;
        UpdateScore.Invoke(score);
    }

}
