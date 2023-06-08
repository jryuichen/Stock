//MIT License
//Copyright (c) 2020 Mohammed Iqubal Hussain
//Website : Polyandcode.com

using System;
using UnityEngine;
using UnityEngine.UI;

namespace PolyAndCode.UI
{
    /// <summary>
    ///     Entry for the recycling system. Extends Unity's inbuilt ScrollRect.
    /// </summary>
    public class RecyclableScrollRect : ScrollRect
    {
        public enum DirectionType
        {
            Vertical,
            Horizontal
        }

        public bool IsGrid;

        public bool AspectRatio = true;

        //Prototype cell can either be a prefab or present as a child to the content(will automatically be disabled in runtime)
        public RectTransform PrototypeCell;

        //If true the intiziation happens at Start. Controller must assign the datasource in Awake.
        //Set to false if self init is not required and use public init API.
        public bool SelfInitialize = true;

        public DirectionType Direction;

        [SerializeField] private int _segments;
        private Vector2 _prevAnchoredPos;

        private RecyclingSystem _recyclingSystem;
        [HideInInspector] public IRecyclableScrollRectDataSource DataSource;

        //Segments : coloums for vertical and rows for horizontal.
        public int Segments
        {
            set => _segments = Math.Max(value, 2);
            get => _segments;
        }

        protected override void Start()
        {
            //defafult(built-in) in scroll rect can have both directions enabled, Recyclable scroll rect can be scrolled in only one direction.
            //setting default as vertical, Initialize() will set this again.
            vertical = true;
            horizontal = false;

            if (!Application.isPlaying) return;

            if (SelfInitialize) Initialize();
        }

        /// <summary>
        ///     Initialization when selfInitalize is true. Assumes that data source is set in controller's Awake.
        /// </summary>
        private void Initialize()
        {
            //Contruct the recycling system.
            if (Direction == DirectionType.Vertical)
                _recyclingSystem =
                    new VerticalRecyclingSystem(PrototypeCell, viewport, content, DataSource, IsGrid, Segments,
                        AspectRatio);
            else if (Direction == DirectionType.Horizontal)
                _recyclingSystem =
                    new HorizontalRecyclingSystem(PrototypeCell, viewport, content, DataSource, IsGrid, Segments);
            vertical = Direction == DirectionType.Vertical;
            horizontal = Direction == DirectionType.Horizontal;

            _prevAnchoredPos = content.anchoredPosition;
            onValueChanged.RemoveListener(OnValueChangedListener);
            //Adding listener after pool creation to avoid any unwanted recycling behaviour.(rare scenerio)
            StartCoroutine(_recyclingSystem.InitCoroutine(() =>
                onValueChanged.AddListener(OnValueChangedListener)
            ));
        }

        /// <summary>
        ///     public API for Initializing when datasource is not set in controller's Awake. Make sure selfInitalize is set to
        ///     false.
        /// </summary>
        public void Initialize(IRecyclableScrollRectDataSource dataSource)
        {
            DataSource = dataSource;
            Initialize();
        }

        /// <summary>
        ///     Added as a listener to the OnValueChanged event of Scroll rect.
        ///     Recycling entry point for recyling systems.
        /// </summary>
        /// <param name="direction">scroll direction</param>
        public void OnValueChangedListener(Vector2 normalizedPos)
        {
            var dir = content.anchoredPosition - _prevAnchoredPos;
            m_ContentStartPosition += _recyclingSystem.OnValueChangedListener(dir);
            _prevAnchoredPos = content.anchoredPosition;
        }

        /// <summary>
        ///     Reloads the data. Call this if a new datasource is assigned.
        /// </summary>
        public void ReloadData()
        {
            ReloadData(DataSource);
        }

        /// <summary>
        ///     Overloaded ReloadData with dataSource param
        ///     Reloads the data. Call this if a new datasource is assigned.
        /// </summary>
        public void ReloadData(IRecyclableScrollRectDataSource dataSource)
        {
            if (_recyclingSystem != null)
            {
                StopMovement();
                onValueChanged.RemoveListener(OnValueChangedListener);
                _recyclingSystem.DataSource = dataSource;
                StartCoroutine(_recyclingSystem.InitCoroutine(() =>
                    onValueChanged.AddListener(OnValueChangedListener)
                ));
                _prevAnchoredPos = content.anchoredPosition;
            }
        }

        public void ReloadAndContinueData()
        {
            if (_recyclingSystem != null)
            {
                StopMovement();
                _recyclingSystem.DataSource = DataSource;
            }
        }

        public void ScrollTargetToTop(int targetIndex)
        {
            ScrollToTarget(targetIndex, ScrollConfig.AlignTop);
        }

        public void ScrollTargetToMiddle(int targetIndex)
        {
            ScrollToTarget(targetIndex, ScrollConfig.AlignMiddle);
        }

        public void ScrollTargetToBottom(int targetIndex)
        {
            ScrollToTarget(targetIndex, ScrollConfig.AlignBottom);
        }

        public void ScrollToTarget(int targetIndex, ScrollConfig scrollConfig)
        {
            onValueChanged.RemoveListener(OnValueChangedListener);
            verticalNormalizedPosition =
                ((VerticalRecyclingSystem)_recyclingSystem).ScrollToTarget(targetIndex, scrollConfig.AlignViewport,
                    scrollConfig.AlignElement);
            StopMovement();
            onValueChanged.AddListener(OnValueChangedListener);
        }

        public float GetCellViewportAlign(RectTransform cellRect) =>
            ((VerticalRecyclingSystem)_recyclingSystem).GetCellViewportAlign(cellRect, verticalNormalizedPosition);

        public int GetViewportMaxFullCellsCount(float viewportPadding = 0f)
        {
            var cellHeight = PrototypeCell.sizeDelta.y;
            var viewportHeight = viewport.rect.height - viewportPadding;
            return (int)(viewportHeight / cellHeight);
        }

        public struct ScrollConfig
        {
            // Viewport：視野，0 -> 置底；0.5 -> 置中；1 -> 置頂
            // Element：元素，0 -> 對齊元素底；0.5 -> 對齊元素中；1 -> 對齊元素頂

            // Getters
            public float AlignViewport { get; }
            public float AlignElement { get; }

            // Constructor
            private ScrollConfig(float alignViewport, float alignElement)
            {
                AlignViewport = alignViewport;
                AlignElement = alignElement;
            }

            // Factories
            public static ScrollConfig Create(float alignViewport, float alignElement)
            {
                var aViewport = Mathf.Clamp01(alignViewport);
                var aElement = Mathf.Clamp01(alignElement);
                return new ScrollConfig(aViewport, aElement);
            }

            public static ScrollConfig AlignTop => new ScrollConfig(1f, 1f);
            public static ScrollConfig AlignMiddle => new ScrollConfig(0.5f, 0.5f);
            public static ScrollConfig AlignBottom => new ScrollConfig(0f, 0f);
        }

        /*
        #region Testing
        private void OnDrawGizmos()
        {
            if (_recyclableScrollRect is VerticalRecyclingSystem)
            {
                ((VerticalRecyclingSystem)_recyclableScrollRect).OnDrawGizmos();
            }

            if (_recyclableScrollRect is HorizontalRecyclingSystem)
            {
                ((HorizontalRecyclingSystem)_recyclableScrollRect).OnDrawGizmos();
            }

        }
        #endregion
        */
    }
}