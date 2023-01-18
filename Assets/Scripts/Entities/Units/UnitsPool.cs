using System.Collections.Generic;
using Assets.Scripts.Enitites.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enitites.Units
{
    public class UnitsPool
    {
        private readonly Dictionary<TypeUnit, List<Unit>> _poolsActiveUnitsByType;
        private readonly Dictionary<TypeUnit, List<Unit>> _poolsByType;
        private readonly Dictionary<TypeUnit, Unit> _prefabs;
        private readonly Transform _rootPanel;

        public UnitsPool(Transform rootPool)
        {
            _poolsActiveUnitsByType = new Dictionary<TypeUnit, List<Unit>>();
            _poolsByType = new Dictionary<TypeUnit, List<Unit>>();
            _prefabs = new Dictionary<TypeUnit, Unit>();
            _rootPanel = rootPool;
        }

        public void CreatePool(TypeUnit typeUnit, Unit prefab, int count)
        {
            if (!_prefabs.ContainsKey(typeUnit))
            {
                _poolsActiveUnitsByType[typeUnit] = new List<Unit>();
                _poolsByType[typeUnit] = new List<Unit>(count);
                _prefabs[typeUnit] = prefab;
            }

            var poolUnits = new List<Unit>(count);
            for (int i = 0; i < count; i++)
            {
                poolUnits.Add(CreateUnit(typeUnit));
            }

            _poolsByType[typeUnit] = poolUnits;
        }

        public List<Unit> GetListActiveUnits(TypeUnit typeUnit)
        {
            return _poolsActiveUnitsByType[typeUnit];
        }

        public Unit GetInactiveUnit(TypeUnit typeUnit)
        {
            var listUnactiveUnits = _poolsByType[typeUnit];
            Unit unit = null;
            if (listUnactiveUnits.Count > 0)
            {
                unit = listUnactiveUnits[0];
                listUnactiveUnits.RemoveAt(0);
                _poolsActiveUnitsByType[typeUnit].Add(unit);
            }
            else
            {
                unit = CreateUnit(typeUnit);
                _poolsActiveUnitsByType[typeUnit].Add(unit);
            }

            if (unit is IReturnableToPool unitReturnableToPool)
            {
                unitReturnableToPool.EventReturnToPool += OnReturnObjectToPoolHandler;
            }

            return unit;
        }

        public void PushToInactiveUnit(Unit unit)
        {
            unit.InsidePlayArea = false;
            unit.gameObject.SetActive(false);
            unit.transform.SetParent(_rootPanel);
            _poolsActiveUnitsByType[unit.TypeUnit].Remove(unit);
            _poolsByType[unit.TypeUnit].Add(unit);
        }

        public void DisableAllUnits()
        {
            foreach (var key in _poolsActiveUnitsByType.Keys)
            {
                var listActiveUnits = _poolsActiveUnitsByType[key];
                for (int i = listActiveUnits.Count - 1; i >= 0; i--)
                {
                    PushToInactiveUnit(listActiveUnits[i]);
                }
            }
        }

        private Unit CreateUnit(TypeUnit typeUnit)
        {
            var prefab = _prefabs[typeUnit];
            var newUnit = GameObject.Instantiate(prefab, _rootPanel);
            newUnit.gameObject.SetActive(false);
            newUnit.TypeUnit = typeUnit;
            return newUnit;
        }

        private void OnReturnObjectToPoolHandler(GameObject ammoObject)
        {
            var unitComponent = ammoObject.GetComponent<Unit>();
            (unitComponent as IReturnableToPool).EventReturnToPool -= OnReturnObjectToPoolHandler;
            PushToInactiveUnit(unitComponent);
        }
    }
}
