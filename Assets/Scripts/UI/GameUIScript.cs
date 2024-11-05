using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    private static readonly int SCORE_FACTOR = 10; 

    [SerializeField] private TextMeshProUGUI scorleLabel;
    [SerializeField] private TextMeshProUGUI highestScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        scorleLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighestScoreString();
    }

    // Update is called once per frame
    void Update()
    {
        scorleLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighestScoreString();
    }

    private string GetScoreString() {
        return (GameManager.Instance.GetScore() * SCORE_FACTOR).ToString();
    }

    private string GetHighestScoreString() {
        return (GameManager.Instance.GetHighestScore() * SCORE_FACTOR).ToString();
    }
}

