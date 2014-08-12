// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace System.Windows.Controls.DataVisualization.Charting
{
    /// <summary>
    ///     Represents a control that displays a data point.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [TemplateVisualState(Name = StateCommonNormal, GroupName = GroupCommonStates)]
    [TemplateVisualState(Name = StateCommonMouseOver, GroupName = GroupCommonStates)]
    [TemplateVisualState(Name = StateSelectionUnselected, GroupName = GroupSelectionStates)]
    [TemplateVisualState(Name = StateSelectionSelected, GroupName = GroupSelectionStates)]
    [TemplateVisualState(Name = StateRevealShown, GroupName = GroupRevealStates)]
    [TemplateVisualState(Name = StateRevealHidden, GroupName = GroupRevealStates)]
    public abstract class DataPoint : Control
    {
        #region CommonStates

        /// <summary>
        ///     Common state group.
        /// </summary>
        internal const string GroupCommonStates = "CommonStates";

        /// <summary>
        ///     Normal state of the Common group.
        /// </summary>
        internal const string StateCommonNormal = "Normal";

        /// <summary>
        ///     MouseOver state of the Common group.
        /// </summary>
        internal const string StateCommonMouseOver = "MouseOver";

        #endregion CommonStates

        #region SelectionStates

        /// <summary>
        ///     Selection state group.
        /// </summary>
        internal const string GroupSelectionStates = "SelectionStates";

        /// <summary>
        ///     Unselected state of the Selection group.
        /// </summary>
        internal const string StateSelectionUnselected = "Unselected";

        /// <summary>
        ///     Selected state of the Selection group.
        /// </summary>
        internal const string StateSelectionSelected = "Selected";

        #endregion SelectionStates

        #region GroupRevealStates

        /// <summary>
        ///     Reveal state group.
        /// </summary>
        internal const string GroupRevealStates = "RevealStates";

        /// <summary>
        ///     Shown state of the Reveal group.
        /// </summary>
        internal const string StateRevealShown = "Shown";

        /// <summary>
        ///     Hidden state of the Reveal group.
        /// </summary>
        internal const string StateRevealHidden = "Hidden";

        #endregion GroupRevealStates

        #region public bool IsSelectionEnabled

        /// <summary>
        ///     Gets or sets a value indicating whether selection is enabled.
        /// </summary>
        public bool IsSelectionEnabled
        {
            get { return (bool)GetValue(IsSelectionEnabledProperty); }
            set { SetValue(IsSelectionEnabledProperty, value); }
        }

        /// <summary>
        ///     Identifies the IsSelectionEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectionEnabledProperty =
            DependencyProperty.Register(
                "IsSelectionEnabled",
                typeof(bool),
                typeof(DataPoint),
                new PropertyMetadata(false, OnIsSelectionEnabledPropertyChanged));

        /// <summary>
        ///     IsSelectionEnabledProperty property changed handler.
        /// </summary>
        /// <param name="d">Control that changed its IsSelectionEnabled.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIsSelectionEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;
            source.OnIsSelectionEnabledPropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     IsSelectionEnabledProperty property changed handler.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        protected virtual void OnIsSelectionEnabledPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue == false)
            {
                IsSelected = false;
                IsHovered = false;
            }
        }

        #endregion public bool IsSelectionEnabled

        /// <summary>
        ///     Gets a value indicating whether the data point is active.
        /// </summary>
        internal bool IsActive { get; private set; }

        /// <summary>
        ///     An event raised when the IsSelected property is changed.
        /// </summary>
        internal event RoutedPropertyChangedEventHandler<bool> IsSelectedChanged;

        /// <summary>
        ///     A value indicating whether the mouse is hovering over the data
        ///     point.
        /// </summary>
        private bool _isHovered;

        /// <summary>
        ///     Gets a value indicating whether the mouse is hovering over
        ///     the data point.
        /// </summary>
        protected bool IsHovered
        {
            get { return _isHovered; }
            private set
            {
                bool oldValue = _isHovered;
                _isHovered = value;
                if (oldValue != _isHovered)
                {
                    OnIsHoveredPropertyChanged(oldValue, value);
                }
            }
        }

        /// <summary>
        ///     IsHoveredProperty property changed handler.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        protected virtual void OnIsHoveredPropertyChanged(bool oldValue, bool newValue)
        {
            VisualStateManager.GoToState(this, newValue ? StateCommonMouseOver : StateCommonNormal, true);
        }

        #region internal bool IsSelected

        /// <summary>
        ///     Gets or sets a value indicating whether the data point is selected.
        /// </summary>
        internal bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        ///     Identifies the IsSelected dependency property.
        /// </summary>
        internal static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(DataPoint),
                new PropertyMetadata(false, OnIsSelectedPropertyChanged));

        /// <summary>
        ///     IsSelectedProperty property changed handler.
        /// </summary>
        /// <param name="d">Control that changed its IsSelected.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;
            source.OnIsSelectedPropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     IsSelectedProperty property changed handler.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnIsSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            VisualStateManager.GoToState(this, newValue ? StateSelectionSelected : StateSelectionUnselected, true);

            RoutedPropertyChangedEventHandler<bool> handler = IsSelectedChanged;
            if (handler != null)
            {
                handler(this, new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue));
            }
        }

        #endregion internal bool IsSelected

        /// <summary>
        ///     Event raised when the actual dependent value of the data point is changed.
        /// </summary>
        internal event RoutedPropertyChangedEventHandler<IComparable> ActualDependentValueChanged;

        #region public IComparable ActualDependentValue

        /// <summary>
        ///     Gets or sets the actual dependent value displayed in the chart.
        /// </summary>
        public IComparable ActualDependentValue
        {
            get { return (IComparable)GetValue(ActualDependentValueProperty); }
            set { SetValue(ActualDependentValueProperty, value); }
        }

        /// <summary>
        ///     Identifies the ActualDependentValue dependency property.
        /// </summary>
        public static readonly DependencyProperty ActualDependentValueProperty =
            DependencyProperty.Register(
                "ActualDependentValue",
                typeof(IComparable),
                typeof(DataPoint),
                new FrameworkPropertyMetadata(0.0, OnActualDependentValuePropertyChanged, CoerceActualDependentValue));

        private static object CoerceActualDependentValue(DependencyObject d, object newValue)
        {
            double coercedValue;
            if (!(newValue is double) && ValueHelper.TryConvert(newValue, out coercedValue))
            {
                return coercedValue;
            }
            return newValue;
        }

        /// <summary>
        ///     Called when the value of the ActualDependentValue property changes.
        /// </summary>
        /// <param name="d">Control that changed its ActualDependentValue.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnActualDependentValuePropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = (IComparable)e.OldValue;
            var newValue = (IComparable)e.NewValue;
            source.OnActualDependentValuePropertyChanged(oldValue, newValue);
        }
        
        /// <summary>
        ///     Called when the value of the ActualDependentValue property changes.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnActualDependentValuePropertyChanged(IComparable oldValue, IComparable newValue)
        {
            RoutedPropertyChangedEventHandler<IComparable> handler = ActualDependentValueChanged;
            if (handler != null)
            {
                handler(this, new RoutedPropertyChangedEventArgs<IComparable>(oldValue, newValue));
            }
        }

        #endregion public IComparable ActualDependentValue

        /// <summary>
        ///     This event is raised when the dependent value of the data point is
        ///     changed.
        /// </summary>
        internal event RoutedPropertyChangedEventHandler<IComparable> DependentValueChanged;

        #region public IComparable DependentValue

        /// <summary>
        ///     Gets or sets the dependent value of the Control.
        /// </summary>
        public IComparable DependentValue
        {
            get { return (IComparable)GetValue(DependentValueProperty); }
            set { SetValue(DependentValueProperty, value); }
        }

        /// <summary>
        ///     Identifies the DependentValue dependency property.
        /// </summary>
        public static readonly DependencyProperty DependentValueProperty =
            DependencyProperty.Register(
                "DependentValue",
                typeof(IComparable),
                typeof(DataPoint),
                new PropertyMetadata(null, OnDependentValuePropertyChanged));

        /// <summary>
        ///     Called when the DependentValue property changes.
        /// </summary>
        /// <param name="d">Control that changed its DependentValue.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnDependentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = (IComparable)e.OldValue;
            var newValue = (IComparable)e.NewValue;
            source.OnDependentValuePropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Called when the DependentValue property changes.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnDependentValuePropertyChanged(IComparable oldValue, IComparable newValue)
        {
            SetFormattedProperty(FormattedDependentValueProperty, DependentValueStringFormat, newValue);
            RoutedPropertyChangedEventHandler<IComparable> handler = DependentValueChanged;
            if (handler != null)
            {
                handler(this, new RoutedPropertyChangedEventArgs<IComparable>(oldValue, newValue));
            }

            if (State == DataPointState.Created)
            {
                // Prefer setting the value as a double...
                double coercedNewValue;
                ActualDependentValue = ValueHelper.TryConvert(newValue, out coercedNewValue) ? coercedNewValue : newValue;
            }
        }

        #endregion public IComparable DependentValue

        #region public string DependentValueStringFormat

        /// <summary>
        ///     Gets or sets the format string for the FormattedDependentValue property.
        /// </summary>
        public string DependentValueStringFormat
        {
            get { return GetValue(DependentValueStringFormatProperty) as string; }
            set { SetValue(DependentValueStringFormatProperty, value); }
        }

        /// <summary>
        ///     Identifies the DependentValueStringFormat dependency property.
        /// </summary>
        public static readonly DependencyProperty DependentValueStringFormatProperty =
            DependencyProperty.Register(
                "DependentValueStringFormat",
                typeof(string),
                typeof(DataPoint),
                new PropertyMetadata(null, OnDependentValueStringFormatPropertyChanged));

        /// <summary>
        ///     Called when DependentValueStringFormat property changes.
        /// </summary>
        /// <param name="d">Control that changed its DependentValueStringFormat.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnDependentValueStringFormatPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            source.OnDependentValueStringFormatPropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Called when DependentValueStringFormat property changes.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnDependentValueStringFormatPropertyChanged(string oldValue, string newValue)
        {
            SetFormattedProperty(FormattedDependentValueProperty, newValue, DependentValue);
        }

        #endregion public string DependentValueStringFormat

        #region public string FormattedDependentValue

        /// <summary>
        ///     Gets the DependentValue as formatted by the DependentValueStringFormat property.
        /// </summary>
        public string FormattedDependentValue
        {
            get { return GetValue(FormattedDependentValueProperty) as string; }
        }

        /// <summary>
        ///     Identifies the FormattedDependentValue dependency property.
        /// </summary>
        public static readonly DependencyProperty FormattedDependentValueProperty =
            DependencyProperty.Register(
                "FormattedDependentValue",
                typeof(string),
                typeof(DataPoint),
                null);

        #endregion public string FormattedDependentValue

        #region public string FormattedIndependentValue

        /// <summary>
        ///     Gets the IndependentValue as formatted by the IndependentValueStringFormat property.
        /// </summary>
        public string FormattedIndependentValue
        {
            get { return GetValue(FormattedIndependentValueProperty) as string; }
        }

        /// <summary>
        ///     Identifies the FormattedIndependentValue dependency property.
        /// </summary>
        public static readonly DependencyProperty FormattedIndependentValueProperty =
            DependencyProperty.Register(
                "FormattedIndependentValue",
                typeof(string),
                typeof(DataPoint),
                null);

        #endregion public string FormattedIndependentValue

        /// <summary>
        ///     Called when the independent value of the data point is changed.
        /// </summary>
        internal event RoutedPropertyChangedEventHandler<object> IndependentValueChanged;

        #region public object IndependentValue

        /// <summary>
        ///     Gets or sets the independent value.
        /// </summary>
        public object IndependentValue
        {
            get { return GetValue(IndependentValueProperty); }
            set { SetValue(IndependentValueProperty, value); }
        }

        /// <summary>
        ///     Identifies the IndependentValue dependency property.
        /// </summary>
        public static readonly DependencyProperty IndependentValueProperty =
            DependencyProperty.Register(
                "IndependentValue",
                typeof(object),
                typeof(DataPoint),
                new PropertyMetadata(null, OnIndependentValuePropertyChanged));

        /// <summary>
        ///     Called when the IndependentValue property changes.
        /// </summary>
        /// <param name="d">Control that changed its IndependentValue.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIndependentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            object oldValue = e.OldValue;
            object newValue = e.NewValue;
            source.OnIndependentValuePropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Called when the IndependentValue property changes.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnIndependentValuePropertyChanged(object oldValue, object newValue)
        {
            SetFormattedProperty(FormattedIndependentValueProperty, IndependentValueStringFormat, newValue);
            RoutedPropertyChangedEventHandler<object> handler = IndependentValueChanged;
            if (handler != null)
            {
                handler(this, new RoutedPropertyChangedEventArgs<object>(oldValue, newValue));
            }

            if (State == DataPointState.Created)
            {
                // Prefer setting the value as a double...
                double coercedNewValue;
                ActualIndependentValue = ValueHelper.TryConvert(newValue, out coercedNewValue) ? coercedNewValue : newValue;
            }
        }

        #endregion public object IndependentValue

        #region public string IndependentValueStringFormat

        /// <summary>
        ///     Gets or sets the format string for the FormattedIndependentValue property.
        /// </summary>
        public string IndependentValueStringFormat
        {
            get { return GetValue(IndependentValueStringFormatProperty) as string; }
            set { SetValue(IndependentValueStringFormatProperty, value); }
        }

        /// <summary>
        ///     Identifies the IndependentValueStringFormat dependency property.
        /// </summary>
        public static readonly DependencyProperty IndependentValueStringFormatProperty =
            DependencyProperty.Register(
                "IndependentValueStringFormat",
                typeof(string),
                typeof(DataPoint),
                new PropertyMetadata(null, OnIndependentValueStringFormatPropertyChanged));

        /// <summary>
        ///     Called when the value of the IndependentValueStringFormat property changes.
        /// </summary>
        /// <param name="d">Control that changed its IndependentValueStringFormat.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIndependentValueStringFormatPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            source.OnIndependentValueStringFormatPropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Called when the value of the IndependentValueStringFormat property changes.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnIndependentValueStringFormatPropertyChanged(string oldValue, string newValue)
        {
            SetFormattedProperty(FormattedIndependentValueProperty, newValue, IndependentValue);
        }

        #endregion public string IndependentValueStringFormat

        /// <summary>
        ///     Occurs when the actual independent value of the data point is
        ///     changed.
        /// </summary>
        internal event RoutedPropertyChangedEventHandler<object> ActualIndependentValueChanged;

        #region public object ActualIndependentValue

        /// <summary>
        ///     Gets or sets the actual independent value.
        /// </summary>
        public object ActualIndependentValue
        {
            get { return GetValue(ActualIndependentValueProperty); }
            set { SetValue(ActualIndependentValueProperty, value); }
        }
        
        /// <summary>
        ///     Identifies the ActualIndependentValue dependency property.
        /// </summary>
        public static readonly DependencyProperty ActualIndependentValueProperty =
            DependencyProperty.Register(
                "ActualIndependentValue",
                typeof(object),
                typeof(DataPoint),
                new FrameworkPropertyMetadata(OnActualIndependentValuePropertyChanged, CoerceActualIndependentValue));

        private static object CoerceActualIndependentValue(DependencyObject d, object newValue)
        {
            double coercedValue;
            if (!(newValue is double) && ValueHelper.TryConvert(newValue, out coercedValue))
            {
                return coercedValue;
            }
            return newValue;
        }

        /// <summary>
        ///     Called when the ActualIndependentValue property changes.
        /// </summary>
        /// <param name="d">Control that changed its ActualIndependentValue.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnActualIndependentValuePropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            object oldValue = e.OldValue;
            object newValue = e.NewValue;
            source.OnActualIndependentValuePropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Called when the ActualIndependentValue property changes.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnActualIndependentValuePropertyChanged(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventHandler<object> handler = ActualIndependentValueChanged;
            if (handler != null)
            {
                handler(this, new RoutedPropertyChangedEventArgs<object>(oldValue, newValue));
            }
        }

        #endregion public object ActualIndependentValue

        /// <summary>
        ///     Occurs when the state of a data point is changed.
        /// </summary>
        internal event RoutedPropertyChangedEventHandler<DataPointState> StateChanged;

        #region public DataPointState State
        
        /// <summary>
        ///     Gets or sets the state of the data point.
        /// </summary>
        internal DataPointState State
        {
            get { return (DataPointState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        /// <summary>
        ///     Identifies the State dependency property.
        /// </summary>
        internal static readonly DependencyProperty StateProperty =
            DependencyProperty.Register(
                "State",
                typeof(DataPointState),
                typeof(DataPoint),
                new FrameworkPropertyMetadata(DataPointState.Created, OnStatePropertyChanged, CoerceState));

        private static object CoerceState(DependencyObject d, object newValue)
        {
            var oldValue = ((DataPoint) d).State;
            if ((DataPointState) newValue < oldValue)
            {
                return oldValue;
            }
            return newValue;
        }

        /// <summary>
        ///     Called when the value of the State property changes.
        /// </summary>
        /// <param name="d">Control that changed its State.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataPoint)d;
            var oldValue = (DataPointState)e.OldValue;
            var newValue = (DataPointState)e.NewValue;
            source.OnStatePropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Called when the value of the State property changes.
        /// </summary>
        /// <param name="oldValue">The value to be replaced.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void OnStatePropertyChanged(DataPointState oldValue, DataPointState newValue)
        {
            // If state ever goes to or past PendingRemoval, the DataPoint is no longer active
            if (newValue >= DataPointState.PendingRemoval)
            {
                IsActive = false;
            }

            // Update selection
            if (DataPointState.Normal < newValue)
            {
                IsSelectionEnabled = false;
            }

            // Start state transition
            bool transitionStarted = false;
            switch (newValue)
            {
                case DataPointState.Showing:
                case DataPointState.Hiding:
                    transitionStarted = GoToCurrentRevealState();
                    break;
            }

            // Fire Changed event
            RoutedPropertyChangedEventHandler<DataPointState> handler = StateChanged;
            if (handler != null)
            {
                handler(this, new RoutedPropertyChangedEventArgs<DataPointState>(oldValue, newValue));
            }

            // Change state if no transition started
            if (!transitionStarted)
            {
                switch (newValue)
                {
                    case DataPointState.Showing:
                        if (_appliedTemplate)
                        {
                            State = DataPointState.Normal;
                        }
                        break;
                    case DataPointState.Hiding:
                        State = DataPointState.Hidden;
                        break;
                }
            }
        }

        #endregion internal DataPointState State

        /// <summary>
        ///     Gets the implementation root of the Control.
        /// </summary>
        /// <remarks>
        ///     Implements Silverlight's corresponding internal property on Control.
        /// </remarks>
        private FrameworkElement ImplementationRoot
        {
            get
            {
                return (1 == VisualTreeHelper.GetChildrenCount(this))
                    ? VisualTreeHelper.GetChild(this, 0) as FrameworkElement
                    : null;
            }
        }

        /// <summary>
        ///     Tracks whether the Reveal/Shown VisualState is available.
        /// </summary>
        private bool _haveStateRevealShown;

        /// <summary>
        ///     Tracks whether the Reveal/Hidden VisualState is available.
        /// </summary>
        private bool _haveStateRevealHidden;

        private bool _appliedTemplate;

        /// <summary>
        ///     Initializes a new instance of the DataPoint class.
        /// </summary>
        protected DataPoint()
        {
            Loaded += OnLoaded;
            IsActive = true;
        }

        /// <summary>
        ///     Updates the Control's visuals to reflect the current state(s).
        /// </summary>
        /// <returns>True if a state transition was started.</returns>
        private bool GoToCurrentRevealState()
        {
            bool transitionStarted = false;
            string stateName = null;
            switch (State)
            {
                case DataPointState.Showing:
                    if (_haveStateRevealShown)
                    {
                        stateName = StateRevealShown;
                    }
                    break;
                case DataPointState.Hiding:
                    if (_haveStateRevealHidden)
                    {
                        stateName = StateRevealHidden;
                    }
                    break;
            }
            if (null != stateName)
            {
                if (!DesignerProperties.GetIsInDesignMode(this))
                {
                    // The use of Dispatcher.BeginInvoke here is necessary to 
                    // work around the StackOverflowException Silverlight throws 
                    // when it tries to play too many VSM animations.
                    Dispatcher.BeginInvoke(new Action(() => VisualStateManager.GoToState(this, stateName, true)));
                }
                else
                {
                    VisualStateManager.GoToState(this, stateName, false);
                }
                transitionStarted = true;
            }
            return transitionStarted;
        }

        /// <summary>
        ///     Builds the visual tree for the DataPoint when a new template is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            VisualStateGroup groupReveal;

            var implementationRoot = ImplementationRoot;
            if (implementationRoot != null)
            {
                // Unhook CurrentStateChanged handler
                // ReSharper disable once AssignNullToNotNullAttribute
                groupReveal = VisualStateManager.GetVisualStateGroups(implementationRoot)
                    .Cast<VisualStateGroup>().FirstOrDefault(group => GroupRevealStates == group.Name);
                if (groupReveal != null)
                {
                    groupReveal.CurrentStateChanged -= OnCurrentStateChanged;
                }
            }

            base.OnApplyTemplate();

            implementationRoot = ImplementationRoot;
            if (implementationRoot != null)
            {
                // Hook CurrentStateChanged handler
                _haveStateRevealShown = false;
                _haveStateRevealHidden = false;
                // ReSharper disable once AssignNullToNotNullAttribute
                groupReveal = VisualStateManager.GetVisualStateGroups(implementationRoot)
                        .Cast<VisualStateGroup>().FirstOrDefault(group => GroupRevealStates == group.Name);
                if (groupReveal != null)
                {
                    groupReveal.CurrentStateChanged += OnCurrentStateChanged;
                    _haveStateRevealShown =
                        groupReveal.States.Cast<VisualState>().Any(state => StateRevealShown == state.Name);
                    _haveStateRevealHidden =
                        groupReveal.States.Cast<VisualState>().Any(state => StateRevealHidden == state.Name);
                }
            }

            _appliedTemplate = true;

            // Go to current state(s)
            GoToCurrentRevealState();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                // Transition to Showing state in design mode so DataPoint will be visible
                State = DataPointState.Showing;
            }
        }

        /// <summary>
        ///     Changes the DataPoint object's state after one of the VSM state animations completes.
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">Event arguments.</param>
        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            switch (e.NewState.Name)
            {
                case StateRevealShown:
                    if (State == DataPointState.Showing)
                    {
                        State = DataPointState.Normal;
                    }
                    break;
                case StateRevealHidden:
                    if (State == DataPointState.Hiding)
                    {
                        State = DataPointState.Hidden;
                    }
                    break;
            }
        }

        /// <summary>
        ///     Handles the Control's Loaded event.
        /// </summary>
        /// <param name="sender">The Control.</param>
        /// <param name="e">Event arguments.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            GoToCurrentRevealState();
        }

        /// <summary>
        ///     Provides handling for the MouseEnter event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            if (IsSelectionEnabled)
            {
                IsHovered = true;
            }
        }

        /// <summary>
        ///     Provides handling for the MouseLeave event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (IsSelectionEnabled)
            {
                IsHovered = false;
            }
        }

        /// <summary>
        ///     Provides handling for the MouseLeftButtonDown event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (IsSelectionEnabled)
            {
                IsSelected = (ModifierKeys.None == (ModifierKeys.Control & Keyboard.Modifiers));
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Sets a dependency property with the specified format.
        /// </summary>
        /// <param name="property">The DependencyProperty to set.</param>
        /// <param name="format">The Format string to apply to the value.</param>
        /// <param name="value">The value of the dependency property to be formatted.</param>
        internal void SetFormattedProperty(DependencyProperty property, string format, object value)
        {
            SetValue(property, string.Format(CultureInfo.CurrentCulture, format ?? "{0}", value));
        }
    }
}