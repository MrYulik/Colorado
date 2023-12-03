using System;

namespace Colorado.Core
{
    public abstract class BaseSingeltonSystem<T> : BaseSystem where T : BaseSingeltonSystem<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("Singelton is null");

                return instance;
            }

            set
            {
                instance = value as T;
            }
        }

        private void Awake()
        {
            T thisInstance = GetComponent<T>();
            if(instance == null)
            {
                instance = thisInstance;
            }
        }
    }
}