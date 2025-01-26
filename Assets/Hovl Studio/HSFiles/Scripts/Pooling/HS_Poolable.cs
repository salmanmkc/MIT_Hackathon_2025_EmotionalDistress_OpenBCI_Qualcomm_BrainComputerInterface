using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CGT.Pooling
{
    public class HS_Poolable : MonoBehaviour
    {
        [TextArea(3, 6)]
        [SerializeField] protected string notes = string.Empty;

        [SerializeField] protected HS_Poolable prefab;

        [Tooltip("What will make this rejoin the pool")]
        [SerializeField] protected HS_PoolRejoinMode rejoinMode = HS_PoolRejoinMode.Manual;
        [Tooltip("Only applies if Rejoin Mode is Auto. In which case, the delay applies automatically.")]
        [SerializeField] protected float delayBeforeAutoRejoin = 1f;

        public virtual HS_Poolable Prefab
        {
            get { return prefab; }
            set { prefab = value; }
            // ^Why this accessor? For some reason, the prefab ref gets messed up by something in Unity.
            // Makes it refer to an instance instead of the original asset in the project
        }

        protected virtual void OnEnable()
        {
            if (rejoinMode == HS_PoolRejoinMode.Auto)
            {
                RejoinPoolAfter(delayBeforeAutoRejoin);
            }
        }

        public virtual void RejoinPoolAfter(float howLongToWait)
        {
            if (RejoiningPool)
            {
                //Debug.Log($"{this.name} already in the process of rejoining the pool.");
                return;
            }

            RejoiningPool = true;
            Invoke(nameof(RejoinPool), howLongToWait);
        }

        public virtual bool RejoiningPool { get; protected set; }

        public virtual void RejoinPool()
        {
            RejoiningPool = false;

            bool rejoinCompatibleWithDeactivation = rejoinMode == HS_PoolRejoinMode.Manual ||
                rejoinMode == HS_PoolRejoinMode.Auto;

            this.gameObject.SetActive(false);

            ReadyToRejoinPool(this);
            AnyReadyToRejoinPool(this);
            RejoiningPool = false;
        }

        public event UnityAction<HS_Poolable> ReadyToRejoinPool = delegate { };
        public static event UnityAction<HS_Poolable> AnyReadyToRejoinPool = delegate { };

        public virtual void Init(int id)
        {
            if (ID >= 0)
                return;

            ID = id;
        }

        public virtual int ID { get; protected set; } = -1;

    }
}