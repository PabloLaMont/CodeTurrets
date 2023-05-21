using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLineError : CodeLine, IError
{
    public float Damage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }
}
