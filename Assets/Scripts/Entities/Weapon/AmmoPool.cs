using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enitites.Weapon
{
    public class AmmoPool : MonoBehaviour
    {
        private readonly Dictionary<TypeAmmo, List<Ammo>> _poolsActiveAmmoByType;
        private readonly Dictionary<TypeAmmo, List<Ammo>> _poolsByType;
        private readonly Dictionary<TypeAmmo, GameObject> _prefabs;
        private readonly Transform _rootPanel;

        public AmmoPool(Transform rootPool)
        {
            _poolsActiveAmmoByType = new Dictionary<TypeAmmo, List<Ammo>>();
            _poolsByType = new Dictionary<TypeAmmo, List<Ammo>>();
            _prefabs = new Dictionary<TypeAmmo, GameObject>();
            _rootPanel = rootPool;
        }

        public void CreatePool(TypeAmmo typeAmmo, GameObject prefab, int count)
        {
            if (!_prefabs.ContainsKey(typeAmmo))
            {
                _poolsActiveAmmoByType[typeAmmo] = new List<Ammo>();
                _poolsByType[typeAmmo] = new List<Ammo>(count);
                _prefabs[typeAmmo] = prefab;
            }

            var poolUnits = new List<Ammo>(count);
            for (int i = 0; i < count; i++)
            {
                poolUnits.Add(CreateAmmo(typeAmmo));
            }

            _poolsByType[typeAmmo] = poolUnits;
        }

        public List<Ammo> GetListActiveUnits(TypeAmmo typeAmmo)
        {
            return _poolsActiveAmmoByType[typeAmmo];
        }

        public Ammo GetInactiveAmmo(TypeAmmo typeAmmo)
        {
            var listUnactiveAmmo = _poolsByType[typeAmmo];
            Ammo ammo = null;
            if (listUnactiveAmmo.Count > 0)
            {
                ammo = listUnactiveAmmo[0];
                listUnactiveAmmo.RemoveAt(0);
                _poolsActiveAmmoByType[typeAmmo].Add(ammo);
            }
            else
            {
                ammo = CreateAmmo(typeAmmo);
                _poolsActiveAmmoByType[typeAmmo].Add(ammo);
            }

            ammo.EventReturnToPool += OnReturnObjectToPoolHandler;
            return ammo;
        }

        public void PushToInactiveAmmo(Ammo ammo)
        {
            ammo.gameObject.SetActive(false);
            ammo.transform.SetParent(_rootPanel);
            _poolsActiveAmmoByType[ammo.TypeAmmo].Remove(ammo);
            _poolsByType[ammo.TypeAmmo].Add(ammo);
        }

        public void DisableAllUnits()
        {
            foreach (var key in _poolsActiveAmmoByType.Keys)
            {
                var listActiveUnits = _poolsActiveAmmoByType[key];
                for (int i = listActiveUnits.Count - 1; i >= 0; i--)
                {
                    PushToInactiveAmmo(listActiveUnits[i]);
                }
            }
        }

        private Ammo CreateAmmo(TypeAmmo typeAmmo)
        {
            var prefab = _prefabs[typeAmmo];
            var newAmmo = GameObject.Instantiate(prefab, _rootPanel);
            newAmmo.gameObject.SetActive(false);
            var ammoComponent = newAmmo.GetComponent<Ammo>();
            ammoComponent.TypeAmmo = typeAmmo;
            return newAmmo.GetComponent<Ammo>();
        }

        private void OnReturnObjectToPoolHandler(GameObject ammoObject)
        {
            var ammoComponent = ammoObject.GetComponent<Ammo>();
            ammoComponent.EventReturnToPool -= OnReturnObjectToPoolHandler;
            PushToInactiveAmmo(ammoComponent);
        }
    }
}
