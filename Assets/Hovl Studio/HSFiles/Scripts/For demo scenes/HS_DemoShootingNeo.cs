using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CGT.Pooling;

namespace Hovl.VFX
{
    public class HS_DemoShootingNeo : MonoBehaviour
    {
        [SerializeField] protected GameObject firePoint;
        [SerializeField] protected Camera cam;
        [SerializeField] protected List<HS_Poolable> prefabs = new List<HS_Poolable>();
        [SerializeField] protected HS_Poolable[] flashes = new HS_Poolable[0];
        [SerializeField] protected Animation camAnim; // For camera shake
        [SerializeField] protected float rapidFireShotsPerSecond = 3;
        [SerializeField] protected float projSwitchDelay = 0.25f;
        [SerializeField] protected HS_PoolableCycler cycler;

        protected virtual void Awake()
        {
            if (cycler == null)
            {
                Debug.LogError($"{this.name} needs a PoolableCycler to work with.");
                return;
            }

            poolableManager = HS_PoolableManager.EnsureExists();

            StartCoroutine(HandleRapidFiring());
        }

        protected HS_PoolableManager poolableManager;

        protected virtual IEnumerator HandleRapidFiring()
        {
            float shotCooldownTime = 1f / rapidFireShotsPerSecond;
            float timeLeft = 0;

            while (true)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0f && Input.GetMouseButton(1))
                {
                    ShootOneBullet();
                    timeLeft = shotCooldownTime;
                }

                timeLeft = Mathf.Clamp(timeLeft, 0, shotCooldownTime);
                // ^To prevent numeric overflow
                yield return null;
            }
        }

        protected virtual void ShootOneBullet()
        {
            HS_Poolable toShoot = poolableManager.GetInstanceOf(PrefabForWhatToShoot);
            toShoot.transform.position = firePoint.transform.position;
            toShoot.transform.rotation = firePoint.transform.rotation;
            toShoot.gameObject.SetActive(true);

            //Debug.Log("Shooting one bullet!");
            ShowFlashes();
        }

        protected virtual HS_Poolable PrefabForWhatToShoot
        {
            get { return cycler.CurrentPrefab; }
        }

        protected virtual void ShowFlashes()
        {
            foreach (var flashEl in flashes)
            {
                if (flashEl == null) continue;
                poolableManager.GetInstanceOf(flashEl);
                flashEl.transform.position = firePoint.transform.position;
                flashEl.transform.rotation = flashEl.transform.rotation;
                flashEl.gameObject.SetActive(true);
            }
        }

        protected virtual void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                HandleShootingSingles();
            }

            HandleTurretRotation();
        }

        protected virtual void HandleShootingSingles()
        {
            camAnim.Play(camAnim.clip.name);
            ShootOneBullet();
        }

        protected virtual void HandleTurretRotation()
        {
            if (cam != null)
            {
                RaycastHit hit;
                var mousePos = Input.mousePosition;
                Ray rayMouse = cam.ScreenPointToRay(mousePos);
                if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLength))
                {
                    RotateToMouseDirection(gameObject, hit.point);
                }
            }
            else
            {
                Debug.Log("No camera");
            }
        }

        protected float maxLength = 50;

        void RotateToMouseDirection(GameObject obj, Vector3 destination)
        {
            direction = destination - obj.transform.position;
            rotation = Quaternion.LookRotation(direction);
            obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
        }

        protected Vector3 direction;
        protected Quaternion rotation;
    }
}