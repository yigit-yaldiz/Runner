using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    int _score = 0;

    public static Score Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        _scoreText.text = _score.ToString();
    }
}
