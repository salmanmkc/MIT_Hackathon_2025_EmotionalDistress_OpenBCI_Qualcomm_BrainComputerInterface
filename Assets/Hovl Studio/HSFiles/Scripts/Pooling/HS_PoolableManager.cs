using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CGT.Pooling
{
    public class HS_PoolableManager : MonoBehaviour
    {
        [SerializeField] protected int defaultPoolCapacity = 5;
        [SerializeField] protected int maxPoolCapacity = 20;
        [SerializeField] protected bool collectionCheck = true;

        /// <summary>
        /// Makes sure at least one VFXManager exists, returning the first one found
        /// </summary>
        public static HS_PoolableManager EnsureExists()
        {
            HS_PoolableManager manager = FindObjectOfType<HS_PoolableManager>();
            if (manager == null)
            {
                string managerName = "CGT_ObjectPoolManager";
                GameObject managerGO = new GameObject(managerName);
                manager = managerGO.AddComponent<HS_PoolableManager>();
                // ^We init it here to deal with potential race conditions, what with
                // how we're making this system manage itself
            }

            return manager;
        }

        protected virtual void Awake()
        {
            transform.position = Vector3.zero;
            // ^So that when things are instantiated, they're initially in the world coords
            // specified in their prefabs
            DontDestroyOnLoad(this.gameObject);
        }

        public virtual HS_Poolable GetInstanceOf(HS_Poolable prefab)
        {
            // We want to have a separate pool for every prefab so we know
            // where to get instances from

            IObjectPool<HS_Poolable> pool = GetPoolFor(prefab);
            toCreateInstanceFrom = prefab;
            HS_Poolable instance = pool.Get();
            return instance;
        }

        protected virtual IObjectPool<HS_Poolable> GetPoolFor(HS_Poolable prefab)
        {
            bool poolSetUpAlready = poolsPerPrefab.ContainsKey(prefab);
            if (!poolSetUpAlready)
            {
                PrepareNewPoolFor(prefab);
            }

            IObjectPool<HS_Poolable> pool = poolsPerPrefab[prefab];
            return pool;
        }

        protected IDictionary<HS_Poolable, IObjectPool<HS_Poolable>> poolsPerPrefab =
            new Dictionary<HS_Poolable, IObjectPool<HS_Poolable>>();

        protected virtual void PrepareNewPoolFor(HS_Poolable prefab)
        {
            IObjectPool<HS_Poolable> pool = new ObjectPool<HS_Poolable>
                        (
                            createFunc: CreatePoolable,
                            actionOnGet: OnGetPoolableFromPool,
                            actionOnRelease: OnReturnToPool,
                            actionOnDestroy: OnPoolableDestroyed,
                            collectionCheck: collectionCheck,
                            defaultCapacity: defaultPoolCapacity,
                            maxSize: maxPoolCapacity
                        );

            poolsPerPrefab.Add(prefab, pool);

            // We want the objects to be parented to this manager to make it easier for the
            // user to keep track of them in Play Mode
            string newPoolName = $"{prefab.name} Pool";
            GameObject holderGO = new GameObject(newPoolName);
            Transform holderTrans = holderGO.transform;
            holderTrans.SetParent(this.transform);
            poolHolders.Add(prefab, holderGO.transform);
            instancesCreated.Add(prefab, 0);
        }

        protected virtual HS_Poolable CreatePoolable()
        {
            HS_Poolable created = Instantiate(toCreateInstanceFrom);
            created.Prefab = toCreateInstanceFrom; // What with the weird Unity bug
            created.gameObject.SetActive(false); // Since the user may want to do stuff before letting it get enabled
            Transform parent = poolHolders[toCreateInstanceFrom];
            created.transform.SetParent(parent, true);


            // We need to identify and keep track of individual instances
            instancesCreated[toCreateInstanceFrom]++;
            int id = instancesCreated[toCreateInstanceFrom];
            created.Init(id);
            created.name = $"{toCreateInstanceFrom.name}_{id}";
            created.ReadyToRejoinPool += OnReadyToRejoinPool;
            return created;
        }

        protected HS_Poolable toCreateInstanceFrom;
        protected IDictionary<HS_Poolable, Transform> poolHolders = new Dictionary<HS_Poolable, Transform>();
        protected IDictionary<HS_Poolable, int> instancesCreated = new Dictionary<HS_Poolable, int>();

        protected virtual void OnReadyToRejoinPool(HS_Poolable thatWillRejoin)
        {
            IObjectPool<HS_Poolable> whichToReturnTo = poolsPerPrefab[thatWillRejoin.Prefab];

            whichToReturnTo.Release(thatWillRejoin);
        }

        protected virtual void OnGetPoolableFromPool(HS_Poolable gotten)
        {
            // We don't activate the object here since the user might want to do stuff with it BEFORE
            // activating it. 
            // And yes, at the time of this writing, this func is empty on purpose.
        }

        protected virtual void OnReturnToPool(HS_Poolable returned)
        {
            Transform holder = poolHolders[returned.Prefab];
            returned.transform.SetParent(holder, true);
        }

        protected virtual void OnPoolableDestroyed(HS_Poolable destroyed)
        {
            destroyed.ReadyToRejoinPool -= OnReadyToRejoinPool;
            Destroy(destroyed.gameObject);
        }
    }
}