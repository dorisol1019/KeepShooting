using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CocosSharp
{
    enum Alignment
    {
        None,
        Vertical,
        Horizontal,
        Column,
        Row
    }

    struct AlignmentState 
    {

        public Alignment Alignment { get; set; }
        public float Padding { get; set; }
        public uint[] NumberOfItemsPer { get; set; }

        public AlignmentState (Alignment alignment, float padding, uint[] numberOfItems)
            : this()
        {
            Alignment = alignment;
            Padding = padding;
            NumberOfItemsPer = numberOfItems;
        }

        public AlignmentState (Alignment alignment, float padding)
            : this(alignment, padding, null)
        {       }

        public AlignmentState (Alignment alignment, uint[] numberOfItems)
            : this(alignment, 0, numberOfItems)
        {       }

    }



    /// <summary>
    /// A CCMenu
    /// Features and Limitation:
    ///  You can add MenuItem objects in runtime using addChild:
    ///  But the only accecpted children are MenuItem objects
    /// </summary>
    public class ModalMenu : CCMenu
    {
 

        #region Properties

        AlignmentState alignmentState;

        // Note that this only has a value if the GamePad or Keyboard is enabled.
        // Touch devices do not have a "focus" concept.

        #endregion Properties


        #region Constructors

        public ModalMenu(params CCMenuItem[] items) : base()
        {
            alignmentState = new AlignmentState(Alignment.None, DefaultPadding);

            Enabled = true;

            SelectedMenuItem = null;
            MenuState = CCMenuState.Waiting;

            IsColorCascaded = true;
            IsOpacityCascaded = true;

            AnchorPoint = new CCPoint(0.5f, 0.5f);
            IgnoreAnchorPointForPosition = true;

            if (items != null)
            {
                int z = 0;
                foreach (CCMenuItem item in items)
                {
                    AddChild(item, z);
                    z++;
                }
            }

            // We will set the position as being not set
            Position = CCPoint.NegativeInfinity;
        }

        #endregion Constructors


        #region Setup content

        CCEventListenerTouchOneByOne touchListener = null;

        protected override void AddedToScene()
        {
//            base.AddedToScene();

            if (Scene != null) 
            {
                touchListener = new CCEventListenerTouchOneByOne();
                touchListener.IsSwallowTouches = true;

                touchListener.OnTouchBegan = TouchBegan;
                touchListener.OnTouchMoved = TouchMoved;
                touchListener.OnTouchEnded = TouchEnded;
                touchListener.OnTouchCancelled = TouchCancelled;

                AddEventListener(touchListener,-9100000);

                switch(alignmentState.Alignment)
                {
                    case Alignment.Vertical:
                        AlignItemsVertically(alignmentState.Padding);
                        break;
                    case Alignment.Horizontal:
                        AlignItemsHorizontally(alignmentState.Padding);
                        break;
                    case Alignment.Column:
                        AlignItemsInColumns(alignmentState.NumberOfItemsPer);
                        break;
                    case Alignment.Row:
                        AlignItemsInRows(alignmentState.NumberOfItemsPer);
                        break;
                }
            }
        }

        protected override void VisibleBoundsChanged()
        {
            base.VisibleBoundsChanged();
            
        }


        

        #endregion Setup content


        public override void OnEnter()
        {
            base.OnEnter();

//            CCFocusManager.Instance.Add(menuItems.ToArray());
        }

        public override void OnExit()
        {
//            if (MenuState == CCMenuState.TrackingTouch)
//            {
//                if (SelectedMenuItem != null)
//                {
//                    SelectedMenuItem.Selected = false;
//                    SelectedMenuItem = null;
//                }
//                MenuState = CCMenuState.Waiting;
//            }

//            CCFocusManager.Instance.Remove(menuItems.ToArray());
////            RemoveEventListener(touchListener);

            base.OnExit();
            if (touchListener != null)
            {
                touchListener.IsSwallowTouches = false;
                RemoveEventListener(touchListener);
                RemoveFromParent(true);
            }
        }


        #region Touch events



        bool TouchBegan(CCTouch touch, CCEvent touchEvent)
        {
            if (MenuState != CCMenuState.Waiting || !Visible || !Enabled)
            {
                return false;
            }

            for (CCNode c = Parent; c != null; c = c.Parent)
            {
                if (c.Visible == false)
                {
                    return false;
                }
            }

            SelectedMenuItem = ItemForTouch(touch);
            if (SelectedMenuItem != null)
            {
                MenuState = CCMenuState.TrackingTouch;
                SelectedMenuItem.Selected = true;
                return true;
            }
            return false;
        }

        void TouchEnded(CCTouch touch, CCEvent touchEvent)
        {
            Debug.Assert(MenuState == CCMenuState.TrackingTouch, "[Menu TouchEnded] -- invalid state");
            if (SelectedMenuItem != null)
            {
                SelectedMenuItem.Selected = false;
                SelectedMenuItem.Activate();
            }
            MenuState = CCMenuState.Waiting;
        }

        void TouchCancelled(CCTouch touch, CCEvent touchEvent)
        {
            Debug.Assert(MenuState == CCMenuState.TrackingTouch, "[Menu ccTouchCancelled] -- invalid state");
            if (SelectedMenuItem != null)
            {
                SelectedMenuItem.Selected = false;
            }
            MenuState = CCMenuState.Waiting;
        }

        void TouchMoved(CCTouch touch, CCEvent touchEvent)
        {
            Debug.Assert(MenuState == CCMenuState.TrackingTouch, "[Menu TouchMoved] -- invalid state");
            CCMenuItem currentItem = ItemForTouch(touch);

            if (currentItem != SelectedMenuItem)
            {
                if(SelectedMenuItem != null)
                {
                    SelectedMenuItem.Selected = false;
                }

                if(currentItem != null)
                {
                    currentItem.Selected = true;
                }

                SelectedMenuItem = currentItem;
            }

        }

        #endregion Touch events
        


    }
}
