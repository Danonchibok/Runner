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
        string presentsText = Generate(points, "�������", "�������", "��������");
        string pointsText = Generate(points, "����", "�����", "������");
        _presentsInPoints.text = $"{points} {presentsText} = {points} {pointsText}";  
    }

    /// <summary>
    /// ���������� ����� � ������, ��������� �� ��������� �����
    /// </summary>
    /// <param name="number">����� �� �������� ������� ��������� �����</param>
    /// <param name="nominativ">������������ ����� �����. �������� "����"</param>
    /// <param name="genetiv">����������� ����� �����. �������� "���"</param>
    /// <param name="plural">������������� ����� �����. �������� "����"</param>
    /// <returns></returns>
    private string Generate(int number, string nominativ, string genetiv, string plural)
    {
        var titles = new[] { nominativ, genetiv, plural };
        var cases = new[] { 2, 0, 1, 1, 1, 2 };
        return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
    }
}
