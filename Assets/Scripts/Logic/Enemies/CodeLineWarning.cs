using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLineWarning : CodeLine, IWarning
{
    public void BadPractice()
    {
        throw new System.NotImplementedException();
    }


}
