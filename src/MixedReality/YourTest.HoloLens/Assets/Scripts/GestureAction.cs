// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity.InputModule;
using HoloToolkit.UX.Dialog;
using UnityEngine;

namespace Academy
{
    /// <summary>
    /// GestureAction performs custom actions based on 
    /// which gesture is being performed.
    /// </summary>
    public class GestureAction : MonoBehaviour, IManipulationHandler, ISpeechHandler
    {
        public Dialog dialogPrefab;
        public GameObject head;

        void IManipulationHandler.OnManipulationStarted(ManipulationEventData eventData)
        {
            InputManager.Instance.PushModalInputHandler(gameObject);
            manipulationOriginalPosition = transform.position;
        }

        void IManipulationHandler.OnManipulationUpdated(ManipulationEventData eventData)
        {
            transform.position = manipulationOriginalPosition + eventData.CumulativeDelta;
        }

        void IManipulationHandler.OnManipulationCompleted(ManipulationEventData eventData)
        {
            InputManager.Instance.PopModalInputHandler();

            var isX = IsApproxEqual(transform.position.x, head.transform.position.x);
            var isY = IsApproxEqual(transform.position.y, head.transform.position.y);
            var isZ = IsApproxEqual(transform.position.z, head.transform.position.z);

            _rightAnswer = isX && isY && isZ;
        }

        void IManipulationHandler.OnManipulationCanceled(ManipulationEventData eventData)
        {
            InputManager.Instance.PopModalInputHandler();
        }

        private bool IsApproxEqual(double real, double needed, double acceptableError = 0.025)
        {
            return needed + acceptableError >= real && needed - acceptableError <= real;
        }

        void ISpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
        {
            if (eventData.RecognizedText.ToLower().Equals("finish"))
            {
                MobileCommunicator.Instance.SendMessage(_rightAnswer.ToString());
            }
            else
            {
                return;
            }

            eventData.Use();
        }

        private bool _rightAnswer;
        private Vector3 manipulationOriginalPosition = Vector3.zero;
    }
}