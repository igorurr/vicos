

using System;
using System.Collections.Generic;
using UnityEngine;
using Component = vicos.Component;

public class App : Component
{
    private int countRender3;
    
    public App()
        : base( new dobj(){ { "id", "app" } } )
    {
        InitData(
            new dobj(){
                { "dyada", "vasya" },
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

    internal override Component[] Render()
    {
        return new Component[]
        {
            new Box1( 
                new dobj() { {"id", "box1"}, {"class", "box1"}, {"state", "usualy"} }, 
                new Box2(
                    new dobj() { {"class", "box2"}, {"state", "navy"} }
                ) 
            ),
            new Box2(
                new dobj() { {"id", "box2"}, {"class", "box2"}, {"state", "navy"} }
            ) 
        };
    }

    protected override void OnWillMount()
    {
        Stash = new dobj() {{"countRender3", 0}};
    }
    
    protected override void OnDidMount()
    {
        Corutine.Tick( () => Data = new dobj() { { "hui", 3 } }, 1 );
    }
    
    protected override void OnUpdate()
    {
        Stash = new dobj() {{"countRender3", (int)(Stash["countRender3"])+1}};
    }

    protected override void OnUnmount()
    {
        Debug.Log(Stash["countRender3"]);
    }
}