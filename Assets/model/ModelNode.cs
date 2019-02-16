using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModelNode
{
    public string Path { get; private set; }

    private List<ModelNode> a_Childs;

    private List<ModelSubscriber> a_Subscribers;

    private ModelNode( string _path )
    {
        Path = _path;
        a_Childs = new List<ModelNode>();
        a_Subscribers = new List<ModelSubscriber>();
    }

    public void Invoke( dobj _data )
    {
        // тут будет не SceneElement а vicos Component, тут в коде я малясь погаречился
        /*if( a_Subscribers.Any() )
            foreach( ModelSubscriber subscriber in a_Subscribers )
                subscriber.Update();*/
    }

    /*public void Subscribe( SceneComponent _sceneComponent )
    {
        a_Subscribers.Add( new ModelSubscriber( _sceneComponent ) );
    }*/

    /*public void Unsubscribe( SceneComponent _sceneComponent )
    {
        ModelSubscriber subscriber = a_Subscribers.Find( el => el.Component == _sceneComponent );

        if( subscriber != null )
            Unsubscribe(subscriber);
    }*/

    public void Unsubscribe( ModelSubscriber _subscriber )
    {
        a_Subscribers.Remove( _subscriber );
    }

    public void CreateChild( string _path )
    {
        a_Childs.Add( new ModelNode( _path ) );
    }

    public void RemoveChild( ModelNode _node )
    {
        if( !a_Childs.Any() )
            return;

        if( a_Childs.Remove( _node ) )
            _node.Destroy();
    }

    private void Destroy()
    {
        if( a_Childs.Any() )
            foreach( ModelNode child in a_Childs )
                child.Destroy();

        if( a_Subscribers.Any() )
            foreach( ModelSubscriber subscriber in a_Subscribers )
                Unsubscribe( subscriber );
    }
}