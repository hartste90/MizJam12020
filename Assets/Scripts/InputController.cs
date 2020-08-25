using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
    private bool isInputEnabled = true;

    public static bool Enabled
    {
        get { return Instance.isInputEnabled; }
        set { Instance.isInputEnabled = value; }

    }
}
