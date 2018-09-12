using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class JetGesture : MonoBehaviour, IManipulationHandler
{
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
    }

    void IManipulationHandler.OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    private Vector3 manipulationOriginalPosition = Vector3.zero;
}