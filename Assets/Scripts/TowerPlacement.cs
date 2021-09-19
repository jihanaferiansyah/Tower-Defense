using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    private Tower _placedTower;
    private Tower _candidateTower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_placedTower != null)
        {
            return;
        }

        Tower tower = other.GetComponent<Tower>();
        if (tower != null)
        {
            _candidateTower = tower;
            _candidateTower.SetPlacePosition(transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_placedTower == null)
        {
            _placedTower = _candidateTower;
            _candidateTower.SetPlacePosition(null);
            _candidateTower = null;
        }
    }

    public Tower GetPlacedTower()
    {
        return _placedTower;
    }
    
    public void SetPlacedTower(Tower tower = null)
    {
        _placedTower = tower;
    }
}
