using HoloToolkit.Unity.InputModule;
using UnityEngine;

public abstract class JetGesture : MonoBehaviour, IManipulationHandler
{
    public GameObject MainFrame;

    protected abstract bool IsRight { get; set; }

    protected abstract double NeededX { get; }

    protected abstract double NeededY { get; }

    protected abstract double NeededZ { get; }

    private bool IsApproxEqual(double real, double needed, double acceptableError = 0.25)
    {
        return real <= needed + acceptableError &&  real >= needed - acceptableError;
    }

    private void OnDragging(double x, double y, double z)
    {
        var rightX = IsApproxEqual(transform.position.x, NeededX);
        var rightY = IsApproxEqual(transform.position.y, NeededY);
        var rightZ = IsApproxEqual(transform.position.z, NeededZ);

        IsRight = rightX && rightY && rightZ;
        if (IsRight)
        {
            transform.position = new Vector3((float)NeededX, (float)NeededY, (float)NeededZ);
        }
    }

    void IManipulationHandler.OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
        manipulationOriginalPosition = transform.position;
        _started = true;
    }

    void IManipulationHandler.OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (!_started)
        {
            return;
        }

        var moveVector = eventData.CumulativeDelta;
        var moveMultiplier = 5.0f;

        moveVector.x = moveVector.x * moveMultiplier;
        moveVector.y = moveVector.y * moveMultiplier;
        moveVector.z = moveVector.z * moveMultiplier;

        transform.position = manipulationOriginalPosition + moveVector;

        OnDragging(transform.position.x, transform.position.y, transform.position.z);
    }

    void IManipulationHandler.OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
        _started = false;
    }

    void IManipulationHandler.OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
        _started = false;
    }
    
    private bool _started;
    private Vector3 manipulationOriginalPosition = Vector3.zero;
}