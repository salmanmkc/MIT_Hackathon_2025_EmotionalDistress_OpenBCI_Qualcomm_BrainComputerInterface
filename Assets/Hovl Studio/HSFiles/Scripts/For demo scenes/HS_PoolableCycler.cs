using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CGT.Pooling
{
    public class HS_PoolableCycler : MonoBehaviour
    {
        [SerializeField] protected List<HS_Poolable> prefabs = new List<HS_Poolable>();
        [SerializeField] protected bool respondToKeyboard = true;
        [SerializeField] protected float axisInputDelay = 0.5f;

        [Header("For Responding to UI")]
        [SerializeField] protected Button toPrevious;
        [SerializeField] protected Button toNext;


        [Header("Visible for debug purposes")]
        [SerializeField] protected HS_Poolable currentPrefab;

        public virtual HS_Poolable CurrentPrefab
        {
            get
            {
                if (currentPrefab == null)
                    currentPrefab = prefabs[prefabIndex];
                return currentPrefab;
            }
        }

        protected virtual void OnEnable()
        {
            Debug.Log("PoolableCycler OnEnable called!");
            ListenForEvents();
            UpdateCurrentPrefab();
            if (respondToKeyboard)
            {
                coroutine = HandleSwitchingByKeyboard();
                StartCoroutine(coroutine);
            }
        }

        protected virtual void ListenForEvents()
        {
            if (toNext != null)
            {
                toNext.onClick.AddListener(MoveToNext);
            }

            if (toPrevious != null)
            {
                toPrevious.onClick.AddListener(MoveToPrevious);
            }
        }

        protected virtual void MoveToNext()
        {
            SetIndexToNext();
            UpdateCurrentPrefab();
        }

        protected virtual void SetIndexToNext()
        {
            prefabIndex++;
            WrapIndexAroundAsNeeded();
        }

        protected int prefabIndex = 0;

        protected virtual void WrapIndexAroundAsNeeded()
        {
            bool skipToTheLast = prefabIndex < 0,
                backToTheFirst = prefabIndex >= prefabs.Count;

            if (skipToTheLast)
            {
                prefabIndex = prefabs.Count - 1;
            }
            else if (backToTheFirst)
            {
                prefabIndex = 0;
            }
        }

        protected virtual void UpdateCurrentPrefab()
        {
            currentPrefab = prefabs[prefabIndex];
            UpdatedPrefab();
        }

        public event UnityAction UpdatedPrefab = delegate { };

        protected virtual void MoveToPrevious()
        {
            SetIndexToPrevious();
            UpdateCurrentPrefab();
        }

        protected virtual void SetIndexToPrevious()
        {
            prefabIndex--;
            WrapIndexAroundAsNeeded();
        }

        protected IEnumerator coroutine;

        protected virtual IEnumerator HandleSwitchingByKeyboard()
        {
            float timer = 0f;
            bool switchedPrefab = false;

            while (true)
            {
                if (switchedPrefab)
                {
                    timer += Time.deltaTime;
                    if (timer >= axisInputDelay)
                    {
                        switchedPrefab = false;
                        timer = 0f;
                    }
                }

                // We want the user to have the option to button-mash their
                // way through switching
                bool toPrevious = Input.GetKeyDown(KeyCode.A),
                    toNext = Input.GetKeyDown(KeyCode.D);

                if (toPrevious && !switchedPrefab)
                {
                    SetIndexToPrevious();
                    switchedPrefab = true;
                }
                if (toNext && !switchedPrefab)
                {
                    SetIndexToNext();
                    switchedPrefab = true;
                }

                if (toPrevious || toNext)
                {
                    switchedPrefab = true;
                }

                // But no mashing through the axis inputs
                if (!switchedPrefab)
                {
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        SetIndexToPrevious();
                        switchedPrefab = true;
                    }
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        SetIndexToNext();
                        switchedPrefab = true;
                    }
                }

                if (switchedPrefab)
                {
                    UpdateCurrentPrefab();
                }

                yield return null;
            }
        }

        protected virtual void OnDisable()
        {
            UNListenForEvents();

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        protected virtual void UNListenForEvents()
        {
            if (toNext != null)
            {
                toNext.onClick.RemoveListener(MoveToNext);
            }

            if (toPrevious != null)
            {
                toPrevious.onClick.RemoveListener(MoveToPrevious);
            }
        }
    }
}