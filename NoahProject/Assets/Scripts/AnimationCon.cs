using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCon
{
    public static void SetPlayerRun(Animator anim, bool run)
    {
        anim.SetBool("IsRunning", run);
    }
    public static void SetPlayerPistol(Animator anim, bool equip)
    {
        //anim.SetBool("EquipPistol", equip);
    }
}
