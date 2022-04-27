using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _presentCountText;
    [SerializeField] private TextMeshProUGUI _presentsInPoints;
    
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            SetProgress(GameManager.Instance.CurrentPoints);
        }
        else
        {
            SetProgress(0);
        }
    }

    public void SetProgress(int points)
    {
        _presentCountText.text = points.ToString();
        string presentsText = Generate(points, "Подарок", "Подарка", "Подарков");
        string pointsText = Generate(points, "Балл", "Балла", "Баллов");
        _presentsInPoints.text = $"{points} {presentsText} = {points} {pointsText}";  
    }

    /// <summary>
    /// Возвращает слова в падеже, зависимом от заданного числа
    /// </summary>
    /// <param name="number">Число от которого зависит выбранное слово</param>
    /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
    /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
    /// <param name="plural">Множественное число слова. Например "дней"</param>
    /// <returns></returns>
    private string Generate(int number, string nominativ, string genetiv, string plural)
    {
        var titles = new[] { nominativ, genetiv, plural };
        var cases = new[] { 2, 0, 1, 1, 1, 2 };
        return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
    }
}
