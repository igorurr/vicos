

using System;
using System.Collections.Generic;
using UnityEngine;
using vicos.unity;
using Component = vicos.Component;

public class Box1 : Component
{
    private int countRender3;
    
    public Box1( dobj props, params Component[] childs )
        : base( props, childs )
    {}

    /*protected override void OnWillMount()
    {
        Debug.Log("OnWillMount App");
    }*/

    protected override void OnDidMount()
    {
        //Debug.Log("OnDidMount App");

        //Corutine.Tick( () => Data = new dobj() { { "hui", 3 } }, 1 );
    }

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
            new Box(
                new dobj()
                {
                    { "key", "box1" },
                    { "class", "box1" },
                },
                new Box(
                    new dobj()
                    {
                        { "key", "box1" },
                        { "class", "box1" },
                    },
                    new Box(
                        new dobj()
                        {
                            { "key", "box1" },
                            { "class", "box1" },
                        },
                        new Text(
                            new dobj()
                            {
                                { "key", "text1" },
                                { "class", "text" },
                            },
                            "Какойто текст тут будет"
                        ),
                        new Box(
                            new dobj()
                            {
                                { "key", "box1" },
                                { "class", "box1" },
                            },
                            Childs
                        )
                    )
                )
            ),
            new Text(
                new dobj()
                {
                    { "key", "text1" },
                    { "class", "text" },
                },
                "Какойто текст тут будет"
            )
        };
    }
}