using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ModelSubscriber
{
    public SceneComponent Component;

    public ModelSubscriber( SceneComponent _component )
    {
        Component = _component;
    }
}