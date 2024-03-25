﻿using UnityEngine;

namespace Grid
{
    public class SlotGridObject
    {
        private Grid<SlotGridObject> _grid;
        private int _x;
        private int _y;

        private Slot _slotItem;

        public SlotGridObject(Grid<SlotGridObject> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;
        }

        public void SetSlotItem(Slot slot)
        {
            if (_slotItem)
            {
                GameObject.Destroy(_slotItem);
            }

            _slotItem = slot;
            _grid.TriggerGridObjectChange(_x, _y);
        }

        public string GetSlotIdentifier()
        {
            return _slotItem.Identifier;
        }


        public Slot GetSlot()
        {
            return _slotItem;
        }

        public void Destroy()
        {
            if (_slotItem)
            {
                GameObject.Destroy(_slotItem.gameObject);
            }
        }
    }
}