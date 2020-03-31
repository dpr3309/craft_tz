using System;
using UnityEngine;

namespace Craft_TZ.View
{

public static class Extentions
{
    public static T Clone<T>(this T origin, bool setAcitve = true) where T : Component
    {
        T item = GameObject.Instantiate(origin, origin.transform.parent) as T;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        item.gameObject.SetActive(setAcitve);
        return item;
    }
}

}