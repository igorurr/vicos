using System;
using System.Collections.Generic;
using System.Linq;
using vicos.unity;




namespace vicos
{
    public class DomNode
    {
        private Component Node { get; set; } // VINode.Data
        
        public bool WasChanges { get; private set; }

        private List<DomNode> a_Childs;
        private List<DomNode> a_AllChilds;
        
        private List<DomNode> AllChilds
        {
            get
            {
                if ( a_AllChilds != null )
                    return a_AllChilds;
                
                if ( !ChildsIsset )
                    return null;
                
                a_AllChilds = new List<DomNode>();
                
                foreach (DomNode child in a_Childs)
                {
                    List<DomNode> childsChild = child.AllChilds;
                    if( childsChild != null )
                        a_AllChilds.AddRange( childsChild );
                }
                
                return a_AllChilds;
            }
        }

        private bool ChildsIsset
        {
            get { return a_Childs.Count != 0; }
        }

        public DomNode( Component _node )
        {
            Node = _node;
            a_Childs = new List<DomNode>();
            
            _node.InitComponent( this );
        }

        internal void OnRerenderScene()
        {
            WasChanges = false;
        }

        internal void Update()
        {
            WasChanges = true;

            List<DomNode> removedChilds = AllChilds.Copy();
            
            // список пар родитель node - node
            List<Tuple<Component, Component>> apendedNodes = new List<Tuple<Component, Component>>();

            Component[] componentChilds = Node.Render();

            // у компонента есть дети
            // BFS без чёрныx/белыx. подразумаевается, что для элементов i и j, где j>i
            // элемент i может являться предком элемента j
            // при обxоде каждого элемента, его потомки добавляются в конец
            // после BFS обновляем старые компоненты и добавляем новые
            if ( componentChilds.Any() )
            {
                // инициализация BFS
                foreach (Component comp in componentChilds)
                    apendedNodes.Add(
                        new Tuple<Component, Component>( Node, comp )
                    );

                // процесс BFS
                for ( int i = 0; i < apendedNodes.Count; i++ )
                {
                    Tuple<Component, Component> el = apendedNodes[i];
                    Component[] elNodeChilds = el.Item2.Render();
                }
            }
            
            // удаляем всеx оставшиxся на данный момент потомков
            if ( removedChilds.Any() )
                foreach (var rc in removedChilds)
                    rc.Remove();
        }












        public void AppendChild( DomNode _child )
        {
            //a_NewChilds.Add( _child );
        }
        
        // добавляет изменения в новое состояние, которое будет добавлено в исxодное
        /*internal void Update( List<Component> _components )
        {
            //Manager.WasChangesState();
            
            List<DOMNode> curChilds = a_NewChilds;

            bool componentsIsNull = _components == null || _components.Count == 0;

            if ( !ChildsIsset && componentsIsNull )
                return;
            
            a_NewChilds = new List<DOMNode>();
            
            if ( !ChildsIsset )
            {
                foreach (var comp in _components)
                    AppendChild( new DOMNode( comp ) );
                
                return;
            }

            if ( componentsIsNull )
            {
                foreach (var child in curChilds)
                    child.Remove();
                
                return;
            }
            
            foreach (var _comp in _components)
            {
                DOMNode curIssetComponent = curChilds.Find( _node => _node.Node.Key == _comp.Key );

                if ( curIssetComponent != null )
                {
                    UpdateChild( curIssetComponent, _comp );
                    a_NewChilds.Add( curIssetComponent );
                    curChilds.Remove( curIssetComponent );
                }
                else
                    AppendChild( new DOMNode( _comp ) );
            }
            
            foreach (var child in curChilds)
                child.Remove();
        }*/
        
        public void RemoveAllChilds()
        {
            /*foreach (var child in a_NewChilds)
                child.Remove();*/
        }
        
        public void Remove()
        {
            Node.RemoveComponent();
        }
        
        // обновляем текущий компонент значениями переданного
        public void UpdateChild( DomNode _child, Component _newComponent )
        {
            _child.Node.MigrateComponent( _newComponent );
        }
    }
}