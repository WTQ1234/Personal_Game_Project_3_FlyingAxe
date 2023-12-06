using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace HRL
{
    // 不太对劲，keycode没法检测
    public enum InputOccasion
    {
        Update,
        LateUpdate,
        FixedUpdate,
    }

    public enum ButtonType
    {
        Down,
        Up,
        Press,
    }

    public class InputManager : MonoSingleton<InputManager>
    {
        [ShowInInspector]
        private Dictionary<int,
            Dictionary<ButtonType, UnityAction>> Dict_UpdateAction;
        [ShowInInspector]
        private Dictionary<int,
            Dictionary<ButtonType, UnityAction>> Dict_LateUpdateAction;
        [ShowInInspector]
        private Dictionary<int,
            Dictionary<ButtonType, UnityAction>> Dict_FixedUpdateAction;

        protected override void Init()
        {
            Dict_UpdateAction = new Dictionary<int, Dictionary<ButtonType, UnityAction>>();
            Dict_LateUpdateAction = new Dictionary<int, Dictionary<ButtonType, UnityAction>>();
            Dict_FixedUpdateAction = new Dictionary<int, Dictionary<ButtonType, UnityAction>>();
        }

        public void RegisterKeyboardAction(InputOccasion inputOccasion, KeyCode keyCode, ButtonType buttonType, UnityAction unityAction)
        {
            var target_dict = __GetTargetDict(inputOccasion);
            __RegisterAction(target_dict, (int)keyCode, buttonType, unityAction);
        }

        public void UnRegisterKeyboardAction(InputOccasion inputOccasion, KeyCode keyCode, ButtonType buttonType, UnityAction unityAction)
        {
            var target_dict = __GetTargetDict(inputOccasion);
            __UnRegisterAction(target_dict, (int)keyCode, buttonType, unityAction);
        }

        public void RegisterMouseAction(InputOccasion inputOccasion, int mouseCode, ButtonType buttonType, UnityAction unityAction)
        {
            var target_dict = __GetTargetDict(inputOccasion);
            __RegisterAction(target_dict, mouseCode * -1, buttonType, unityAction);
        }

        public void UnRegisterMouseAction(InputOccasion inputOccasion, int mouseCode, ButtonType buttonType, UnityAction unityAction)
        {
            var target_dict = __GetTargetDict(inputOccasion);
            __UnRegisterAction(target_dict, mouseCode * -1, buttonType, unityAction);
        }

        private Dictionary<int, Dictionary<ButtonType, UnityAction>> __GetTargetDict(InputOccasion inputOccasion)
        {
            Dictionary<int, Dictionary<ButtonType, UnityAction>> target_dict = null;
            switch (inputOccasion)
            {
                case InputOccasion.Update:
                    target_dict = Dict_UpdateAction;
                    break;
                case InputOccasion.FixedUpdate:
                    target_dict = Dict_FixedUpdateAction;
                    break;
                case InputOccasion.LateUpdate:
                    target_dict = Dict_LateUpdateAction;
                    break;
            }
            return target_dict;
        }

        private void __RegisterAction(Dictionary<int, Dictionary<ButtonType, UnityAction>> target_dict, int code, ButtonType buttonType, UnityAction unityAction)
        {
            if (target_dict.TryGetValue(code, out var dict_ButtonTypePairs))
            {
                if (dict_ButtonTypePairs.ContainsKey(buttonType))
                {
                    dict_ButtonTypePairs[buttonType] += unityAction;
                }
                else
                {
                    dict_ButtonTypePairs.Add(buttonType, unityAction);
                }
            }
            else
            {
                var dict = new Dictionary<ButtonType, UnityAction>();
                dict.Add(buttonType, unityAction);
                target_dict.Add(code, dict);
            }
        }

        private void __UnRegisterAction(Dictionary<int, Dictionary<ButtonType, UnityAction>> target_dict, int code, ButtonType buttonType, UnityAction unityAction)
        {
            if (target_dict.ContainsKey(code))
            {
                var dict_ButtonTypePairs = target_dict[code];
                if (dict_ButtonTypePairs.ContainsKey(buttonType))
                {
                    dict_ButtonTypePairs[buttonType] -= unityAction;
                    if (dict_ButtonTypePairs[buttonType] == null)
                    {
                        dict_ButtonTypePairs.Remove(buttonType);
                    }
                }
                if (dict_ButtonTypePairs.Count == 0)
                {
                    target_dict.Remove(code);
                }
            }
        }

        private void Update()
        {
            CheckInput(Dict_UpdateAction);
        }

        private void FixedUpdate()
        {
            CheckInput(Dict_FixedUpdateAction);
        }

        private void LateUpdate()
        {
            CheckInput(Dict_LateUpdateAction);
        }

        private void CheckInput(Dictionary<int,
            Dictionary<ButtonType, UnityAction>> Dict_Input)
        {
            foreach (var pairs in Dict_Input)
            {
                int code = pairs.Key;
                var Dict_ButtonTypePairs = pairs.Value;
                foreach (var ButtonTypePairs in Dict_ButtonTypePairs)
                {
                    ButtonType buttonType = ButtonTypePairs.Key;
                    UnityAction unityAction = ButtonTypePairs.Value;
                    if (code <= 0)
                    {
                        switch (buttonType)
                        {
                            case ButtonType.Up:
                                if (Input.GetMouseButtonUp(code * -1)) { unityAction.Invoke(); }
                                break;
                            case ButtonType.Down:
                                if (Input.GetMouseButtonDown(code * -1)) { unityAction.Invoke(); }
                                break;
                            case ButtonType.Press:
                                if (Input.GetMouseButton(code * -1)) { unityAction.Invoke(); }
                                break;
                        }
                    }
                    else
                    {
                        switch (buttonType)
                        {
                            case ButtonType.Up:
                                if (Input.GetKeyUp((KeyCode)code)) { unityAction.Invoke(); }
                                break;
                            case ButtonType.Down:
                                if (Input.GetKeyDown((KeyCode)code)) { unityAction.Invoke(); }
                                break;
                            case ButtonType.Press:
                                if (Input.GetKey((KeyCode)code)) { unityAction.Invoke(); }
                                break;
                        }
                    }
                }
            }
        }
    }
}