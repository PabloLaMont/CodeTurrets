using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret
{
    IEnumerator Compile(CodeLine codeLine, GameObject item);

}

public interface IGpu : ITurret
{
   
}
public interface ICooler : ITurret
{
    
}

