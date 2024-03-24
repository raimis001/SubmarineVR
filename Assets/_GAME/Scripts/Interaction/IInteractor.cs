using UnityEngine;
using UnityEngine.Events;

public class IInteractor : MonoBehaviour
{

    [SerializeField]
    UnityEvent<XRHand, IInteractor> OnInteract;
    [SerializeField]
    UnityEvent<XRHand, IInteractor> OnStay;

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Hand"))
            return;
        
        XRHand hand = other.GetComponentInParent<XRHand>();
        if (!hand) 
            return;

        OnStay.Invoke(hand, this);

        if (!hand.gripHold)
            return;

        OnInteract.Invoke(hand, this);
    }
}
