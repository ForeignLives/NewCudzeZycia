using UnityEngine;
using UnityEngine.Playables;
public class TimelinePauseStart : MonoBehaviour
{
    public void PauseTimeline(PlayableDirector _Timeline)
    {
        _Timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void ResumeTimeline(PlayableDirector _Timeline)
    {
        _Timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
