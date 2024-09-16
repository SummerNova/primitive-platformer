using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    string _text = "0000";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.UpdateScore += IncrementScore;
    }

    private void IncrementScore(int newScore)
    {
        bool isNegative = false;
        if (newScore < 0)
        {
            isNegative = true;
            newScore *= -1;
        }

        _text = newScore.ToString();

        while (_text.Length < 4)
        {
            _text = "0" + _text;
        }

        if (isNegative)  _text = "-" + _text;

        _textMeshPro.text = _text;
    }
}
