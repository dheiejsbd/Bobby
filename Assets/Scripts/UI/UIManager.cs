using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boby
{
    public class UIManager 
    {
        protected List<UIWidget> Widgets;

        public void Initialize()
        {
            Widgets = new List<UIWidget>();
        }

        public void Terminate()
        {
        }

        public void Prepare()
        {
            Transform Client = GameObject.Find("Client").transform;
            /// Transform�� Find�� �ڱ� �ڽĵ� �߿� ���� �̸��� ������Ʈ�� ã�´�.
            Transform Canvas = Client.Find("Canvas");

            /// 
            //UIWidget Temps = Canvas.GetComponentInChildren<UIWidget>();
            UIWidget[] Temps = Canvas.GetComponentsInChildren<UIWidget>();
            Widgets.AddRange(Temps);
        }        

        public void OnUpdate()
        {
           
        }
    }

   
}
