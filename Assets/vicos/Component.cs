using System;
using System.Collections.Generic;
using System.Linq;

namespace vicos
{
    public abstract class Component
    {
        #region child classes

        #endregion
        
        #region atributes

        dobj           a_Props;
        dobj           a_Data;
        dobj           a_Stash;
        
        public List<Component>      Childs { get; private set; }
        public VirtualDomNode       VirtualDomNode { get; private set; }
        
        #endregion
        
        #region properties
        
        public dobj Props {
            get { return a_Props; }
        }
        
        public tolis<string> Class {
            get { return (tolis<string>) a_Props["class"]; }
        }
        
        public string Key {
            get { return ((string) a_Props["key"]) == String.Empty ? ((string) a_Props["id"]) : ((string) a_Props["key"]); }
        }
        
        public string Id {
            get { return (string) a_Props["id"]; }
        }
        
        public dobj Data {
            get
            {
                return a_Data;
            }
            set
            {
                a_Data = value;
            }
        }
        
        public tolis<string> State {
            get
            {
                return (tolis<string>) a_Data["state"];;
            }
        }
        
        public dobj Stash {
            get
            {
                return a_Stash;
            }
            set
            {
                a_Stash = a_Stash.Merge( value );
            }
        }

        public bool ChildsIsset
        {
            get { return Childs != null && Childs.Count != 0; }
        }
        
        #endregion
        
        
        
        #region Public Methods

        public Component( dobj _props, Component _child )
            : this( _props, new List<Component>(){ _child } )
        {}

        public Component( dobj _props, List<Component> _childs = null )
        {
            a_Data  = new dobj();
            a_Stash = new dobj();
            
            InitProps( _props );
            InitChilds( _childs );
        }

        public void SetData( dobj _data )
        {
            dobj newDaata = a_Data.Merge( _data );

            if ( !newDaata.Equals(a_Data) )
            {
                a_Data = newDaata;
                UpdateComponent( Props, newDaata );
            }
        }
        
        #endregion
        
        #region Internal Methods
        // тут должны быть методы которые не будет видно за пределами namespace vicos, но иx почему то видно

        internal void InitComponent( VirtualDomNode _virtualDomNode )
        {
            VirtualDomNode = _virtualDomNode;
            
            OnWillMount();

            GetDerivedStateFromPropsOnStart();
            
            VirtualDomNode.Update( Render() );
            
            OnDidMount();
        }
        
        // перерисовка компонента из родителя
        internal void MigrateComponent( Component from )
        {
            UpdateComponent( from.Props, Data );
        }

        internal void RemoveComponent()
        {
            OnUnmount();
            
            VirtualDomNode.RemoveAllChilds();
        }

        #endregion
        
        #region Protected Methods

        protected virtual void OnWillMount() { }

        protected abstract List<Component> Render();

        protected virtual void OnDidMount() { }

        // Изменить данные компонента после получения новыx свойств
        // старые свойства доступны в Props
        // старые данные доступны в Data
        // возвращаются изменённые данные
        protected virtual dobj GetDerivedStateFromProps( dobj _newProps, dobj _newData )
        {
            return Data;
        }

        
        // Необxодимо ли обновлять компонент
        // все данные компонента обновлены
        protected virtual bool ShouldUpdate()
        {
            return true;
        }

        protected virtual void OnUpdate() { }

        protected virtual void OnUnmount() { }

        protected void InitData( dobj data )
        {
            a_Data = data;
        }

        #endregion
        
        #region Private Methods
        
        private void InitProps( dobj _props )
        {
            #if DEVELOPMENT
            
            // всяческие проверки свойств
                
            #endif
            
            a_Props = _props ?? new dobj();
        }
        
        private void InitChilds( List<Component> _childs )
        {
            #if DEVELOPMENT
            
            List<string> childsKeys = new List<string>();
            
            foreach (var child in _childs)
            {
                if( child.Key == "" )
                    throw new Exception("отсутствует обязательный параметр key");
                
                if( childsKeys.Contains( child.Key ) )
                    throw new Exception($"Значения key повторяются");
                
                childsKeys.Add( child.Key );
            }
                
            #endif
            
            Childs = _childs;
        }

        // в конструкторе свойства создаются по дефолту, данный в OnWillMount
        // , необходимо их сравнить с null при инициализации компонента
        private void GetDerivedStateFromPropsOnStart()
        {
            dobj curProps = a_Props;
            dobj curData  = a_Data;
            a_Props = null;
            a_Data  = null;
            a_Data  = GetDerivedStateFromProps( curProps, curData );
        }

        private void UpdateComponent( dobj _props, dobj _data )
        {
            a_Data = GetDerivedStateFromProps( _props, _data );
            UpdateComponent();
        }

        // обновляем текущий компонент значениями переданного
        private void UpdateComponent()
        {
            if ( !ShouldUpdate() )
                return;
            
            VirtualDomNode.Update( Render() );
            
            OnUpdate();
        }

        #endregion
        
    }
}