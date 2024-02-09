using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AnimationGenerator : MonoBehaviour
{
    public static AnimationGenerator instance;
    public static AnimationGenerator Instance
    {
        get
        {
            return instance;
        }
    }

    public List<Sprite> Sprites;
    public int FPS = 12;

    private void Start()
    {
        instance = this;
    }

    public void SetSprites(Object[] objs)
    {
        ClearSprites();
        foreach(Object obj in objs)
        {
            Sprites.Add((Sprite)obj);
        }
    }

    public AnimationClip Generate()
    {
        AnimationClip animClip = new AnimationClip();
        animClip.frameRate = FPS;
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";
        ObjectReferenceKeyframe[] spriteKeyFrames =
            new ObjectReferenceKeyframe[Sprites.Count];

        for (int i = 0; i < Sprites.Count; i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = ((float)i / animClip.frameRate);
            spriteKeyFrames[i].value = Sprites[i];
        }

        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

        return animClip;
    }

    public void ClearSprites()
    {
        Sprites.Clear();
    }

    public void AddSprite(Object obj)
    {
        Sprites.Add((Sprite)obj);
    }
}
