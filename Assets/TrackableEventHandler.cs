using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    public bool isTracked = false;
    public GameObject imagen, cubo3D;

    protected virtual void Awake()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        imagen.SetActive(false);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            trackedBehaviour();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            trackLostBehaviour();
        }
        else
        {
            trackLostBehaviour();
        }
    }

    private void trackedBehaviour()
    {
        isTracked = true;

        if (!isBothTargetsTracked())
            imagen.SetActive(true);
        else
            cubo3D.SetActive(true);
    }

    private void trackLostBehaviour()
    {
        isTracked = false;
        imagen.SetActive(false);
        cubo3D.SetActive(false);
    }

    private bool isBothTargetsTracked()
    {
        bool value = true;

        foreach (TrackableBehaviour m in FindObjectsOfType<TrackableBehaviour>())
        {
            if (!m)
                value = false;
        }
        return value;
    }
}
