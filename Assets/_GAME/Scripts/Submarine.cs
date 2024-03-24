using UnityEngine;

public class Submarine : MonoBehaviour
{
    [SerializeField]
    float shotDelay = 1;
    [SerializeField]
    Transform torpedoPoint;

    float _shotDelay = 0;
    private void Update()
    {
        if (_shotDelay > 0)
        {
            _shotDelay -= Time.deltaTime;
            return;
        }

        if (XRHand.right.triggerDown || XRHand.left.triggerDown)
            Shot();

    }

    void Shot()
    {
        _shotDelay = shotDelay;
        GameObject torpedo = Pool.GetPool("Torpedo");

        torpedo.transform.SetPositionAndRotation(torpedoPoint.position, torpedoPoint.rotation);
    }

    public void OnHandStay(XRHand hand, IInteractor interactor)
    {
        if (_shotDelay > 0)
            return;

        if (hand.triggerDown)
            Shot();

    }
}
