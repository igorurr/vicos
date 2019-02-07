

using System;
using System.Collections.Generic;
using UnityEngine;
using vicos.unity;
using Component = vicos.Component;

public class Box2 : Component
{
    private int countRender3;
    
    public Box2( dobj props, params Component[] childs )
        : base( props, childs )
    {
        InitData(
            new dobj(){
                { "petr", "fedua" },
                { "kuka", "reku" },
                { "chlen", 3 },
                { "chlen2", new dobj(){
                    { "dyada", "vasya" },
                    { "petr", "fedua" },
                    { "kuka", "reku" },
                    { "chlen", 3 },
                    { "chlen2", 3 }
                } }
            }
        );

        countRender3 = 0;
    }

    /*protected override void OnWillMount()
    {
        Debug.Log("OnWillMount App");
    }*/

    /*protected override bool ShouldUpdate()
    {
        Debug.Log("ShouldUpdate App");
        return base.ShouldUpdate();
    }

    protected override void OnUpdate()
    {
        Debug.Log("OnUpdate App");
        base.OnUpdate();
    }

    protected override void OnUnmount()
    {
        Debug.Log("OnUnmount App");
        base.OnUnmount();
    }*/

    internal override Component[] Render()
    {
        /*  <App>
         *    <box>
         *      <text>pupa zalupa menuaushauasya</text>
         *      <box param=menuausheesyavalue>
         *        <box>
         *          <box>
         *            <box>
         *              <text>pupa zalupa</text>
         *            </box>
         *            <text>pupa zalupa menuaushauasya</text>
         *          </box>
         *        </box>
         *      </box>
         *    </box>
         *  </App>
         *
         *     1 App
         *     1 box
         *     1 text
         *     4 box
         *     2 text
         */

        //Debug.Log("Render App");
        
        countRender3 += 3;

        return new Component[]
        {
            new Text(
                new dobj()
                {
                    { "key", "vasya" }
                },
                "pupa salupa"
            )
        };
    }
}