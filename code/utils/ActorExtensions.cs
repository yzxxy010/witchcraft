using UnityEngine;

namespace VideoCopilot.code.utils;

public static class ActorExtensions
{
    private const string yuanneng_key = "wushu.yuannengNum";
    private const string benyuan_key  = "wushu.benyuanNum";
    private const string meditation_key = "wushu.meditationNum";
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

    public static float GetBenYuan(this Actor actor)
    {
        actor.data.get(benyuan_key, out float val, 0);

        return val;
    }

    public static void ChangeBenYuan(this Actor actor, float delta)
    {
        actor.data.get(benyuan_key, out float val, 0);
        val += delta;
        actor.data.set(benyuan_key, Mathf.Max(0, val));
    }
}