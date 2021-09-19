using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image towerIcon;

    private Tower _towerPrefab;
    private Tower _currentSpawnedTower;

    public void SetTowerPrefab(Tower tower)
    {
        _towerPrefab = tower;
        towerIcon.sprite = tower.GetTowerHeadIcon();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject newTowerObj = Instantiate(_towerPrefab.gameObject);
        _currentSpawnedTower = newTowerObj.gameObject.GetComponent<Tower>();
        _currentSpawnedTower.ToggleOrderInLayer(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        _currentSpawnedTower.transform.position = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentSpawnedTower.PlacePosition == null)
        {
            Destroy(_currentSpawnedTower.gameObject);
        }
        else
        {
            foreach (var towerPlacement in LevelManager.Instance.towerPlacements)
            {
                Tower placedTower = towerPlacement.GetPlacedTower();
                if (placedTower != null && towerPlacement.transform.position == placedTower.gameObject.transform.position)
                {
                    continue;
                }
                towerPlacement.SetPlacedTower(null);
            }
            
            _currentSpawnedTower.LockPlacement();
            _currentSpawnedTower.ToggleOrderInLayer(false);
            LevelManager.Instance.RegisterSpawnedTower(_currentSpawnedTower);
            
            _currentSpawnedTower = null;
        }
    }
}
