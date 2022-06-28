using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[Serializable]
public class TextWritingBehavior : PlayableBehaviour
{
    private bool firstFramed = false;
    private string originalText;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Text UItext = playerData as Text;
        if (!firstFramed)
        {
            originalText = UItext.text;
            firstFramed = true;
        }
        UItext.text = originalText.Substring(0, Mathf.RoundToInt((float)(playable.GetTime() / playable.GetDuration())) * originalText.Length);
    }
}
