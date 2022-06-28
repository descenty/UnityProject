using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TextWritingClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private TextWritingBehavior template = new TextWritingBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<TextWritingBehavior>.Create(graph, template);
    }

    public ClipCaps clipCaps
    {
        get
        {
            return ClipCaps.None;
        }
    }
}
