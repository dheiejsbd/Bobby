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
            /// Transform의 Find는 자기 자식들 중에 같은 이름의 오브젝트르 찾는다.
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
