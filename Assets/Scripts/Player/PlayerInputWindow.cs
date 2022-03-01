using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Boby
{
    /*
    public class PlayerInputWindow 
    {
        Dictionary<string, bool> Buttons= new Dictionary<string, bool>();
        Vector3 Joystick;
        RectTransform Joystick1;
       public void Initialize()
       {
            Transform Client = GameObject.Find("Client").transform;
            Transform Canvas = Client.Find("Canvas");

            Transform Lobby = Canvas.Find("Lobby");
            Transform Game = Canvas.Find("Game");

            Transform PlayerInput = Lobby.Find("PlayerInput");
            Buttons.Add("Jump", false);
            Buttons.Add("Action", false);
            
            Joystick1 = PlayerInput.Find("JoystickGround").Find("Joystick").GetComponent<RectTransform>();


            UIButton Jump = PlayerInput.Find("JumpIamge").GetComponent<UIButton>();
            Jump.OnClickButton += (GameObject button) =>
            {
                Buttons["Jump"] = true;
            };

            UIButton Action = PlayerInput.Find("ActionImage").GetComponent<UIButton>();
            Action.OnClickButton += (GameObject button) =>
            {
                Buttons["Action"] = true;
            };
        }

        public void LateUpdate()
        {
            List<string> key = new List<string>();
            foreach (var item in Buttons)
            {
                key.Add(item.Key);
            }
            for (int i = 0; i < key.Count; i++)
            {
                Buttons[key[i]] = false;
            }
        }

        public bool GetButten(string ButtenName)
        {
            return Buttons[ButtenName];
        }
        public Vector3 GetMoveDIr()
        {
            Vector3 MovePos = new Vector3(Joystick1.localPosition.normalized.x,0,Joystick1.localPosition.normalized.y);
            return MovePos;
        }
    }
    */
}