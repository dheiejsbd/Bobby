using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public event Action startFlame;
    public event Action endFlame;

    public void StartFlame()
    {
        startFlame?.Invoke();
    }

    public void EndFlame()
    {
        endFlame?.Invoke();
    }
}
