

using UnityEngine;

namespace Bobby
{
    public delegate void Complete(GameObject obj);

    public delegate void ClickButton(GameObject Button);
    public delegate void DoubleClickButton(GameObject Button);
    public delegate void PressButton(GameObject Button);
    public delegate void ReleaseButton(GameObject Button);
}