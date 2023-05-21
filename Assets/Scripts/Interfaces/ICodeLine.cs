using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface ICodeLine
{
    string Text { get; set; }
    string Type { get; set; }
    float TimeToMove { get; set; }
    float Complexity { get; set; }
    string[] Answers { get; set; }


    IEnumerator Move();
    void Die();
    void SetData(CodeLineData data);
}
public interface IError : ICodeLine
{
    float Damage { get; set; }
    void Hit();
}

public interface IWarning : ICodeLine
{
    void BadPractice();
}

