using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

namespace GameEditor
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Tilemap))]
    public class TilemapShadow : MonoBehaviour
    {
        [SerializeField] private List<Sprite> targetTile;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private GameObject rootChild;

        private void Start()
        {
            Init();
        }

        private void Reset()
        {
            foreach (Transform childTransform in transform)
            {
                DestroyImmediate(childTransform.gameObject);
            }
            Init();
        }

        private void OnValidate()
        {
            Reset();
        }

        private void Init()
        {
            tilemap = GetComponent<Tilemap>();

            rootChild = new GameObject("RootChild");
            rootChild.transform.SetParent(transform);
            
            foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
            {
                if (!tilemap.HasTile(pos)) continue;
                
                Sprite sprite = tilemap.GetSprite(pos);
                if (!targetTile.Contains(sprite)) continue;
                
                GameObject shadowObject = new GameObject(sprite.name);
                shadowObject.transform.parent = rootChild.transform;
                shadowObject.transform.position = pos;

                shadowObject.AddComponent<SpriteRenderer>().sprite = sprite;
                shadowObject.AddComponent<ShadowCaster2D>();
            }
        }
    }
}
