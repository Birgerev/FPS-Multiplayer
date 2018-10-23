using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput
{
    public bool interact1down;
    public bool interact1;
    public bool interact2;
    public bool reload;

    public WeaponInput(bool interact1down, bool interact1, bool interact2, bool reload)
    {
        this.interact1down = interact1down;
        this.interact1 = interact1;
        this.interact2 = interact2;
        this.reload = reload;
    }

    public WeaponInput()
    {

    }
}
