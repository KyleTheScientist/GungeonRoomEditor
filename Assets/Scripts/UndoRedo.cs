using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UndoRedo
{
    public Stack<State> undos = new Stack<State>();
    public Stack<State> redos = new Stack<State>();

    public void RegisterState(TilemapHandler map, string eventName)
    {
        var state = new State()
        {
            map = map,
            tiles = map.AllTiles(),
            eventName = eventName,
        };
        undos.Push(state);
        redos.Clear();
    }

    public void Undo()
    {
        if (undos.Count == 0) return;
        var oldState = undos.Pop();

        if (oldState.map == null) return;
        var curState = new State()
        {
            map = oldState.map,
            tiles = oldState.map.AllTiles(),
            eventName = oldState.eventName,
        };
        redos.Push(curState);

        oldState.map.BuildFromTileArray(oldState.tiles);
    }

    public void Redo()
    {
        if (redos.Count == 0) return;

        var newState = redos.Pop();

        if (newState.map == null) return;
        var curState = new State()
        {
            map = newState.map,
            tiles = newState.map.AllTiles(),
            eventName = newState.eventName,
        };
        undos.Push(curState);

        newState.map.BuildFromTileArray(newState.tiles);
    }


    public struct State
    {
        public TilemapHandler map;
        public Tile[,] tiles;
        public string eventName;
    }
}
