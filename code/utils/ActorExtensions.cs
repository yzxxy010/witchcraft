using UnityEngine;

namespace VideoCopilot.code.utils;

public static class ActorExtensions
{
    private const string yuanneng_key = "wushu.yuannengNum";
    private const string meditation_key = "wushu.meditationNum";
    private const string resurrection_key = "wushu.resurrectionNum";
    private const string Rresurrection_key = "wushu.RresurrectionNum";
    private const string accuracy_key = "wushu.accuracy";
    private const string dodge_key = "wushu.dodge";
    public static float GetAccuracy(this Actor actor)
    {
        actor.data.get(accuracy_key, out float val, 0);
        return val;
    }

    public static void SetAccuracy(this Actor actor, float val)
    {
        actor.data.set(accuracy_key, val);
    }

    public static void ChangeAccuracy(this Actor actor, float delta)
    {
        actor.data.get(accuracy_key, out float val, 0);
        val += delta;
        actor.data.set(accuracy_key, Mathf.Max(0, val));
    }

    public static float GetDodge(this Actor actor)
    {
        actor.data.get(dodge_key, out float val, 0);
        return val;
    }

    public static void SetDodge(this Actor actor, float val)
    {
        actor.data.set(dodge_key, val);
    }

    public static void ChangeDodge(this Actor actor, float delta)
    {
        actor.data.get(dodge_key, out float val, 0);
        val += delta;
        actor.data.set(dodge_key, Mathf.Max(0, val));
    }
    public static float GetRresurrection(this Actor actor)
    {
        actor.data.get(Rresurrection_key, out float val, 1);
        return val;
    }
    public static void SetRresurrection(this Actor actor, float val)
    {
        actor.data.set(Rresurrection_key, val);
    }
    public static void ChangeRresurrection(this Actor actor, float delta)
    {
        actor.data.get(Rresurrection_key, out float val, 0);
        val += delta;
        actor.data.set(Rresurrection_key, Mathf.Max(1, val));
    }

    public static float GetResurrection(this Actor actor)
    {
        actor.data.get(resurrection_key, out float val, 0);
        return val;
    }
    public static void SetResurrection(this Actor actor, float val)
    {
        actor.data.set(resurrection_key, val);
    }
    public static void ChangeResurrection(this Actor actor, float delta)
    {
        actor.data.get(resurrection_key, out float val, 0);
        val += delta;
        actor.data.set(resurrection_key, Mathf.Max(0, val));
    }

    public static float GetMeditation(this Actor actor)
    {
        actor.data.get(meditation_key, out float val, 0);
        return val;
    }
    public static void SetMeditation(this Actor actor, float val)
    {
        actor.data.set(meditation_key, val);
    }
    public static void ChangeMeditation(this Actor actor, float delta)
    {
        actor.data.get(meditation_key, out float val, 0);
        val += delta;
        actor.data.set(meditation_key, val);
    }

    public static float GetYuanNeng(this Actor actor)
    {
        actor.data.get(yuanneng_key, out float val, 0);

        return val;
    }

    public static void SetYuanNeng(this Actor actor, float val)
    {
        actor.data.set(yuanneng_key, val);
    }

    public static void ChangeYuanNeng(this Actor actor, float delta)
    {
        actor.data.get(yuanneng_key, out float val, 0);
        val += delta;
        actor.data.set(yuanneng_key, Mathf.Max(0, val));
    }
}