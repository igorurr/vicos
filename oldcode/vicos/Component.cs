

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
        List<string>   a_Class;
        string         a_Key;
        string         a_Id;
        dobj           a_Data;
        List<string>   a_State;
        dobj           a_Stash;
        
        public Component[]          Childs { get; private set; }
        public DomNode              DOMNode { get; private set; }
        public SceneComponent       SceneComponent { get; private set; }
        
        #endregion
        
        #region properties
        
        public dobj Props {
            get { return a_Props; }
        }
        
        public List<string> Class {
            get { return a_Class; }
        }
        
        public string Key {
            get { return a_Key == String.Empty ? Id : a_Key; }
        }
        
        public string Id {
            get { return a_Id; }
        }
        
        public dobj Data {
            get
            {
                return a_Data;
            }
            set
            {
                dobj newDaata = a_Data.Merge( value );

                if ( !newDaata.Equals(a_Data) )
                {
                    a_Data = newDaata;
                    DOMNode.Update();
                }
            }
        }
        
        public List<string> State {
            get
            {
                return a_State;
            }
            set
            {
                a_State = Utils.Merge( a_State, value );

                List<string> newState = Utils.Merge( a_State, value );

                if ( Utils.ListComparer<string>.Compare( newState, a_State ) )
                {
                    a_State = newState;
                    DOMNode.Update();
                }
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
            get { return Childs != null && Childs.Length != 0; }
        }
        
        #endregion
        
        
        
        #region Public Methods

        public Component( dobj _props, params Component[] _childs )
        {
            a_Props = new dobj();
            a_Data = new dobj();
            a_Stash = new dobj();
            
            InitProps( _props ?? new dobj() );
            
            InitChilds( _childs );
        }
        
        //TODO: мб удалить
        public virtual void RenderInScene()
        {
            SceneComponent.RenderInScene();
        }
        
        #endregion
        
        #region Internal Methods
        // тут должны быть методы которые не будет видно за пределами namespace vicos, но иx почему то видно

        internal void InitComponent( DomNode _domNode )
        {
            DOMNode = _domNode;
            
            OnWillMount();
            
            //DOMNode.Update( Render(  ) );
            
            OnDidMount();
        }
        
        // возвращает прошлые данные
        internal void MigrateComponent( Component from )
        {
            /*bool wasChanges = false;

            if ( Utils.ListComparer<string>.Compare( from.Class, Class ) )
            {
                a_Class = from.Class;
                
                wasChanges = true;
            }

            if ( Utils.ListComparer<string>.Compare( from.State, State ) )
            {
                a_State = from.State;
                
                wasChanges = true;
            }

            if ( !from.Data.Equals(Data) )
            {
                a_Data = from.Data;
                
                wasChanges = true;
            }

            if ( !from.Props.Equals(Props) )
            {
                a_Props = from.Props;
                
                wasChanges = true;
            }

            if( wasChanges )
                UpdateComponent();
            */
            
            a_Class = from.Class;
            a_State = from.State;
            a_Data  = from.Data;
            a_Props = from.Props;
            
            DOMNode.Update();
        }

        internal void RemoveComponent()
        {
            OnUnmount();
            
            DOMNode.RemoveAllChilds();
        }

        internal abstract Component[] Render();

        #endregion
        
        #region Protected Methods

        protected virtual void OnWillMount() { }

        protected virtual void OnDidMount() { }

        // Сомпонент получает новые свойства. Тут можно иx немного изменить
        // ничего не обновлено, на вxод приxодят новые свойства
        // возвращаются изменённые новые свойства
        protected virtual dobj OnWillReceiveProps( dobj newProps )
        {
            return null;
        }

        // Изменить данные компонента после получения новыx свойств
        // На этом этапе свойства обновлены, всё остальное прежнее
        // Возвращаются новые данные ( data компонента )
        protected virtual dobj GetDerivedStateFromProps()
        {
            return null;
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
        
        private void InitChilds( Component[] _childs )
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
        
        private void InitProps( dobj _newProps )
        {
            #if DEVELOPMENT
            
            // всяческие проверки свойств
                
            #endif

            dobj newProps = _newProps.Merge( OnWillReceiveProps( _newProps ) ?? new dobj() );

            a_Id = ((string)newProps["id"]) ?? "";
            a_Key = ((string)newProps["key"]) ?? "";

            a_Class = StrTolist( (string) newProps["class"] ?? "" );
            a_State = StrTolist( (string) newProps["state"] ?? "" );

            a_Props = newProps.Merge( new dobj() {
                { "id", string.Empty },
                { "key", string.Empty },
                { "class", string.Empty },
                { "state", string.Empty }
            });

            a_Data = GetDerivedStateFromProps() ?? new dobj();
        }
        

        private List<string> StrTolist( string classes )
        {
            return classes.Split(' ').ToList();
        }

        // обновляем текущий компонент значениями переданного
        private void Update()
        {
            //TODO: чекни как работает в реакте shouldUpdate. Есть 2 пути:
            // 1 - обновлять по дефолту на пофиг virtualDom
            // 2 - не обновлять даже virtualDom, но тогда надо ли обновлять дочерние компоненты и если да то как
            
            if ( !ShouldUpdate() )
                return;
            
            OnUpdate();
        }

        #endregion
        
    }
}