using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boby
{
    public interface IPage
    {
        /// <summary>
        /// ������ ���̵�
        /// </summary>
        int ID { get; }
        string BGM {get;}

        void Initialize();
        void Terminate();

        void Prepare();

        IEnumerator Enter();
        IEnumerator Exit();
        void Update();
        void LateUpdate();
    }
    
}
