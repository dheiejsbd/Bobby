﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bobby
{
    public static class CoolTimeImage
    {
        public static void UpdateUI(Image Image, float Time, float TargetTime)
        {
            Image.fillAmount = Time / TargetTime;
        }
    }
}
