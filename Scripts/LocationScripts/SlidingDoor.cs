using UnityEngine;

public abstract class SlidingDoor : MonoBehaviour
{
    private bool isOpening;
    private bool isClosing;
    protected void OnTriggerEnter(Collider other)
    {
        isOpening = true;
        isClosing= false;
    }

    protected void OnTriggerExit(Collider other)
    {
        isClosing = true;
        isOpening = false;
    }

    private void Update()
    {
        if (isOpening) OpenDoor();
        if (isClosing) CloseDoor();
    }
    protected abstract void OpenDoor();
    protected abstract void CloseDoor();
}
